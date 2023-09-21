using GBReaderCremaL.Domains;
using GBReaderCremaL.Presenters.Events;

namespace GBReaderCremaL.Presenters.Views;

public interface IReadView
{
    void DisplayPageInfos(string txt, int src);
    
    void DisplayChoice(string txt, int dst);

    void DisplayReset();

    void Clear();

    void SetStatus(string msg);

    event EventHandler<ChangePageEventArgs> OnMakeChoice;
    event EventHandler<ChangeViewEventArgs> OnGoHome;
    event EventHandler OnRetry;
    event EventHandler OnQuit;

}