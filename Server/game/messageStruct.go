package game

import (
	"Server/zero"
	"encoding/json"
	"fmt"
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

// ToJSON 转成json数据
func (p *Player) ToJSON() []byte {
	b, _ := json.Marshal(p)
	fmt.Println("player len is ", len(b))
	return b
}

type ResponseRegOrLogIn struct {
	Name     string        `json:"name"`
	Type     int           `json:"type"`
	NextProcess int        `json:"nextProcess"`
	Colors    []int         `json:"colors"`
	Keys      []int          `json:"keys"`
	Ret bool                `json:"ret"`
}

// ToJSON 转成json数据
func (p *ResponseRegOrLogIn) ToJSON() []byte {
	b, _ := json.Marshal(p)
	fmt.Println("responreg len is ", len(b))
	return b
}


type RoomInfo struct {
	resourceRand int     `json:"resourceRand"`
	partners []Partner    `json:"partners"`
}

// ToJSON 转成json数据
func (p *RoomInfo) ToJSON() []byte {
	b, _ := json.Marshal(p)
	return b
}

type Partner struct {
	x        int           `json:"x"`
	y        int           `json:"y"`
	name     string        `json:"name"`
}

// ToJSON 转成json数据
func (p *Partner) ToJSON() []byte {
	b, _ := json.Marshal(p)
	return b
}
