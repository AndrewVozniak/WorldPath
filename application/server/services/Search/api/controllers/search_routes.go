package controllers

import (
	services "Search/services/main"
	"github.com/gin-gonic/gin"
)

func SearchRoutes(c *gin.Context) {
	response := services.SearchRoutes(c.Param("query"))

	c.JSON(200, gin.H{
		"message": response,
	})
}
