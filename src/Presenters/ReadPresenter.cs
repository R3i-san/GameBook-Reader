using System.ComponentModel;
using GBReaderCremaL.Domains;
using GBReaderCremaL.Infrastructure.Exceptions;
using GBReaderCremaL.Presenters.Events;
using GBReaderCremaL.Presenters.Routes;
using GBReaderCremaL.Presenters.Views;
using Path = GBReaderCremaL.Domains.Path;

namespace GBReaderCremaL.Presenters;

public class ReadPresenter
{
    private IReadView _readView;
    private ISession _session;
    private IBrowseToViews _router;
    private IMainPresenter _mainPresenter;
    
    private Book _book;

    public ReadPresenter(IReadView readView, ISession session, IBrowseToViews router, IMainPresenter mainPresenter)
    {
        _router = router;
        _readView = readView;
        _session = session;
        _mainPresenter = mainPresenter;

        _readView.OnMakeChoice += ChangePage;
        _readView.OnRetry += FirstPage;
        _readView.OnQuit += Save;
        _readView.OnGoHome += _router.OnChangeView;

        _router.OnEnter += Start;
        
        _session.PropertyChanged += NextPage;
    }

    private void Start(object? sender, ChangeViewEventArgs args)
    {
        var data = args.Data as string;
        string isbn = data ?? "";
        
        if (isbn != "")
        {
            Session s = _mainPresenter.GetSessionOf(isbn);

            _session.UpdateValues(s);
            SetBook(s.Isbn);
            DisplayPage(_session.NumPage);
        }
    }

    private void SetBook(string isbn)
    {
        _book = _mainPresenter.SearchBook(isbn)[0];
        _book.AddPages(_mainPresenter.GetPages(isbn));
    }

    private void Save(object? sender, EventArgs args)
    {
        if (!_book.isLastPage(_session.NumPage-1))
        {
            try
            {
                _mainPresenter.SaveSession((Session)_session);
            }
            catch (ResourceLoadingException rle)
            {
                _readView.SetStatus(rle.Message);
            }
        }
    }

    private void ChangePage(object? sender, ChangePageEventArgs args)
    {
        _session.NumPage = args.NumPage;
    }
    
    private void FirstPage(object? sender, EventArgs args)
    {
        ChangePage(this, new ChangePageEventArgs(1));
    }

    private void NextPage(object? sender, PropertyChangedEventArgs args)
    {
        DisplayPage(_session.NumPage);
    }

    private void NoChoice(object? sender, EventArgs args)
    {
        _mainPresenter.DestroySession(_book.Isbn);
        _readView.DisplayReset();
    }

    private void DisplayPage(int numPage)
    {
        _readView.DisplayPageInfos(_book.Pages[numPage-1].Txt, numPage);
        DisplayChoices(numPage);
    }

    private void DisplayChoices(int numPage)
    {
        _readView.Clear();
        
        if (_book.Pages[numPage - 1].Dst.Count == 0)
        {
            NoChoice(this, EventArgs.Empty);
        }
        
        foreach (Path p in _book.Pages[numPage-1].Dst)
        {
            _readView.DisplayChoice(p.Txt, p.Dst);
        }
    }
    
}