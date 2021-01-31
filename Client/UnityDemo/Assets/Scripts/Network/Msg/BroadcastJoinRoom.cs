using UnityEngine;

[System.Serializable]
public class BroadcastJoinRoom : BaseMsg
{
    public string name;
    

    public BroadcastJoinRoom() : base(MsgID.Broadcast_Leave)
    {
    }

    public override void FromData(byte[] data)
    {
        var jsonString = System.Text.Encoding.Default.GetString(data);

        BroadcastJoinRoom jsonData = JsonUtility.FromJson<BroadcastJoinRoom>(jsonString);
        this.name = jsonData.name;
    }

    public override byte[] ToData()
    {
        return new byte[0];
    }
}
