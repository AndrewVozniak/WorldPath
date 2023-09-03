import pika

RABBITMQ_HOST = 'localhost'


def hello_queue(ch, method, props, body):
    message = body.decode()
    print(f"Received request: {message}")

    response = f"Hello, {message}!"

    ch.basic_publish(
        exchange='',
        routing_key=props.reply_to,
        properties=pika.BasicProperties(
            correlation_id=props.correlation_id
        ),
        body=response)
    ch.basic_ack(delivery_tag=method.delivery_tag)


def bye_queue(ch, method, props, body):
    message = body.decode()
    print(f"Received request: {message}")

    response = f"Bye, {message}!"

    ch.basic_publish(
        exchange='',
        routing_key=props.reply_to,
        properties=pika.BasicProperties(
            correlation_id=props.correlation_id
        ),
        body=response)
    ch.basic_ack(delivery_tag=method.delivery_tag)


connection = pika.BlockingConnection(pika.ConnectionParameters(host=RABBITMQ_HOST))
channel = connection.channel()

channel.queue_declare(queue='hello_queue')
channel.basic_consume(queue='hello_queue', on_message_callback=hello_queue)

channel.queue_declare(queue='bye_queue')
channel.basic_consume(queue='bye_queue', on_message_callback=bye_queue)

print("RPC Server is waiting for requests...")
channel.start_consuming()
