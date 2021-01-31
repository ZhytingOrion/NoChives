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
	fmt.Printf("[HandleMessage] 处理网络请求 消息号 %v", msgID)
	switch msgID {
	case RequestRegister:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			fmt.Printf("json.Unmarshal failed err:%v msgID:%v", err, msgID)
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
			fmt.Printf("json.Unmarshal failed err:%v msgID:%v", err, msgID)
			return
		}
		m := f.(map[string]interface{})
		name := m["name"].(string)
		game.respUserLoginIn(name, s)
		break
	case RequestStartGame:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			fmt.Printf("json.Unmarshal failed err:%v msgID:%v", err, msgID)
			return
		}
		m := f.(map[string]interface{})
		name := m["name"].(string)
		mode := m["mode"].(int)
		game.respUserStartGame(name, mode)
	case RequestMove:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			return
		}
		m := f.(map[string]interface{})
		x := m["x"].(float32)
		y := m["y"].(float32)
		name := m["name"].(string)
		game.respUserMove(name, x, y)
		break
	case RequestGetColor:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			fmt.Printf("json.Unmarshal failed err:%v msgID:%v", err, msgID)
			return
		}
		m := f.(map[string]interface{})
		name := m["name"].(string)
		color := m["color"].(int)
		game.respUserGetColor(name, color)
	case RequestJoinRoom:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			fmt.Printf("json.Unmarshal failed err:%v msgID:%v", err, msgID)
			return
		}
		m := f.(map[string]interface{})
		name := m["name"].(string)
		roomId  := m["roomId"].(int)
		lastRoomId   := m["lastRoomId"].(int)
		game.respUserJoinRoom(name, roomId, lastRoomId)
	default:
		fmt.Printf("未知的消息id %v", msgID)
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
		message := zero.NewMessage(BroadcastLeaveRoom, lostPlayer.ToJSON())
		p.Session.GetConn().SendMessage(message)
	}
}

// HandleConnect 处理网络连接
func (game *Game) HandleConnect(s *zero.Session) {
	log.Println(s.GetConn().GetName() + " connected.")
}
