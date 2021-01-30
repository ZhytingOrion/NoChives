using UnityEngine;

[System.Serializable]
public class RequestLogin : BaseMsg
{
    public string name;

    public RequestLogin() : base(MsgID.RequestLoginIn)
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
