using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ResponseJoinRoom : BaseMsg
{
    public int resourceRand;
    public float[] positionX;
    public float[] positionY;
    public string[]  names;

    public ResponseJoinRoom() : base(MsgID.ResponseJoinRoom)
    {
    }

    public override void FromData(byte[] data)
    {
        var jsonString = System.Text.Encoding.Default.GetString(data);

        ResponseJoinRoom jsonData = JsonUtility.FromJson<ResponseJoinRoom>(jsonString);
        this.resourceRand = jsonData.resourceRand;
        this.positionX = jsonData.positionX;
        this.positionY = jsonData.positionY;
        this.names = jsonData.names;
    }

    public override byte[] ToData()
    {
        return new byte[0];
    }
}
