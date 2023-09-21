/*using GBReaderCremaL.Domains;
using GBReaderCremaL.Infrastructure;
using GBReaderCremaL.Infrastructure.Exceptions;
using GBReaderCremaL.Infrastructure.Mappers;

namespace GBReaderCremaL.Repo;

public class StorageRepo : IStorageRepo
{
    private GameBookStorage _storage;

    private GameBookStorageFactory _factory;

    public StorageRepo()
    {
        try
        {
            _factory = new GameBookStorageFactory(DbConfig.ConnectionString);
            _storage = _factory.SetConnection();

        }
        catch (ConnectionException ce)
        {
            throw new InitializeStorageException(ce.Message, ce);
        }
    }

    public List<Page> GetPages(string isbn)
    {
        return PageMapper.ToPages(_storage.GetPages(isbn));
    }


    public List<Book> RetrieveBooks()
    {
        return BookMapper.toBooks(_storage.RetrieveBooks());
        //return new List<Book>();
    }

    public List<Book> Search(string item)
    {
        return BookMapper.toBooks(_storage.Search(item));
    }
}*/