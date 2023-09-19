package controllers

import (
	services "Search/services/main"
	"github.com/gin-gonic/gin"
)

func SearchTravels(c *gin.Context) {
	response, err := services.SearchTravels(c.Param("query"))

	if err != nil {
		c.JSON(500, gin.H{
			"message": err,
		})
		return
	}

	c.JSON(200, gin.H{
		"message": response,
	})
}
