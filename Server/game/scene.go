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

// respUserRegister 回复客户端注册
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
	fmt.Printf("[respUserRegister]  success name:%v result:%v\n", name, ret)
}

// respUserLoginIn 回复客户端登录
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
	fmt.Printf("[respUserLoginIn] success name:%v result:%v\n", name, ret)
}

// respUserStartGame 回复客户端进入游戏
func (g *Game) respUserStartGame(name string, mode int) {
	player, ok := g.userList.Load(name)
	if !ok {
		fmt.Printf("[respUserStartGame] 玩家不存在 %v\n", name)
		return
	}
	//更新玩家所玩模式
	player.(*Player).Type = mode
	// 如果是单人模式
	var seed int
	positionX := make([]float64, 0)
	positionY := make([]float64, 0)
	names := make([]string, 0)

	switch mode {
	case SingleMode:
		seed = g.createOrGetStaticRoom(name).ranNum
		break
	case MultiPlayerMode:
		dRoom := g.createOrGetDynamicRoom(player.(*Player))
		seed = dRoom.randNum

		f := func (key, value interface{}) bool {
			if name != key.(string) {
				names = append(names, key.(string))
				positionX = append(positionX, value.(*Player).X)
				positionX = append(positionX, value.(*Player).Y)
			}

			message := zero.NewMessage(BroadcastJoinRoom, []byte(name))
			value.(*Player).Session.GetConn().SendMessage(message)
			fmt.Printf("[respUserJoinRoom] 广播进入房间 %v \n", value.(*Player).Name)
			return true
		}
		dRoom.players.Range(f)
		break
	default:
		fmt.Printf("[respUserStartGame] failed mode undefine %v\n", mode)
	}

	ret := &RoomInfo{
		ResourceRand: seed,
		PositionX: positionX,  // 单人模式下为空，多人模式下为房间所有人但没有我
		PositionY: positionY,
		Names: names,
	}
	message := zero.NewMessage(ResponseJoinRoom, ret.ToJSON())
	player.(*Player).Session.GetConn().SendMessage(message)
	fmt.Printf("[respUserStartGame] success name:%v result:%v\n", name, ret)
}

// respUserMove 回复客户端移动同步
func (g *Game) respUserMove(name string, x float64, y float64) {
	player, ok := g.userList.Load(name)
	if !ok {
		fmt.Printf("[respUserMove] 玩家不存在 %v\n", name)
		return
	}
	player.(*Player).X = x
	player.(*Player).Y = y

	if player.(*Player).Type == SingleMode {
		return
	}

	// 多人模式给其他同步广播此玩家移动
	ret := &Partner{
		X: x,
		Y: y,
		Name: name,
	}
	message := zero.NewMessage(BroadcastMove, ret.ToJSON())
	player.(*Player).Session.GetConn().SendMessage(message)
	fmt.Printf("[respUserJoinRoom] BroadcastMove success %v \n", name)
}

// respUserGetColor 回复客户端拿到颜色
func (g *Game) respUserGetColor(name string, color int) {
	player, ok := g.userList.Load(name)
	if !ok {
		fmt.Printf("[respUserMove] 玩家不存在 %v\n", name)
		return
	}
	player.(*Player).Colors = append(player.(*Player).Colors, color)

	if player.(*Player).Type == SingleMode {
		return
	}

	message := zero.NewMessage(BroadcastGetColor, []byte(name))
	player.(*Player).Session.GetConn().SendMessage(message)
	fmt.Printf("[respUserJoinRoom] BroadcastGetColor success %v \n", name)
}



// respUserGetColor 回复客户端拿到颜色
func (g *Game) respUserGetKey(name string, key int) {
	player, ok := g.userList.Load(name)
	if !ok {
		fmt.Printf("[respUserMove] 玩家不存在 %v\n", name)
		return
	}
	player.(*Player).Keys = append(player.(*Player).Colors, key)
}

