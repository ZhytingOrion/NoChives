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
        return new byte[0];
    }
}
