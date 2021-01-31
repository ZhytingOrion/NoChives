package game

import (
	"Server/zero"
	"encoding/json"
	"fmt"
)

// Player Player
type Player struct {
	Name     string        `json:"name"`
	X        float32           `json:"x"`
	Y        float32           `json:"y"`
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

// 回复客户端登录、注册的消息结构体
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

// 回应玩家加入房间的数据结构
type RoomInfo struct {
	ResourceRand int      `json:"resourceRand"`          // 瓶子随机值
	PositionX []float32      `json:"xs"`                   // 同一个关卡其他玩家的位置
	PositionY []float32      `json:"ys"`
	Names     []string     `json:"names"`
}

// ToJSON 转成json数据
func (p *RoomInfo) ToJSON() []byte {
	b, _ := json.Marshal(p)
	return b
}


// 广播玩家位置的消息
type Partner struct {
	Name string         `json:"name"`
	X     float32         `json:"x"`
	Y     float32         `json:"y"`
}

// ToJSON 转成json数据
func (p *Partner) ToJSON() []byte {
	b, _ := json.Marshal(p)
	return b
}

