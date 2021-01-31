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
	fmt.Printf("[HandleMessage] 处理网络请求 消息号 %v\n", msgID)
	switch msgID {
	case RequestRegister:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			fmt.Printf("json.Unmarshal failed err:%v msgID:%v\n", err, msgID)
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
			fmt.Printf("json.Unmarshal failed err:%v msgID:%v\n", err, msgID)
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
			fmt.Printf("json.Unmarshal failed err:%v msgID:%v\n", err, msgID)
			return
		}
		m := f.(map[string]interface{})
		name := m["name"].(string)
		mode := int(m["mode"].(float64))
		game.respUserStartGame(name, mode)
	case RequestMove:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			return
		}
		m := f.(map[string]interface{})
		x := m["x"].(float64)
		y := m["y"].(float64)
		name := m["name"].(string)
		game.respUserMove(name, x, y)
		break
	case RequestGetColor:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			fmt.Printf("json.Unmarshal failed err:%v msgID:%v\n", err, msgID)
			return
		}
		m := f.(map[string]interface{})
		name := m["name"].(string)
		color := int(m["color"].(float64))
		game.respUserGetColor(name, color)
	case RequestGetKey:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			fmt.Printf("json.Unmarshal failed err:%v msgID:%v\n", err, msgID)
			return
		}
		m := f.(map[string]interface{})
		name := m["name"].(string)
		key := int(m["key"].(float64))
		game.respUserGetColor(name, key)
	case RequestJoinRoom:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			fmt.Printf("json.Unmarshal failed err:%v msgID:%v\n", err, msgID)
			return
		}
		m := f.(map[string]interface{})
		name := m["name"].(string)
		roomId  := int(m["roomId"].(float64))
		lastRoomId   := int(m["lastRoomId"].(float64))
		game.respUserJoinRoom(name, roomId, lastRoomId)
	case RequestQuitGame:
		var f interface{}
		err := json.Unmarshal(msg.GetData(), &f)
		if err != nil {
			fmt.Printf("json.Unmarshal failed err:%v msgID:%v\n", err, msgID)
			return
		}
		m := f.(map[string]interface{})
		name := m["name"].(string)
		game.respQuitGame(name)
	default:
		fmt.Printf("我们仍未知道那天客户端到底发了什么消息 id %v\n", msgID)
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
