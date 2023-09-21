using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace GBReaderCremaL.Avalonia.Controls;

public partial class ReadControl : UserControl
{

    public interface IReadViewListener
    {
        void MakeChoice(object? sender, int choice);
    }

    private IReadViewListener _listener;
    private TextBlock _txt;
    private Button _button;
    private int _choice;

    public ReadControl()
    {
        InitializeComponent();
        LocateControls();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void LocateControls()
    {
        _txt = this.FindControl<TextBlock>("Text");
        _button = this.FindControl<Button>("Choice");
    }
    
    public void SetListener(IReadViewListener listener)
    {
        _listener = listener;
    }
    
    private void SetButton(int dst)
    {
        _button.Content = "Allez à la page " + dst;
        _button.Click += MakeChoice;
    }
    
    public void SetContent(string txt, int dst)
    {
        _txt.Text = txt;
        SetButton(dst);
        SetChoice(dst);
    }

    private void MakeChoice(object? sender, RoutedEventArgs args)
    {
        _listener.MakeChoice(this, _choice);
    }
    
    private void SetChoice(int value)
    {
        _choice = value;
    }
    
}