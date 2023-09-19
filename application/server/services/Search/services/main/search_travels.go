package services

import (
	rpc "Search/services/rabbit"
	travels "Search/services/rabbit/conf"
	"encoding/json"
	"log"
)

type Travel struct {
	ID          string `json:"id"`
	Title       string `json:"title"`
	Description string `json:"description"`
	Type        string `json:"type"`
	UpdatedAt   string `json:"updated_at"`
	CreatedAt   string `json:"created_at"`
}

func SearchTravels(req string) ([]Travel, error) {
	client := rpc.NewClient(travels.RabbitUser, travels.RabbitPass, travels.RabbitHost, travels.RabbitPort)
	defer client.Close()

	responseStr := client.SendRequest("search_travel_by_name", req)

	var travelsList []Travel
	err := json.Unmarshal([]byte(responseStr), &travelsList)
	if err != nil {
		log.Println("Error unmarshalling response: ", err)
		return nil, err
	}

	return travelsList, nil
}
