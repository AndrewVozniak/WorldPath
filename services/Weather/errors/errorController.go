package errors

import (
	"net/http"
)

func HttpError(w http.ResponseWriter, err error) {
	if err != nil {
		http.Error(w, "Internal Server Error", http.StatusInternalServerError)
	}
}
