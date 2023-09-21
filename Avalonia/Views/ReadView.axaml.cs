using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GBReaderCremaL.Avalonia.Controls;
using GBReaderCremaL.Presenters.Events;
using GBReaderCremaL.Presenters.Views;

namespace GBReaderCremaL.Avalonia.Views;

public partial class ReadView : UserControl, IReadView, ReadControl.IReadViewListener
{
    private TextBlock _text;
    private Button _reset;
    private WrapPanel _choices;

    public ReadView()
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
        _text = this.FindControl<TextBlock>("PageText");
        _choices = this.FindControl<WrapPanel>("Choices");
        _reset = this.FindControl<Button>("Reset");
    }

    public void Clear()
    {
        _choices.Children.Clear();
    }

    public void SetStatus(string msg)
    {
        _text.Text = msg;
    }

    public void DisplayPageInfos(string txt, int src)
    {
        _text.Text = $"Page {src} : \n" + txt;
    }
    
    public void DisplayChoice(string txt, int dst)
    { 
        var readControl = new ReadControl();

        readControl.SetContent(txt, dst);
        readControl.SetListener(this);

        _choices.Children.Add(readControl);
    }

    public void DisplayReset()
    {
        _reset.IsVisible = true;
    }

    public void  HideReset()
    {
        _reset.IsVisible = false;
    }
    

    private void GoHome(object? sender, RoutedEventArgs args)
    {
        HideReset();
        OnQuit?.Invoke(this, args);
        OnGoHome?.Invoke(this, new ChangeViewEventArgs(EnumViewsName.Book, null));
    }
    
    private void Retry(object? sender, RoutedEventArgs args)
    {
        HideReset();
        OnRetry?.Invoke(this, args);
    }

    public void MakeChoice(object? sender, int choice)
    {
        OnMakeChoice?.Invoke(sender, new ChangePageEventArgs(choice));
    }

    public event EventHandler<ChangePageEventArgs>? OnMakeChoice;
    public event EventHandler<ChangeViewEventArgs>? OnGoHome;
    public event EventHandler? OnRetry;
    public event EventHandler? OnQuit;
}