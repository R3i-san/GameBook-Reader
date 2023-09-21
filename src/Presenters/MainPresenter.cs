using System.ComponentModel;
using GBReaderCremaL.Domains;
using GBReaderCremaL.Infrastructure.Exceptions;
using GBReaderCremaL.Presenters.Views;
using GBReaderCremaL.Repo;

namespace GBReaderCremaL.Presenters;

public class MainPresenter : IMainPresenter
{
    private IMainView _mainView;
    private ISessionRepo _sessionRepo;
    private IStorageRepo _storageRepo;
    private ISession _session;

    public MainPresenter(ISessionRepo sessionRepo, IStorageRepo storageRepo, ISession session, IMainView view)
    {
        _mainView = view;
        _sessionRepo = sessionRepo;
        _storageRepo = storageRepo;

        _session = session;
        _mainView.OnClose += Close;
    }
    
    private void Close(object? sender, CancelEventArgs args)
    {
        try
        {
            SaveSession((Session)_session);
        }
        catch (ResourceLoadingException rle)
        {
            _mainView.SetErrorMessage(rle.Message);
        }
    }
    
    public void SaveSession(Session session)
    {

        try
        {
            _sessionRepo.SaveSession(session);
        }
        catch (FindPathException fpe)
        {
            _mainView.SetErrorMessage(fpe.Message);
        }
        catch (ResourceLoadingException rle)
        {
            _mainView.SetErrorMessage(rle.Message);
        }
       
    }
    
    public void DestroySession(string isbn)
    {
        try
        {
            _sessionRepo.DestroySession(isbn);
        }
        catch (ResourceLoadingException rle)
        {
            _mainView.SetErrorMessage(rle.Message);
        }
        
    }
    
    public Session GetSessionOf(string isbn)
    {
        try
        {
            return _sessionRepo.GetSessionOf(isbn);
        }
        catch (ResourceLoadingException rle)
        {
            _mainView.SetErrorMessage(rle.Message);
        }

        return new Session();
    }

    public List<Session> RetrieveSessions()
    {
        
        try
        {
            return _sessionRepo.GetSessions();
        }
        catch (ResourceLoadingException rle)
        {
            _mainView.SetErrorMessage(rle.Message);
        }

        return new List<Session>();
    }

    public List<Book> SearchBook(string isbn)
    {
        
        try
        {
            return _storageRepo.Search(isbn);
        }
        catch (ResourceLoadingException rle)
        {
            _mainView.SetErrorMessage(rle.Message);
        }

        return new List<Book>();
        
    }

    public List<Book> RetrieveBooks()
    {
        
        try
        {
            return _storageRepo.RetrieveBooks();
        }
        catch (ResourceLoadingException rle)
        {
            _mainView.SetErrorMessage(rle.Message);
        }

        return new List<Book>();

    }

    public List<Page> GetPages(string isbn)
    {
        
        try
        {
            return _storageRepo.GetPages(isbn);
        }
        catch (ResourceLoadingException rle)
        {
            _mainView.SetErrorMessage(rle.Message);
        }

        return new List<Page>();
       
    }
}