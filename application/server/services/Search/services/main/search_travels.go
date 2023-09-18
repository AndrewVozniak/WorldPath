package services

func SearchTravels(req string) string {
	//client := rpc.NewClient(conf.RabbitUser, conf.RabbitPass, conf.RabbitHost, conf.RabbitPort)
	//defer client.Close()
	//
	//response := client.SendRequest("hello_queue", "Hello World!")
	//
	//log.Printf("Received response: %s", response)

	response := "Travels" + req

	return response
}
