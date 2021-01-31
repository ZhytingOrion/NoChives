using UnityEngine;

[System.Serializable]
public class RequestGetKey : BaseMsg
{
    public string name;
    public int key;

    public RequestGetKey() : base(MsgID.RequestGetKey)
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
