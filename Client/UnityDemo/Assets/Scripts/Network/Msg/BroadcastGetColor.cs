using UnityEngine;

[System.Serializable]
public class BroadcastGetColor : BaseMsg
{
    public string name;
    

    public BroadcastGetColor() : base(MsgID.BroadcastGetColor)
    {
    }

    public override void FromData(byte[] data)
    {
        var jsonString = System.Text.Encoding.Default.GetString(data);

        BroadcastGetColor jsonData = JsonUtility.FromJson<BroadcastGetColor>(jsonString);
        this.name = jsonData.name;
    }

    public override byte[] ToData()
    {
        return new byte[0];
    }
}
