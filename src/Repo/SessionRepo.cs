using GBReaderCremaL.Domains;
using GBReaderCremaL.Repo.Mappers;

namespace GBReaderCremaL.Repo;

/*
 public class SessionRepo : ISessionRepo

{
    private SessionStorageFactory _storageFactory;
    private SessionStorage _sessionStorage;
    
    public SessionRepo()
    {
        try
        {
            _storageFactory = new SessionStorageFactory(JsonConfig.PathString);
        }
        catch (FindPathException fpe)
        {
            throw new InitializeStorageException(fpe.Message, fpe);
        }

        _sessionStorage = _storageFactory.SetSessionStorage();
    }

    public Session GetSessionOf(string isbn)
    {
        return SessionMapper.ToSession(_sessionStorage.GetSessionOf(isbn));
    }

    public List<Session> GetSessions()
    {
        try
        {
            return SessionMapper.ToSessions(_sessionStorage.GetSessions());
        } 
        catch (FindPathException fpe)
        {
            throw new InitializeStorageException(fpe.Message, fpe);
        }
    }

    public void SaveSession(Session session)
    {
        try
        {
            _sessionStorage.SaveSession(SessionMapper.ToDtoSession(session));
        }
        catch (FindPathException fpe)
        {
            throw new InitializeStorageException(fpe.Message);
        }
    }

    public void DestroySession(string isbn)
    {
        _sessionStorage.DestroySession(isbn);
    }
}
*/