package controllers

import (
	searchengine "Search/services/main/search_engine"
	"github.com/gin-gonic/gin"
)

func SearchTravels(c *gin.Context) {
	response, err := searchengine.SearchTravels(c.Param("query"))

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
