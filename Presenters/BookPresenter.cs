using GBReaderCremaL.Domains;
using GBReaderCremaL.Presenters.Events;
using GBReaderCremaL.Presenters.Routes;
using GBReaderCremaL.Presenters.Views;
using GBReaderCremaL.Repo.Exceptions;

namespace GBReaderCremaL.Presenters;

public class BookPresenter
{
    private IBrowseToViews _router;
    private IBooksView _bookView;
    private IShelf _shelf;
    private IMainPresenter _mainPresenter;

    public BookPresenter(IBooksView bookView, IShelf shelf, IBrowseToViews router, IMainPresenter mainPresenter)
    {
        _bookView = bookView;
        _router = router;
        _shelf = shelf;
        _mainPresenter = mainPresenter;

        _router.OnEnter += Start;
        
        _bookView.Refresh += Refresh;
        _bookView.OnSearchBook += OnSearch;
        _bookView.OnDetailsSelected += OnDetails;
        _bookView.OnStartReading += _router.OnChangeView;
        _bookView.OnStatsDisplayed += _router.OnChangeView;

        _shelf.CollectionChanged += DisplayBooks;
        
        Refresh(this, EventArgs.Empty);
    }
    
    private void OnDetails(object? sender, DetailsEventArgs args)
    {
        DisplayDetails(_shelf.GetBook(args.Isbn));
    }
    
    
    private void OnSearch(object? sender, BookViewEventArgs args)
    {
        try
        {
            _shelf.Books = _mainPresenter.SearchBook(args.Isbn);
        }
        catch (ResourceException re)
        {
            _bookView.SetMessage(re.Message);
        }
    }

    private void Start(object? sender, ChangeViewEventArgs args)
    {
        Refresh(sender, EventArgs.Empty);
    }

    private void Refresh(object? sender, EventArgs args)
    {
        _bookView.Clear();
     
        try
        {
            _shelf.Books = _mainPresenter.RetrieveBooks();
        }
        catch (ResourceException re)
        {
            _bookView.SetMessage(re.Message);
        }
    }

    private void DisplayDetails(Book b)
    {
        _bookView.DisplayBook(b.Isbn, b.Title, b.Summary, b.Author);
    }

    private void DisplayBooks(object? sender, EventArgs args)
    {
        _bookView.Clear();

        if (_shelf.IsEmpty())
        {
            _bookView.SetMessage("Aucun livre disponible.");
        }
        else
        {
            for (int i = 0; i < _shelf.Size(); i++)
            {
                Book b = _shelf.Books[i];
                if (i == 0)
                {
                    _bookView.DisplayBook(b.Isbn, b.Title, b.Summary, b.Author);
                }
                else
                {
                    _bookView.DisplayBook(b.Isbn, b.Title);
                }

            }
        }
    }
}