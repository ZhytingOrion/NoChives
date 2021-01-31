using System;
using System.Collections.Generic;


using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance = null;

    public string host = "127.0.0.1";
    public int port = 3344;

    public Queue<Message> m_msgQueue = new Queue<Message>();
    // for queue
    private object thisLock = new object();

    private GameSocket m_gameSocket;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);


        m_gameSocket = new GameSocket(OnConnected, OnDisconnect, OnMessage);
    }

    void Update()
    {
        lock (thisLock)
        {
            if (m_msgQueue.Count > 0)
            {
                Message msg = m_msgQueue.Dequeue();
                HandleMessage(msg);
            }
        }
    }

    private void HandleMessage(Message msg)
    {
        Debug.Log("Get Message " + msg.MessageID);
        var msgID = (MsgID)msg.MessageID;
        switch (msgID)
        {
            case MsgID.ResponseRegister:
                {
                    ResponseRegister data = new ResponseRegister();
                    data.FromMessage(msg);
                    NotificationCenter.Instance.PushEvent(NotificationType.Network_OnResponseRegister, data);
                    break;
                }
            case MsgID.ResponseLoginIn:
                {
                    ResponseLogin data = new ResponseLogin();
                    data.FromMessage(msg);
                    NotificationCenter.Instance.PushEvent(NotificationType.Network_OnResponseLogin, data);
                    break;
                }
            case MsgID.ResponseJoinRoom:
                {
                    ResponseJoinRoom data = new ResponseJoinRoom();
                    data.FromMessage(msg);
                    NotificationCenter.Instance.PushEvent(NotificationType.Network_OnResponseJoinRoom, data);
                    break;
                }


            case MsgID.BroadcastMove:
                {
                    BroadcastMove data = new BroadcastMove();
                    data.FromMessage(msg);
                    NotificationCenter.Instance.PushEvent(NotificationType.Network_OnBroadcastMove, data);
                    break;
                }

            case MsgID.BroadcastJoinRoom:
                {
                    BroadcastJoinRoom data = new BroadcastJoinRoom();
                    data.FromMessage(msg);
                    NotificationCenter.Instance.PushEvent(NotificationType.Network_OnBroadcastJoinRoom, data);
                    break;
                }

            case MsgID.BroadcastLeaveRoom:
                {
                    BroadcastLeaveRoom data = new BroadcastLeaveRoom();
                    data.FromMessage(msg);
                    NotificationCenter.Instance.PushEvent(NotificationType.Network_OnBroadcastLeaveRoom, data);
                    break;
                }
        }
    }

    /// <summary>
    /// Connect to server.
    /// </summary>
    public void Connect()
    {
        TimeSpan timeout = new TimeSpan(0, 0, 5);
        m_gameSocket.Connect(host, port, timeout);
    }

    /// <summary>
    /// Sends message.
    /// </summary>
    /// <param name="msg">Message.</param>
    public void SendMessage(Message msg)
    {
        if (m_gameSocket != null)
        {
            m_gameSocket.PushMessage(msg);
        }
    }

    public void SendLogin(string username)
    {
        var msg = new RequestLogin();
        msg.name = username;
        SendMessage(msg.ToMessage());
        Debug.Log("发送登录消息" + username);
    }

    public void SendStartGame(string username, int mode)    //1 single 2 mutli
    {
        var msg = new RequestStartGame();
        msg.name = username;
        msg.mode = mode;
        SendMessage(msg.ToMessage());
        Debug.Log("发送游戏模式");
    }

    public void SendRegister(string username)
    {
        var msg = new RequestRegister();
        msg.name = username;
        SendMessage(msg.ToMessage());
        Debug.Log("发送注册消息" + username);
    }

    public void SendJoinRoom(string name, int roomId, int lastRoomId)
    {
        var msg = new RequestJoinRoom();
        msg.name = name;
        msg.roomId = roomId;
        msg.lastRoomId = lastRoomId;
        SendMessage(msg.ToMessage());
        Debug.Log("发送进入下一关消息" + name + " " + roomId + " " + lastRoomId);
    }

    public void SendMove(int x, int y, string name)
    {
        var msg = new RequestMove();
        msg.name = name;
        msg.x = x;
        msg.y = y;

        SendMessage(msg.ToMessage());
        Debug.Log("发送移动消息" + name);
    }

     public void SendGetColor(string name, int color)
    {
        var msg = new RequestGetColor();
        msg.name = name;
        msg.color = color;

        SendMessage(msg.ToMessage());
         Debug.Log("发送获得颜色消息" + name + " " + color);
    }


    public void SendGetKey(string name, int key)
    {
        var msg = new RequestGetKey();
        msg.name = name;
        msg.key = key;

        SendMessage(msg.ToMessage());
         Debug.Log("发送获得钥匙消息" + name + " " + key);
    }


    public void SendQuitGame(string name, int key)
    {
        var msg = new RequestQuitGame();
        msg.name = name;

        SendMessage(msg.ToMessage());
         Debug.Log("发送获得钥匙消息" + name + " " + key);
    }

    public void OnConnected(Message msg)
    {
        NotificationCenter.Instance.PushEvent(NotificationType.Network_OnConnected, null);
    }

    public void OnDisconnect(Message msg)
    {
        m_gameSocket.Reset();
        NotificationCenter.Instance.PushEvent(NotificationType.Network_OnDisconnected, null);
    }

    public void OnMessage(Message msg)
    {
        Debug.Log("Get Message "+msg.Size);
        lock (thisLock)
        {
            m_msgQueue.Enqueue(msg);
        }
    }

    private void OnDestroy()
    {
        m_gameSocket.Reset();
    }
}
