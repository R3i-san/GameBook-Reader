using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using GBReaderCremaL.Presenters.Events;
using GBReaderCremaL.Presenters.Views;

namespace GBReaderCremaL.Avalonia.Views;

public partial class StatsView : UserControl, IStatsView
{
    private WrapPanel _stats;
    
    public StatsView()
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
        _stats = this.FindControl<WrapPanel>("StatsPanel");
    }

    public void Clear()
    {
        _stats.Children.Clear();
    }

    public void DisplayCount(int count)
    {
        TextBlock txt = new TextBlock();
        {
            txt.Text = "Nombre de sessions en cours : " + count;
        }
        _stats.Children.Add(txt);
    }

    public void DisplayStats(params string[] items)
    {
        StackPanel stats = new StackPanel();
        {
            stats.Orientation = Orientation.Vertical;
            stats.Margin = Thickness.Parse("10");
        }
        
        foreach (string item in items)
        {
            TextBlock txt = new TextBlock();
            txt.Text += item + "\n";
            stats.Children.Add(txt);
        }
        
        _stats.Children.Add(stats);
    }

    public void GoHome(object? sender, RoutedEventArgs args)
    {
        OnGoHome?.Invoke(sender, new ChangeViewEventArgs(EnumViewsName.Book, null));
    }

    public event EventHandler<ChangeViewEventArgs>? OnGoHome;
}