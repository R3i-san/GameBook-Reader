namespace GBReaderCremaL.Infrastructure.Exceptions;

public class InitializeStorageException : Exception
{
    public InitializeStorageException(String msg, Exception e) : base(msg, e) { }
}