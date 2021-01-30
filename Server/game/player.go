package game

import (
	"Server/zero"
	"encoding/json"
)

// Player Player
type Player struct {
	Name     string        `json:"name"`
	Type     int           `json:"type"`   // 多人模式还是单人模式
	Session  *zero.Session `json:"-"`
	NextProcess int        `json:"nextProcess"`
	Colors    []int         `json:"colors"`
	Keys      []int          `json:"keys"`
}

// CreatePlayer 创建玩家
func CreatePlayer(name string, s *zero.Session) *Player {

	player := &Player{
		Name:     name,
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
