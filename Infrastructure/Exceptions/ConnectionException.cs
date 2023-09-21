namespace GBReaderCremaL.Infrastructure.Exceptions;

public class ConnectionException : Exception
{
    public ConnectionException(String msg, Exception e) : base(msg, e)
    {
    }

}