package routes

import (
	"Search/api/controllers"
	"github.com/gin-gonic/gin"
)

func InitRoutes(r *gin.Engine) {
	r.GET("/", controllers.Home)
	r.GET("/search/travels/:query", controllers.SearchTravels)
	r.GET("/search/places/:query", controllers.SearchPlaces)
	r.GET("/search/routes/:query", controllers.SearchRoutes)
}
