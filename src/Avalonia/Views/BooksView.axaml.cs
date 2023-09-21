using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderCremaL.Avalonia.Controls;
using GBReaderCremaL.Presenters.Events;
using GBReaderCremaL.Presenters.Views;

namespace GBReaderCremaL.Avalonia.Views;

public partial class BooksView : UserControl, IBooksView, BooksControl.IBookViewListener
{
    private WrapPanel _books;
    private TextBlock _status;
    private TextBox _searchBar;
    
    public BooksView()
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
        _books = this.FindControl<WrapPanel>("Books");
        _status = this.FindControl<TextBlock>("Status");
        _searchBar = this.FindControl<TextBox>("SearchBar");
    }
    
    public void DetailsSelected(object? sender, DetailsEventArgs args)
    {
        _books.Children.Clear();
        OnDetailsSelected?.Invoke(this, args);
        /*_books.Children.Add(
            new TextBlock() { 
                TextWrapping = TextWrapping.Wrap,
            Background = Brush.Parse("pink")
        });*/
    }
    
    public void DisplayBook(string isbn, params string[] items)
    {
        var bookControl = new BooksControl();
    
        bookControl.SetBook(isbn, items);
        bookControl.SetListener(this);
        
        _books.Children.Add(bookControl);
    }

    public void Clear()
    {
        _books.Children.Clear();
    }
    
    public void Search(object? sender, RoutedEventArgs args)
    {
        OnSearchBook?.Invoke(sender, new BookViewEventArgs(_searchBar.Text));
    }

    public void SetMessage(string msg)
    {
        _status.Text = msg;
    }

    public void Stats(object? sender, RoutedEventArgs args)
    {
        OnStatsDisplayed?.Invoke(sender, new ChangeViewEventArgs(EnumViewsName.Stats,null));
    }
    
    public void StartReading(object? sender, string isbn)
    {
        OnStartReading?.Invoke(sender, new ChangeViewEventArgs(EnumViewsName.Read, isbn));
    }

    public void Reset(object? sender, RoutedEventArgs args)
    {
        Refresh?.Invoke(sender, args);
    }
    
    public event EventHandler? Refresh;

    public event EventHandler<DetailsEventArgs>? OnDetailsSelected;
    public event EventHandler<BookViewEventArgs>? OnSearchBook;
    public event EventHandler<ChangeViewEventArgs>? OnStartReading;
    public event EventHandler<ChangeViewEventArgs>? OnStatsDisplayed;
}
