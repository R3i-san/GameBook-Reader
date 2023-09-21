using GBReaderCremaL.Domains;

namespace GBReaderCremaL.Repo;

public interface ISessionRepo
{
    Session GetSessionOf(string isbn);
    List<Session> GetSessions();
    bool SaveSession(Session session);

    bool DestroySession(string isbn);

}