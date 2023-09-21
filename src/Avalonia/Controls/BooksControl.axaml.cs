using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderCremaL.Presenters.Events;

namespace GBReaderCremaL.Avalonia.Controls;

public partial class BooksControl : UserControl
{
    public interface IBookViewListener
    {
        void DetailsSelected(object? sender, DetailsEventArgs args);
        void StartReading(object? sender, string isbn);
    }
    
    private string? _isbn;
    private TextBlock? _txt;
    private Button? _button;
    private IBookViewListener? _listener;
    
    public BooksControl()
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
        _txt = this.FindControl<TextBlock>("Book");
        _button = this.FindControl<Button>("Button");
    }

    /**
     * Set a event listener
     */
    public void SetListener(IBookViewListener listener)
    {
        _listener = listener;
    }

    /**
     * Display the data of a book
     */
    public void SetBook(string isbn, params string[] items)
    {
        _isbn = isbn;
        _txt.Text += _isbn + "\n";
        foreach (string s in items)
        {
            _txt.Text += s + "\n";
        }

        _button.Content = "Commencer la lecture";
    }
    
    private void View_Details(object? sender, PointerPressedEventArgs args)
    {
        _listener.DetailsSelected(this, new DetailsEventArgs(_isbn));
    }

    private void Read(object? sender, RoutedEventArgs args)
    {
        _listener.StartReading(this, _isbn);
    }
    
}