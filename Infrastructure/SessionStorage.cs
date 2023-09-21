using System.Globalization;
using GBReaderCremaL.Domains;
using Newtonsoft.Json;
using GBReaderCremaL.Infrastructure.DTO;
using GBReaderCremaL.Infrastructure.Exceptions;
using GBReaderCremaL.Repo;
using GBReaderCremaL.Repo.Mappers;

namespace GBReaderCremaL.Infrastructure;

public class SessionStorage : ISessionRepo
{
    private string _path;
    private List<DTOSession> _sessions;
    
    public SessionStorage(string path)
    {
        _path = path;
        _sessions = DeserializeSessions();
    }

    private List<DTOSession> DeserializeSessions()
    {
        try
        {
            string jsonSession = File.ReadAllText(_path);
            var sessions = JsonConvert.DeserializeObject<List<DTOSession>>(jsonSession);
            return sessions == null ? new List<DTOSession>():sessions;
        }
        catch (DirectoryNotFoundException)
        {
            throw new FindPathException("Le dossier contenant le fichier de session n'existe pas");
        }
        catch (FileNotFoundException)
        {
            throw new FindPathException("Le fichier de session n'existe pas");

        }
        catch (JsonReaderException)
        {
            throw new ResourceLoadingException("Impossible de lire le contenu du fichier");
        }
        catch (IOException) 
        {
            throw new ResourceLoadingException("Impossible d'ouvrir le fichier de session");
        }
    } 
    
    public List<Session> GetSessions()
    {
        return SessionMapper.ToSessions(DeserializeSessions());
    }
    
    public Session GetSessionOf(String isbn)
    {
        foreach (DTOSession s in _sessions)
        {
            if (s.Isbn.Equals(isbn))
            {
                return SessionMapper.ToSession(s);
            }
        }

        String now = DateTime.Now.ToString(DateTimeFormatInfo.InvariantInfo);
        return SessionMapper.ToSession(new DTOSession(isbn, 1, now, now));
    }

    public bool SaveSession(Session session)
    {
        DTOSession dtoSession = SessionMapper.ToDtoSession(session);
        try
        {
            DestroySession(dtoSession.Isbn);
            dtoSession.EditTime = DateTime.Now.ToString(DateTimeFormatInfo.InvariantInfo);
            _sessions.Add(dtoSession);

            string jsonSessions = JsonConvert.SerializeObject(_sessions, Formatting.Indented);
            File.WriteAllText(_path, jsonSessions);

            return true;
        }
        catch (DirectoryNotFoundException)
        {
            throw new FindPathException("Le dossier contenant le fichier de session n'existe pas");
        }
        catch (FileNotFoundException)
        {
            throw new FindPathException("Le fichier de session n'existe pas");

        }
        catch (IOException) 
        {
            throw new ResourceLoadingException("Impossible d'ouvrir le fichier de session");
        }
    }

    public bool DestroySession(string isbn)
    {
        DTOSession dtoSession = SessionMapper.ToDtoSession(GetSessionOf(isbn));
        try
        {
            if (GetSessionOf(isbn) != null)
            {

                foreach (DTOSession  dtoS in _sessions)
                {
                    if (dtoS.Isbn == dtoSession.Isbn)
                    {
                        _sessions.Remove(dtoS);
                        string jsonSessions = JsonConvert.SerializeObject(_sessions, Formatting.Indented);
                        File.WriteAllText(_path, jsonSessions);
                        return true;
                    }
                }
            }

            return false;
        } 
        catch (DirectoryNotFoundException)
        {
            throw new FindPathException("Le dossier contenant le fichier de session n'existe pas");
        }
        catch (FileNotFoundException)
        {
            throw new FindPathException("Le fichier de session n'existe pas");

        }
        catch (IOException) 
        {
            throw new ResourceLoadingException("Impossible d'ouvrir le fichier de session");
        }

    }
}