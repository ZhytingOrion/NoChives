using UnityEngine;

[System.Serializable]
public class BroadcastLeaveRoom : BaseMsg
{
    public string name;
    

    public BroadcastLeaveRoom() : base(MsgID.Broadcast_Leave)
    {
    }

    public override void FromData(byte[] data)
    {
        var jsonString = System.Text.Encoding.Default.GetString(data);

        BroadcastLeaveRoom jsonData = JsonUtility.FromJson<BroadcastLeaveRoom>(jsonString);
        this.name = jsonData.name;
    }

    public override byte[] ToData()
    {
        return new byte[0];
    }
}

