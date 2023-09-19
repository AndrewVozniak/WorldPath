package services

import (
	rpc "Search/services/rabbit"
	travels "Search/services/rabbit/conf"
)

func SearchTravels(req string) string {
	client := rpc.NewClient(travels.RabbitUser, travels.RabbitPass, travels.RabbitHost, travels.RabbitPort)
	defer client.Close()

	response := client.SendRequest("search_travel_by_name", req)

	return response
}
