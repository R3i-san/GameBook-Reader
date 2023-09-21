using GBReaderCremaL.Domains;
using GBReaderCremaL.Infrastructure.DTO;

namespace GBReaderCremaL.Repo.Mappers;

public class SessionMapper
{

    public static DTOSession ToDtoSession(Session session)
    {
        return new DTOSession(session.Isbn, session.NumPage, session.StartTime, session.EditTime);
    } 
    
    public static Session ToSession(DTOSession session)
    {
        return new Session(session.Isbn, session.NumPage, session.StartTime, session.EditTime);
    }

    public static List<Session> ToSessions(IList<DTOSession> dtoSessions)
    { 
        List<Session> newSessions = new List<Session>();

        foreach (DTOSession dtoS in dtoSessions)
        {
            newSessions.Add(ToSession(dtoS));
        }

        return newSessions;
    }

}