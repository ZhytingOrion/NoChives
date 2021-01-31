package game

import (
	"Server/zero"
)


// CreatePlayer 创建玩家
func CreatePlayer(name string, s *zero.Session) *Player {

	player := &Player{
		Name:     name,
		//Type:     rand.Intn(5),
		NextProcess: 1,                   //新玩家默认进第一关
		Colors: make([]int, 0),
		Keys: make([]int, 0),
		X:InitPositionX,
		Y:InitPositionY,

		Session:  s,
	}

	return player
}

