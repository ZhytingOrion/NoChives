package game

import (
	"encoding/json"
	"fmt"
	"log"
	"Server/zero"
)

// HandleMessage 处理网络请求
func (game *Game) HandleMessage(s *zero.Session, msg *zero.Message) {
	msgID := msg.GetID()

	switch msgID {
	case RequestRegister:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			return
		}
		m := f.(map[string]interface{})
		name := m["name"].(string)
		game.respUserRegister(name, s)
		break
	case RequestLoginIn:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			return
		}
		m := f.(map[string]interface{})
		name := m["name"].(string)
		game.respUserLoginIn(name, s)
		break
	default:
		fmt.Printf("未知的消息id %v", msgID)
/*
	case RequestMove:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			return
		}
		m := f.(map[string]interface{})
		x := m["x"]
		y := m["y"]
		playerID := m["playerID"].(string)

		player := world.GetPlayer(playerID)
		player.X = int(x.(float64))
		player.Y = int(y.(float64))

		players := world.GetPlayerList()

		for _, p := range players {
			message := zero.NewMessage(BroadcastMove, player.ToJSON())
			p.Session.GetConn().SendMessage(message)
		}
		break
 */
	}
}

// HandleDisconnect 处理网络断线
func (game *Game) HandleDisconnect(s *zero.Session, err error) {
	log.Println(s.GetConn().GetName() + " lost.")
	uid := s.GetUserID()
	lostPlayer := world.GetPlayer(uid)
	if lostPlayer == nil {
		return
	}

	world.RemovePlayer(uid)
	for _, p := range world.GetPlayerList() {
		message := zero.NewMessage(BroadcastLeave, lostPlayer.ToJSON())
		p.Session.GetConn().SendMessage(message)
	}
}

// HandleConnect 处理网络连接
func (game *Game) HandleConnect(s *zero.Session) {
	log.Println(s.GetConn().GetName() + " connected.")
}
