using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    public GameObject playerPrefab;
    public GameObject hero;

    void Start()
    {
        RegisterEvents();
        NetworkManager.Instance.Connect();
    }

    void RegisterEvents()
    {
        NotificationCenter nc = NotificationCenter.Instance;

        nc.AddEventListener(NotificationType.Network_OnBroadcastMove, OnBroadcastMove);
        nc.AddEventListener(NotificationType.Network_OnBroadcastJoinRoom, OnBroadcastJoinRoom);
        nc.AddEventListener(NotificationType.Network_OnBroadcastLeaveRoom, OnBroadcastLeaveRoom);
        nc.AddEventListener(NotificationType.Network_OnConnected, OnConnected);
        nc.AddEventListener(NotificationType.Network_OnDisconnected, OnDisconnected);
        nc.AddEventListener(NotificationType.Operate_MapPosition, OnTouchMap);
        nc.AddEventListener(NotificationType.Network_OnResponseRegister, OnResponseRegister);
        nc.AddEventListener(NotificationType.Network_OnResponseLogin, OnResponseLogin);
        nc.AddEventListener(NotificationType.Network_OnBroadcastGetColor, OnBroadcastGetColor);
        nc.AddEventListener(NotificationType.Network_OnResponseJoinRoom, OnResponseJoinRoom);
    }

    void OnResponseRegister(NotificationArg arg)
    {
        ResponseRegister data = arg.GetValue<ResponseRegister>();
        if (data.ret)
        {
            Debug.Log("注册成功");
            PlayerManager.Instance.playerName = data.name;
            PlayerManager.Instance.nextProcess = data.nextProcess;
            PlayerManager.Instance.collectedColor.Clear();
            for (int i = 0; i < data.colors.Length; i++)
            {
                PlayerManager.Instance.collectedColor.Add(DataTransfer.Instance.IndexToColor(data.colors[i]));
            }
            for(int i = 0; i<data.keys.Length;i++)
            {
                Color color = DataTransfer.Instance.IndexToColor(data.keys[i]);
                if (!PlayerManager.Instance.collectedColor.Contains(color))
                PlayerManager.Instance.collectedKeys.Add(color);
            }
        }
        else
            Debug.Log("注册失败");
    }

    void OnResponseLogin(NotificationArg arg)
    {
        ResponseLogin data = arg.GetValue<ResponseLogin>();
        if (data.ret)
        {
            Debug.Log("登录成功");
            PlayerManager.Instance.playerName = data.name;
            PlayerManager.Instance.nextProcess = data.nextProcess;
            PlayerManager.Instance.collectedColor.Clear();
            for (int i = 0; i < data.colors.Length; i++)
            {
                PlayerManager.Instance.collectedColor.Add(DataTransfer.Instance.IndexToColor(data.colors[i]));
            }
            for (int i = 0; i < data.keys.Length; i++)
            {
                Color color = DataTransfer.Instance.IndexToColor(data.keys[i]);
                if (!PlayerManager.Instance.collectedColor.Contains(color))
                    PlayerManager.Instance.collectedKeys.Add(color);
            }
        }
        else
            Debug.Log("登录失败");
    }

    void OnResponseJoinRoom(NotificationArg arg)
    {
        ResponseJoinRoom data = arg.GetValue<ResponseJoinRoom>();
        //PlayerData self = data.self;
        //hero = CreatePlayer(self.x, self.y, self.playerID, self.type);

        //foreach (PlayerData pdata in data.list)
        //{
        //    players.Add(pdata.playerID, CreatePlayer(pdata.x, pdata.y, pdata.playerID, pdata.type));
        //}
    }

    void OnBroadcastMove(NotificationArg arg)
    {
        BroadcastMove data = arg.GetValue<BroadcastMove>();

        //if (players.ContainsKey(data.playerID))
        //{
        //    var p = players[data.playerID];
        //    p.GetComponent<Player>().MoveTo(data.x, data.y);
        //}
    }

    void OnBroadcastJoinRoom(NotificationArg arg)
    {
        BroadcastJoinRoom data = arg.GetValue<BroadcastJoinRoom>();
        //var p = CreatePlayer(data.x, data.y, data.playerID, data.type);
        //players.Add(data.playerID, p);
    }

    void OnBroadcastLeaveRoom(NotificationArg arg)
    {
        BroadcastLeaveRoom data = arg.GetValue<BroadcastLeaveRoom>();

        //if (players.ContainsKey(data.playerID))
        //{
        //    var p = players[data.playerID];
        //    Destroy(p);
        //}
    }

    void OnTouchMap(NotificationArg arg)
    {
        Vector3 targetPos = arg.GetValue<Vector3>();
        //hero.GetComponent<Player>().MoveTo(targetPos.x, targetPos.z);

        NetworkManager.Instance.SendMove((int)targetPos.x, (int)targetPos.z, hero.GetComponent<Player>().ID);
    }

    public GameObject CreatePlayer(float x, float y, string playerID, int type)
    {
        Vector3 position = new Vector3(x, 0.5f, y);
        GameObject player = Instantiate(playerPrefab);
        player.transform.position = position;
        player.GetComponent<Player>().SetName(playerID);
        player.GetComponent<Player>().SetType(type);
        player.GetComponent<Player>().ID = playerID;

        return player;
    }


    void OnConnected(NotificationArg arg)
    {
        Debug.Log("OnConnected");
        //NetworkManager.Instance.SendJoin();
    }

    void OnDisconnected(NotificationArg arg)
    {
        Debug.Log("OnDisconnected");
    }

    void OnBroadcastGetColor(NotificationArg arg)
    {
        Debug.Log("OnBroadcastGetColor");

    }
}
