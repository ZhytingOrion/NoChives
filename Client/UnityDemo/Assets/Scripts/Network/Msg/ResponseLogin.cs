using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResponseLogin : BaseMsg
{
    //public List<PlayerData> list;
    //public PlayerData player;
    //public bool ret;
    //public ResponseRegOrLogInData info;
    public string name;
    public int type;
    public int nextProcess;
    public int[] colors;
    public int[] keys;
    public bool ret;

    public ResponseLogin() : base(MsgID.ResponseLoginIn)
    {
    }

    public override void FromData(byte[] data)
    {
        var jsonString = System.Text.Encoding.Default.GetString(data);

        ResponseLogin jsonData = JsonUtility.FromJson<ResponseLogin>(jsonString);
        //this.list = jsonData.list;
        //this.player = jsonData.player;
        //this.ret = jsonData.ret;
        //this.info = jsonData.info;
        this.name = jsonData.name;
        this.type = jsonData.type;
        this.nextProcess = jsonData.nextProcess;
        this.colors = jsonData.colors;
        this.keys = jsonData.keys;
        this.ret = jsonData.ret;
    }

    public override byte[] ToData()
    {
        return new byte[0];
    }
}
