package main

import (
	router "Search/api/routes"
	conf "Search/config"
	"github.com/gin-gonic/gin"
)

func main() {
	r := gin.Default()
	router.InitRoutes(r)
	r.Run(conf.ServiceHost + ":" + conf.ServicePort)
}
