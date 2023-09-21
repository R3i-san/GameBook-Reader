using GBReaderCremaL.Domains;
using GBReaderCremaL.Presenters.Events;
using GBReaderCremaL.Presenters.Routes;
using GBReaderCremaL.Presenters.Views;

namespace GBReaderCremaL.Presenters;

public class StatsPresenter
{
    private IStatsView _statsView;
    private IBrowseToViews _router;
    private IMainPresenter _mainPresenter;
    
    public StatsPresenter(IStatsView view, IBrowseToViews router, IMainPresenter mainPresenter)
    {
        _statsView = view;
        _router = router;

        _mainPresenter = mainPresenter;
        
        _statsView.OnGoHome += _router.OnChangeView;
        _router.OnEnter += ViewStats;
    }

    public void ViewStats(object? sender, ChangeViewEventArgs args)
    {
        List<Session> sessions = _mainPresenter.RetrieveSessions();

        _statsView.Clear();
        _statsView.DisplayCount(sessions.Count);
        
        foreach (Session s in sessions)
        {
            _statsView.DisplayStats(
                "ISBN : " + s.Isbn, 
                "Dernière page lue : Page n°" + s.NumPage, 
                "Première session : " + s.StartTime, 
                "Dernière session : " + s.EditTime);
        }
    }
    
    
}