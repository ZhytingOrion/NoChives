using UnityEngine;

[System.Serializable]
public class RequestJoinRoom : BaseMsg
{
    public string name;
    public int roomId;
    public int lastRoomId;

    public RequestJoinRoom() : base(MsgID.RequestJoinRoom)
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