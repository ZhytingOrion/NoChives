package game

import (
	"Server/zero"
	"encoding/json"
	"math/rand"
)

// Player Player
type Player struct {
	PlayerID string        `json:"playerID"`
	X        int           `json:"x"`
	Y        int           `json:"y"`
	Name     string        `json:"name"`
	Type     int           `json:"type"`
	Session  *zero.Session `json:"-"`
	NextProcess int        `json:"nextProcess"`
	Colors    []int         `json:"colors"`
}

// CreatePlayer 创建玩家
func CreatePlayer(name string, s *zero.Session) *Player {

	player := &Player{
		//PlayerID: name,
		Name:     name,
		X:        rand.Intn(10),
		Y:        rand.Intn(10),
		//Type:     rand.Intn(5),
		NextProcess: 1,                   //新玩家默认进第一关
		Colors: make([]int, 0),

		Session:  s,
	}

	return player
}

// ToJSON 转成json数据
func (p *Player) ToJSON() []byte {
	b, _ := json.Marshal(p)
	return b
}
