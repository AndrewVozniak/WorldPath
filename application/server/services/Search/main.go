package main

import (
	rpc "Search/rabbit"
	"log"
)

func main() {
	client := rpc.NewClient("amqp://guest:guest@localhost:5672/")
	defer client.Close()

	response := client.SendRequest("hello_queue", "Hello World!")
	log.Printf("Received response: %s", response)
}
