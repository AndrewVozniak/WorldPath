package services

import (
	conf "Search/config"
	rpc "Search/services/rabbit"
	"log"
)

func Main() string {
	client := rpc.NewClient(conf.RabbitUser, conf.RabbitPass, conf.RabbitHost, conf.RabbitPort)
	defer client.Close()

	response := client.SendRequest("hello_queue", "Hello World!")

	log.Printf("Received response: %s", response)

	return response
}
