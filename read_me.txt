1、
SocketService  {
  onMessage            func (*Session, *Message） 如何处理网络请求
  onConnect             func (*Session)                   如何处理客户端连接成功？
  onDisconnect         func (*Session, error)         如何处理客户端下线
  sessions                  *sync.Map




// 此文件为客户端服务器协议文件

// Player Player
type Player struct {
	Name     string        `json:"name"`
	NextProcess int        `json:"nextProcess"`
	Colors    []int         `json:"colors"`
	Keys      []int         `json:"keys"`   //拥有的钥匙。颜色一样的钥匙只能拥有一把
}

type RoomInfo struct {
     ResourceRand int                // 瓶子随机值
     Partners []Partner      // 同一个关卡其他玩家的位置
}

type Partner struct {
    Name string
    X     float
    Y     float
}

const (
	SingleMode = 1     // 单人模式
	MultiPlayer = 2   // 多人模式
)

// 我需要x，y坐标的选点。

请求注册： 1    RequestRegister(name string)
         2    ResponseRegister (player *Player, ok bool)    玩家信息、是否注册成功
请求登录  3     RequestLoginIn（name string）
         4    ResponseLoginIn(player *Player, ok bool)      玩家信息、是否登录成功

进入游戏  5    RequestStartGame(name string, mode int32)     名字 单人模式/多人
         6    ResponseJoinRoom(roomInfo RoomInfo)  瓶子随机值，其他玩家坐标

         18   BroadcastJoinRoom(name string) 仅在多人模式下,   进入关卡的玩家


请求移动  11   RequestMove(x float, y float, name string)      玩家坐标，玩家名字      单人模式下客户端不必给服务器发此请求
        12    BroadcastMove(x float, y float, name string)   移动的玩家坐标，名字

拿到颜色  13    RequestGetColor(name string)
         14    BroadcastGetColor(name string)    拿到颜色的玩家名字  仅在多人模式下，服务器才会向客户端发此广播

进入下一关 15   RequestJoinRoom(name string, roomId int, LastRoomId int)  玩家姓名 房间id 退出房间id
         6    ResponseJoinRoom(roomInfo RoomInfo)  瓶子随机值，其他玩家坐标
         16   BroadcastLeaveRoom(name string)      仅在多人模式下，  退出关卡的玩家
         17   BroadcastJoinRoom(name string)      仅在多人模式下,   进入关卡的玩家




 网络断线 16   BroadcastLeaveRoom(name string)   仅在多人模式下，服务器才会向

	profile_ := map[string]string{
		"UserID":   u.GetID(),
		"Name":     u.prop.Data.Base.Name,
		"Level":    strconv.Itoa(int(u.prop.Data.Base.Level)),
		"FromHead": strconv.Itoa(int(u.prop.Data.Base.Head)),
		"TitleID":  strconv.Itoa(int(u.prop.Data.Title.TitleID)),
	}


profile, err := json.Marshal(profile_)

fromProfile := make(map[string]string)
err := json.Unmarshal(val.FromData, &fromProfile)










messageBody := MessageBody{
		Type:    DConst.ChatTypeNormal,
		Content: msgContent,
	}

type MessageBody struct {
	Type    DConst.MessageType
	Content interface{}
}

jsonBody, err := json.Marshal(messageBody)




