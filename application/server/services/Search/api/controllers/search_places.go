package controllers

import (
	services "Search/services/main/search_engine"
	"github.com/gin-gonic/gin"
)

func SearchPlaces(c *gin.Context) {
	response := services.SearchPlaces(c.Param("query"))

	c.JSON(200, gin.H{
		"message": response,
	})
}
