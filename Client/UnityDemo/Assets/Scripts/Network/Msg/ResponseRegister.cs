using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResponseRegOrLogInData
{
    public string name;
    public int type;
    public int nextProcess;
    public int[] colors;
    public int[] keys;
    public bool ret;
}

[Serializable]
public class ResponseRegister : BaseMsg
{
    //public List<PlayerData> list;
    //public ResponseRegOrLogInData info;
    public string name;
    public int type;
    public int nextProcess;
    public int[] colors;
    public int[] keys;
    public bool ret;

    public ResponseRegister() : base(MsgID.ResponseRegister)
    {
    }

    public override void FromData(byte[] data)
    {
        var jsonString = System.Text.Encoding.Default.GetString(data);

        ResponseRegister jsonData = JsonUtility.FromJson<ResponseRegister>(jsonString);
        //this.list = jsonData.list;
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
