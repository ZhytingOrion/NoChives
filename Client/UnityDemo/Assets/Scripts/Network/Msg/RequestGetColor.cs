using UnityEngine;

[System.Serializable]
public class RequestGetColor : BaseMsg
{
    public string name;
    public int color;

    public RequestGetColor() : base(MsgID.RequestGetColor)
    {

    }

    public override void FromData(byte[] data)
    {
    }

    public override byte[] ToData()
    {
        var str = JsonUtility.ToJson(this);
        return System.Text.Encoding.ASCII.GetBytes(str);
    }
}
