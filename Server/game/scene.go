package game

import (
	"Server/zero"
	"fmt"
	"sync"
)

func NewGame() *Game {
	g := &Game{
		dynamicRooms: &sync.Map{},
		staticRooms: &sync.Map{},
		userList : &sync.Map{},
	}
	return g
}


type Game struct {
	dynamicRooms *sync.Map           // 此map长度为3， key为关卡 多人模式房间
	staticRooms  *sync.Map           // 单人模式房间 key为玩家nama
	userList     *sync.Map            // key为uid， value为玩家信息。包含服务器所有的玩家信息, 其实可以做全局变量。但是反正我game是单开，就直接放里面了
}

type StaticRoom struct {
	// 现在想不到单人模式的房间里存什么
}

// DynamicRoom DynamicRoom
type DynamicRoom struct {
	// 多人房间下存房间内玩家
	players map[string]*Player
}

func init() {
}



func (g *Game) respUserRegister(name string, s *zero.Session) {
	p, ok := g.userRegister(name, s)
	ret := &ResponseRegOrLogIn{
		player: p,
		ret: ok,
	}
	message := zero.NewMessage(ResponseRegister, ret.ToJSON())
	s.GetConn().SendMessage(message)
	fmt.Printf("[respUserRegister]  success name:%v result:%v", name, ret)
}

func (g *Game) respUserLoginIn(name string, s *zero.Session) {
	p, ok := g.userLoginIn(name)
	ret := &ResponseRegOrLogIn{
		player: p,
		ret: ok,
	}
	message := zero.NewMessage(ResponseLogIn, ret.ToJSON())
	s.GetConn().SendMessage(message)
	fmt.Printf("[respUserLoginIn] success name:%v result:%v", name, ret)
}


// UserRegister 玩家注册
func (g *Game) userRegister(name string, s *zero.Session) (*Player, bool){
	_, ok := g.userList.Load(name)
	if ok {
		// 注册过了不能再注册（不允许重名的现象）
		return nil, false
	}

	// 创建一个新玩家
	player := CreatePlayer(name, s)
	// 存到玩家列表中
	g.userList.Store(name, player)

	return player, true
}

// UserLoginIn 玩家登录
func (g *Game) userLoginIn(name string) (*Player, bool) {
	player, ok := g.userList.Load(name)
	if !ok {
		// 没有注册过不能登录
		// 创建一个新玩家
		return nil, false
	}
	return player.(*Player), true
}



