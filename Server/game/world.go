package game

import "sync"

// 思路：
// 服务器保存一个玩家列表， 相当于一张玩家注册表。包含所有玩家  名字和id
// 分为单人房间和多人房间
// 单人房间可以有1到多个
// 多人房间只有3个，分别为1级房间，2级房间。3级房间 （房间人数超过4人则不允许登录）
// 多人房间管理一个玩家列表。需要包含玩家所有信息吗？，用来通知玩家位置和获取信息。
// 一个客户端连接就是一个玩家？
// 玩家信息如下： 拥有的颜色， 已经通过的关卡，名字。


var world *DynamicRoom

func init() {
	world = CreateDynamic()
}


// CreateWorld 创建世界
func CreateDynamic() *DynamicRoom {
	world := &DynamicRoom{
		players: &sync.Map{},
	}

	return world
}

// AddPlayer 加入玩家
func (w *DynamicRoom) AddPlayer(p *Player) {
	//w.players[p.PlayerID] = p
}

// RemovePlayer 移除玩家
func (w *DynamicRoom) RemovePlayer(id string) {
	//if _, ok := w.players[id]; ok {
	//	delete(w.players, id)
	//}
}

// GetPlayer 获取玩家
func (w *DynamicRoom) GetPlayer(id string) *Player {
	//if v, ok := w.players[id]; ok {
	//	return v
	//}

	return nil
}

// GetPlayerList 获取全部玩家
func (w *DynamicRoom) GetPlayerList() []*Player {
	list := make([]*Player, 0)
	//for _, p := range w.players {
	//	list = append(list, p)
	//}
	return list
}

