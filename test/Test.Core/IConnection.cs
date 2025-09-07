namespace Test.Core;

public interface IConnection
{
    void Open();
}


public class SampleConnection : IConnection
{
    public void Open()
    {
        throw new NotImplementedException();
    }
}
