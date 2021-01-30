package game

import "encoding/json"

type ResponseRegOrLogIn struct {
	player *Player   `json:"player"`
	ret bool          `json:"ret"`
}

// ToJSON 转成json数据
func (p *ResponseRegOrLogIn) ToJSON() []byte {
	b, _ := json.Marshal(p)
	return b
}
