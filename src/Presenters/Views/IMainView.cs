using System.ComponentModel;
using GBReaderCremaL.Presenters.Events;

namespace GBReaderCremaL.Presenters.Views;

public interface IMainView
{
    void GoToView(EnumViewsName name);

    public void SetErrorMessage(string msg);
    
    public event EventHandler<CancelEventArgs>? OnClose;

}