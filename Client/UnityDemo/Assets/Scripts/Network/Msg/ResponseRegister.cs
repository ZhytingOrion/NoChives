using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResponseRegOrLogInData
{
    public PlayerData player;
    public bool ret;
}

[Serializable]
public class ResponseRegister : BaseMsg
{
    //public List<PlayerData> list;
    public ResponseRegOrLogInData info;

    public ResponseRegister() : base(MsgID.ResponseRegister)
    {
    }

    public override void FromData(byte[] data)
    {
        var jsonString = System.Text.Encoding.Default.GetString(data);

        ResponseRegister jsonData = JsonUtility.FromJson<ResponseRegister>(jsonString);
        //this.list = jsonData.list;
        this.info = jsonData.info;
    }

    public override byte[] ToData()
    {
        return new byte[0];
    }
}
