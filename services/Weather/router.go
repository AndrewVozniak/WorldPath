package main

import (
	"github.com/gorilla/mux"
	"net/http"
)

func StartServer() {
	r := mux.NewRouter()

	r.PathPrefix("/").HandlerFunc(weatherHandler).Methods("GET")

	http.Handle("/", r)
	http.ListenAndServe(":3000", nil)
}
