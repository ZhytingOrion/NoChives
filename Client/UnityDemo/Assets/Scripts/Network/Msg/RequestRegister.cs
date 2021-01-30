using UnityEngine;

[System.Serializable]
public class RequestRegister : BaseMsg
{
    public string name;

    public RequestRegister() : base(MsgID.RequestRegister)
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
