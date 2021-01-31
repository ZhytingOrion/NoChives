package game

import (
	"Server/zero"
	"fmt"
	"math/rand"
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
	staticRooms  *sync.Map           // 单人模式房间 key为玩家name, 单人模式房间
	userList     *sync.Map            // key为uid， value为玩家信息。包含服务器所有的玩家信息, 其实可以做全局变量。但是反正我game是单开，就直接放里面了
}

type StaticRoom struct {
	ranNum int
}

// DynamicRoom DynamicRoom
type DynamicRoom struct {
	// 多人房间下存房间内玩家
	players *sync.Map                // key是name, value 是player
	randNum int
}

func init() {
}



func (g *Game) respUserRegister(name string, s *zero.Session) {
	p, ok := g.userRegister(name, s)
	ret := &ResponseRegOrLogIn{
		Name: name,
		Type: p.Type,
		NextProcess: p.NextProcess,
		Colors: p.Colors,
		Keys: p.Keys,
		Ret: ok,
	}
	message := zero.NewMessage(ResponseRegister, ret.ToJSON())
	s.GetConn().SendMessage(message)
	fmt.Printf("[respUserRegister]  success name:%v result:%v", name, ret)
}

func (g *Game) respUserLoginIn(name string, s *zero.Session) {
	p, ok := g.userLoginIn(name)
	ret := &ResponseRegOrLogIn{
		Name: name,
		Type: p.Type,
		NextProcess: p.NextProcess,
		Colors: p.Colors,
		Keys: p.Keys,
		Ret: ok,
	}
	message := zero.NewMessage(ResponseLogIn, ret.ToJSON())
	s.GetConn().SendMessage(message)
	fmt.Printf("[respUserLoginIn] success name:%v result:%v", name, ret)
}

func (g *Game) respUserStartGame(name string, mode int) {
	player, ok := g.userList.Load(name)
	if !ok {
		fmt.Printf("[respUserStartGame] 玩家不存在 %v", name)
		return
	}
	//更新玩家所玩模式
	player.(*Player).Type = mode
	// 如果是单人模式
	var seed int
	partnerList := make([]Partner, 0)
	switch mode {
	case SingleMode:
		seed = g.createOrGetStaticRoom(name).ranNum
		break
	case MultiPlayerMode:


		break
	default:
		fmt.Printf("[respUserStartGame] failed mode undefine %v", mode)
	}

	ret := &RoomInfo{
		resourceRand: seed,
		partners: partnerList,
	}
	message := zero.NewMessage(ResponseJoinRoom, ret.ToJSON())
	player.(*Player).Session.GetConn().SendMessage(message)

	fmt.Printf("[respUserStartGame] success name:%v result:%v", name, ret)
}


// createOrGetStaticRoom 得到单人房间。如果没有就创建
func (g *Game) createOrGetStaticRoom(name string) *StaticRoom{
	sRoom := &StaticRoom{}
	val, _ := g.staticRooms.LoadOrStore(name, sRoom)
	val.(*StaticRoom).ranNum = rand.Intn(MaxRandLimit)

	// 这个单人房间没必要存随机值。每次客户端请求给他生成一个完了
	return val.(*StaticRoom)
}

func (g *Game) createOrGetDynamicRoom(level int, player *Player) *DynamicRoom {
	// 创造一个包含此玩家的多人房间
	dRoom := &DynamicRoom{
		randNum: rand.Intn(MaxRandLimit),
		players: &sync.Map{},
	}
	dRoom.players.Store(player.Name, player)

	// 得到此等级的多人房间，如果没有就创建
	//val, _ := g.dynamicRooms.LoadOrStore(level, dRoom)
	return nil
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



