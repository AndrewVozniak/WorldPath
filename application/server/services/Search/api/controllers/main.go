package controllers

import (
	services "Search/services/main"
	"github.com/gin-gonic/gin"
)

func Main(c *gin.Context) {
	response := services.Main()

	c.JSON(200, gin.H{
		"message": response,
	})
}
