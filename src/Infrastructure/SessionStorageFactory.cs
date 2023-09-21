using GBReaderCremaL.Infrastructure.Exceptions;

namespace GBReaderCremaL.Infrastructure;

public class SessionStorageFactory
{
    
    private string _path;

    public SessionStorageFactory(string path)
    {
        if (path.Length < 5)
        {
            throw new FindPathException("Erreur de configuration du fichier de session");
        }
        
        if (!path.Substring(path.Length - 4, 4).Equals("json"))
        {
            throw new FindPathException("Le fichier de session doit être un fichier json");
        }
        
        _path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + path;

    }
    public SessionStorage SetSessionStorage()
    {
        if(!File.Exists(_path))
        {
            CreateSessionFile();
        }
        return new SessionStorage(_path);
    }

    public void CreateSessionFile()
    {
        File.Create(_path).Dispose();
    }
    
    public void DestroySessionFile()
    {
        File.Delete(_path);
    }
    
}