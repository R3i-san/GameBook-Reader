namespace GBReaderCremaL.Repo.Exceptions;

public class ResourceException : Exception
{
    public ResourceException(String msg, Exception e) : base(msg, e)
    {
    }   
}