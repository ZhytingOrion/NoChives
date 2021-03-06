﻿using UnityEngine;

[System.Serializable]
public class RequestMove : BaseMsg
{
    public float x;
    public float y;
    public string name;

    public RequestMove() : base(MsgID.RequestMove)
    {
    }

    public override byte[] ToData()
    {
        var str = JsonUtility.ToJson(this);
        return System.Text.Encoding.ASCII.GetBytes(str);
    }

    public override void FromData(byte[] data)
    {
    }
}
