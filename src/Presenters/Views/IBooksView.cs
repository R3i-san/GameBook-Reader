using GBReaderCremaL.Presenters.Events;

namespace GBReaderCremaL.Presenters.Views;

public interface IBooksView
{
    void Clear();
    
    void DisplayBook(string isbn, params string[] items);
    
    void SetMessage(string msg);
    
    public event EventHandler? Refresh;
    public event EventHandler<DetailsEventArgs>? OnDetailsSelected;
    public event EventHandler<ChangeViewEventArgs>? OnStatsDisplayed;
    public event EventHandler<ChangeViewEventArgs>? OnStartReading;
    public event EventHandler<BookViewEventArgs>? OnSearchBook;
    
}