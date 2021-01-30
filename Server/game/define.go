package game

const (
	// RequestRegister 请求注册
	RequestRegister int32 = 1

	// ResponseRegister 响应注册
	ResponseRegister int32 = 2

	// RequestLoginIn 请求登录
	RequestLoginIn int32 = 3

	// ResponseLogIn 响应登录
	ResponseLogIn int32 = 4

	// RequestStartGame 请求进入游戏
	RequestStartGame int32 = 5

	// ResponseJoinRoom 响应进入房间
	ResponseJoinRoom int32 = 6

	// RequestMove 请求移动
	RequestMove int32 = 11

	// BroadcastMove 响应移动
	BroadcastMove int32 = 12

	// RequestGetColor 请求得到颜色
	RequestGetColor int32 = 13

	// BroadcastGetColor 广播玩家得到颜色
	BroadcastGetColor int32 = 14

	// RequestJoinRoom 请求加入房间
	RequestJoinRoom int32 = 15

	// BroadcastLeaveRoom 广播退出房间
	BroadcastLeaveRoom int32 = 16

	// BroadcastJoinRoom 广播加入房间
	BroadcastJoinRoom int32 = 17
)











/*
// RequestJoin 请求加入
const RequestJoin int32 = 1

// ResponseJoin 响应加入
const ResponseJoin int32 = 2

// RequestMove 请求移动
const RequestMove int32 = 3

// BroadcastMove 广播移动
const BroadcastMove int32 = 4

// BroadcastJoin 广播加入
const BroadcastJoin int32 = 5

// BroadcastLeave 广播离开
const BroadcastLeave int32 = 6
*/