using GBReaderCremaL.Presenters.Events;
namespace GBReaderCremaL.Presenters.Routes;

public interface IBrowseToViews
{

    //void GoToView(object? sender, ChangeViewEventArgs args);
    
    /*void GoToStats();

    void GoHome(object? sender, EventArgs args);

    void GoToRead(string isbn);*/
    
    public void OnChangeView(object? sender, ChangeViewEventArgs args);
    
    event EventHandler<ChangeViewEventArgs> OnEnter;


    /*event EventHandler<Session> OnStartSession;

    event EventHandler<IList<Session>> OnViewStats;*/


}