using UnityEngine;

[System.Serializable]
public class RequestStartGame : BaseMsg
{
    public string name;
    public int mode;

    public RequestStartGame() : base(MsgID.RequestStartGame)
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
