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
        return new byte[0];
    }
}
