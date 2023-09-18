package controllers

import (
	services "Search/services/main"
	"github.com/gin-gonic/gin"
)

func SearchTravels(c *gin.Context) {
	response := services.SearchTravels(c.Param("query"))

	c.JSON(200, gin.H{
		"message": response,
	})
}
