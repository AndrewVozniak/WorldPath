package rpc

import (
	"log"
	"time"

	amqp "github.com/rabbitmq/amqp091-go"
)

func failOnError(err error, msg string) {
	if err != nil {
		log.Panicf("%s: %s", msg, err)
	}
}

type RPCClient struct {
	conn *amqp.Connection
	ch   *amqp.Channel
}

func NewClient(host string) *RPCClient {
	conn, err := amqp.Dial(host)
	failOnError(err, "Failed to connect to RabbitMQ")
	ch, err := conn.Channel()
	failOnError(err, "Failed to open a channel")
	return &RPCClient{conn: conn, ch: ch}
}

func (client *RPCClient) SendRequest(queueName, body string) string {
	q, err := client.ch.QueueDeclare(
		queueName,
		false, // durable
		false, // delete when unused
		false, // exclusive
		false, // no-wait
		nil,   // arguments
	)
	failOnError(err, "Failed to declare a queue")

	responseQ, err := client.ch.QueueDeclare(
		"",    // empty name to let the server generate a unique name
		false, // durable
		false, // delete when unused
		true,  // exclusive
		false, // no-wait
		nil,   // arguments
	)
	failOnError(err, "Failed to declare a response queue")

	err = client.ch.Publish(
		"",     // exchange
		q.Name, // routing key
		false,  // mandatory
		false,  // immediate
		amqp.Publishing{
			ContentType: "text/plain",
			Body:        []byte(body),
			ReplyTo:     responseQ.Name,
		},
	)
	failOnError(err, "Failed to publish a message")

	// Consume the response
	msgs, err := client.ch.Consume(
		responseQ.Name, // queue
		"",             // consumer
		true,           // auto-ack
		false,          // exclusive
		false,          // no-local
		false,          // no-wait
		nil,            // args
	)
	failOnError(err, "Failed to register a consumer")

	timeout := time.After(10 * time.Second)
	select {
	case msg := <-msgs:
		return string(msg.Body)
	case <-timeout:
		log.Println("Waited 10 seconds for a response, but didn't get one.")
		return ""
	}
}

func (client *RPCClient) Close() {
	client.ch.Close()
	client.conn.Close()
}
