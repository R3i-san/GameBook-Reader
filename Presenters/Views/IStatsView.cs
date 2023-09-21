using GBReaderCremaL.Presenters.Events;

namespace GBReaderCremaL.Presenters.Views;

public interface IStatsView
{
    public void Clear();

    public void DisplayCount(int count);
    
    public void DisplayStats(params string[] stats);

    event EventHandler<ChangeViewEventArgs>? OnGoHome;
}