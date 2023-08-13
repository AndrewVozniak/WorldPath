package main

import (
	"encoding/json"
	"net/http"
	"weather/actions"
	"weather/errors"
)

func weatherHandler(w http.ResponseWriter, r *http.Request) {
	actions.Cors(w)
	err := json.NewEncoder(w).Encode(map[string]string{"weather": "sunny"})
	errors.HttpError(w, err)
}

func main() {
	StartServer()
}
