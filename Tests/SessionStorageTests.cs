using GBReaderCremaL.Domains;
using GBReaderCremaL.Infrastructure;
using GBReaderCremaL.Infrastructure.Exceptions;
using NUnit.Framework;



namespace GBReaderCremaL.Tests;

[TestFixture]
public class SessionStorageTests
{

    private static SessionStorageFactory? _fact;
    private SessionStorage? _sessionStorage;
    private Session? _session;
    
    [OneTimeSetUp]
    public static void BeforeAll()
    {
        _fact = new SessionStorageFactory(@"\ue36\test-q210044-session.json");
    }
    
    [SetUp]
    public void BeforeEach()
    {
        _sessionStorage = _fact!.SetSessionStorage();
        _session = new Session("2210044014",  1, "01/01/70 00:00:00", "01/01/70 00:00:00");
    }
    
    [TearDown]
    public  void AfterEach()
    {
       _fact!.DestroySessionFile();
    }

    [Test]
    public void SaveSessionCorrectly()
    {
        Assert.True(_sessionStorage!.SaveSession(_session!));
    }
    
    [Test]
    public void RetrieveSessionCorrectly()
    {
        _sessionStorage!.SaveSession(_session!);
        Session s = _sessionStorage.GetSessionOf("2210044014");
        
        Assert.That(_session!.Isbn, Is.EqualTo(s.Isbn));
        Assert.That(_session.NumPage, Is.EqualTo(s.NumPage));
        Assert.That(_session.EditTime, Is.EqualTo(s.EditTime));
        Assert.That(_session.StartTime, Is.EqualTo(s.StartTime));
    }
    
    
    
    [Test]
    public void RetrieveWrongSession()
    {
        File.WriteAllText( Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+ @"\ue36\test-q210044-session.json", 
            "données érronées");

        Assert.Throws<ResourceLoadingException>(() => _sessionStorage!.GetSessions());
        
    }
    
}