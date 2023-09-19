package controllers

import (
	searchengine "Search/services/main/search_engine"
	"github.com/gin-gonic/gin"
)

func SearchRoutes(c *gin.Context) {
	response := searchengine.SearchRoutes(c.Param("query"))

	c.JSON(200, gin.H{
		"message": response,
	})
}
