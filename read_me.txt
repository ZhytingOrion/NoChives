1、
SocketService  {
  onMessage            func (*Session, *Message） 如何处理网络请求
  onConnect             func (*Session)                   如何处理客户端连接成功？
  onDisconnect         func (*Session, error)         如何处理客户端下线
  sessions                  *sync.Map


