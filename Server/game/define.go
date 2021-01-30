package game


// RequestRegister 请求注册
const RequestRegister int32 = 1

// ResponseRegister 响应注册
const ResponseRegister int32 = 2


// RequestLoginIn 请求登录
const RequestLoginIn int32 = 3

// ResponseLogIn 响应登录
const ResponseLogIn int32 = 4


// RequestStartGame 请求进入游戏
const RequestStartGame int32 = 5

// ResponseStartGame 响应进入游戏
const ResponseStartGame int32 = 6

// BroadcastJoin 广播加入                   // 多人模式下进入关卡后广播在关卡中其他用户他的存在？需要吗？
const BroadcastJoin int32 = 13

// BroadcastLeave 广播离开
const BroadcastLeave int32 = 14




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