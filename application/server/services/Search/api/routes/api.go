package routes

import (
	"Search/api/controllers"
	"github.com/gin-gonic/gin"
)

func InitRoutes(r *gin.Engine) {
	r.GET("/", controllers.Main)
}
