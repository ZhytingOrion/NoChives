package main

import (
	"log"
	"time"

	"Server/game"
	"Server/zero"
)

func main() {
	host := "127.0.0.1:3344"

	ss, err := zero.NewSocketService(host)
	if err != nil {
		log.Println(err)
		return
	}

	ss.SetHeartBeat(5*time.Second, 30*time.Second)

	ss.RegMessageHandler(game.HandleMessage)
	ss.RegConnectHandler(game.HandleConnect)
	ss.RegDisconnectHandler(game.HandleDisconnect)

	log.Println("server running on " + host)
	ss.Serv()
}