// respUserJoinRoom 请求进入房间
func (g *Game) respUserJoinRoom(name string, roomId int, lastRoomId int) {
	player, ok := g.userList.Load(name)
	if !ok {
		fmt.Printf("[respUserMove] 玩家不存在 %v\n", name)
		return
	}
	player.(*Player).NextProcess = roomId
	mode := player.(*Player).Type

	var seed int
	positionX := make([]float64, 0)
	positionY := make([]float64, 0)
	names := make([]string, 0)

	switch mode {
	case SingleMode:
		seed = g.createOrGetStaticRoom(name).ranNum
		break
	case MultiPlayerMode:
		dRoom := g.createOrGetDynamicRoom(player.(*Player))
		seed = dRoom.randNum

		f := func (key, value interface{}) bool {
			if name != key.(string) {
				names = append(names, key.(string))
				positionX = append(positionX, value.(*Player).X)
				positionX = append(positionX, value.(*Player).Y)
			}
			message := zero.NewMessage(BroadcastJoinRoom, []byte(name))
			value.(*Player).Session.GetConn().SendMessage(message)
			fmt.Printf("[respUserJoinRoom] 广播进入房间 %v %v\n", value.(*Player).Name, roomId)
			return true
		}
		dRoom.players.Range(f)
		break
	default:
		fmt.Printf("[respUserStartGame] failed mode undefine %v\n", mode)
	}

	ret := &RoomInfo{
		ResourceRand: seed,
		PositionX: positionX,  // 单人模式下为空，多人模式下为房间所有人但没有我
		PositionY: positionY,
		Names: names,
	}
	message := zero.NewMessage(ResponseJoinRoom, ret.ToJSON())
	player.(*Player).Session.GetConn().SendMessage(message)
	fmt.Printf("[respUserJoinRoom] ResponseJoinRoom success %v \n", ret)

	if mode == SingleMode {
		return
	}

	val, ok := g.dynamicRooms.Load(lastRoomId)
	if !ok {
		fmt.Println("[respUserJoinRoom] 退出多人房间错误，没有这个房间", lastRoomId)
		return
	}

	f := func (key, value interface{}) bool {
		message := zero.NewMessage(BroadcastLeaveRoom, []byte(name))
		value.(*Player).Session.GetConn().SendMessage(message)
		fmt.Printf("[respUserJoinRoom] 广播退出房间 %v %v\n", value.(*Player).Name, lastRoomId)
		return true
	}

	val.(*DynamicRoom).players.Range(f)
}


// respQuitGame 请求退出游戏
func (g *Game) respQuitGame(name string) {
	player, ok := g.userList.Load(name)
	if !ok {
		fmt.Printf("[respQuitGame] 玩家不存在 %v\n", name)
		return
	}
	g.userList.Delete(name)

	mode := player.(*Player).Type
	switch mode {
	case SingleMode:
		g.staticRooms.Delete(name)
		break
	case MultiPlayerMode:
		level := player.(*Player).NextProcess
		val, ok := g.dynamicRooms.Load(level)
		if !ok {
			fmt.Printf("[respQuitGame] 此多人房间不存在，失败 %v\n", level)
			break
		}
		val.(*DynamicRoom).players.Delete(name)
		break
	}

	g.userList.Delete(name)
	fmt.Printf("[respQuitGame] 玩家销毁成功 %v\n", name)
}




// createOrGetStaticRoom 得到单人房间。如果没有就创建
func (g *Game) createOrGetStaticRoom(name string) *StaticRoom{
	sRoom := &StaticRoom{}
	val, _ := g.staticRooms.LoadOrStore(name, sRoom)
	val.(*StaticRoom).ranNum = rand.Intn(MaxRandLimit)

	// 这个单人房间没必要存随机值。每次客户端请求给他生成一个完了
	return val.(*StaticRoom)
}

// createOrGetDynamicRoom 得到多人房间
func (g *Game) createOrGetDynamicRoom(player *Player) *DynamicRoom {
	// 创造一个包含此玩家的多人房间
	dRoom := &DynamicRoom{
		randNum: rand.Intn(MaxRandLimit),
		players: &sync.Map{},
	}
	// 如果没有此房间，则创建的房间里有我
	dRoom.players.Store(player.Name, player)

	// 得到此等级的多人房间，如果没有就创建
	val, _ := g.dynamicRooms.LoadOrStore(player.NextProcess, dRoom)
	return val.(*DynamicRoom)
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



