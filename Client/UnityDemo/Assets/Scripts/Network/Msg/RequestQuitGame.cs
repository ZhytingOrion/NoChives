using UnityEngine;

[System.Serializable]
public class RequestQuitGame : BaseMsg
{
    public string name;

    public RequestQuitGame() : base(MsgID.RequestQuitGame)
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
