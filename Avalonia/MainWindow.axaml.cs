using System;
using System.Collections.Generic;
using System.ComponentModel;
using Avalonia.Controls;
using GBReaderCremaL.Presenters.Events;
using GBReaderCremaL.Presenters.Routes;
using GBReaderCremaL.Presenters.Views;

namespace GBReaderCremaL.Avalonia
{

    public partial class MainWindow : Window, IMainView, IBrowseToViews
    {
        private readonly IDictionary<EnumViewsName, UserControl> _views;
        
        public MainWindow()
        {
            InitializeComponent();
            LocateControls();
            CanResize = false;
            _views = new Dictionary<EnumViewsName, UserControl>();

            Closing += Close;
        }
        
        private void LocateControls() {}

        public void AttachView(EnumViewsName name, UserControl view)
        { 
            _views[name] = view;
        }


        public void SetErrorMessage(string msg)
        {
            StackPanel messagePanel = new StackPanel();
            {
                TextBlock message = new TextBlock();
                message.Text = msg;
                messagePanel.Children.Add(message);
            }

            Content = messagePanel;
        }

        public void GoToView(EnumViewsName name)
        {
            Content = _views[name];
        }

        public void OnChangeView(object? sender, ChangeViewEventArgs args)
        {
            GoToView(args.Name);
            OnEnter?.Invoke(args.Name, args);
        }

        public void Close(object? sender, CancelEventArgs args)
        {
            OnClose?.Invoke(this, args);
        }
        
        public event EventHandler<CancelEventArgs>? OnClose;
        
        /*public void GoToStats()
        {
            IList<Session> sessions = _sessionRepo.GetSessions();
        
            GoToView(EnumViewsName.STATS);
            ViewStats(this, sessions);
        }

        public void GoHome(object? sender, EventArgs args)
        {
            GoToView(EnumViewsName.BOOK);
        }

        public void GoToRead(string isbn)
        {
            _mainView.GoToView(EnumViewsName.READ);
            StartSession(isbn);
        }*/
        public event EventHandler<ChangeViewEventArgs>? OnEnter;
    }
}