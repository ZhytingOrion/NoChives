using UnityEngine;

[System.Serializable]
public class BroadcastMove : BaseMsg
{
    public string name;
    public float x;
    public float y;

    public BroadcastMove() : base(MsgID.BroadcastMove)
    {
    }

    public override void FromData(byte[] data)
    {
        var jsonString = System.Text.Encoding.Default.GetString(data);

        BroadcastMove jsonData = JsonUtility.FromJson<BroadcastMove>(jsonString);
        this.name = jsonData.name;
        this.x = jsonData.x;
        this.y = jsonData.y;
    }

    public override byte[] ToData()
    {
        return new byte[0];
    }
}

