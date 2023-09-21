using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GBReaderCremaL.Avalonia.Views;
using GBReaderCremaL.Domains;
using GBReaderCremaL.Infrastructure;
using GBReaderCremaL.Infrastructure.Exceptions;
using GBReaderCremaL.Presenters;
using GBReaderCremaL.Presenters.Events;
using GBReaderCremaL.Repo;

namespace GBReaderCremaL.Avalonia
{
    public partial class App : Application
    {
        private MainWindow? _mainWindow;
        private IStorageRepo? _storage;
        private ISessionRepo? _session;
        
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                InitViewsAndPresenters();
                desktop.MainWindow = _mainWindow;
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void InitViewsAndPresenters()
        {
            
            _mainWindow = new MainWindow();
            
            try
            {

                GameBookStorageFactory storageFactory = new GameBookStorageFactory(DbConfig.ConnectionString); 
                SessionStorageFactory sessionFactory = new SessionStorageFactory(JsonConfig.PathString); 
                
                _storage = storageFactory.SetConnection();
                _session = sessionFactory.SetSessionStorage();
                
                var session = new Session();
                var shelf = new Shelf();

                MainPresenter mainPresenter = new MainPresenter(_session, _storage!, session, _mainWindow);

                var booksView = new BooksView();
                BookPresenter bookPresenter = new BookPresenter(booksView, shelf, _mainWindow, mainPresenter);

                var readView = new ReadView();
                ReadPresenter readPresenter = new ReadPresenter(readView, session, _mainWindow, mainPresenter);

                var statsView = new StatsView();
                StatsPresenter statsPresenter = new StatsPresenter(statsView, _mainWindow, mainPresenter);
            
                _mainWindow.AttachView(EnumViewsName.Book, booksView);
                _mainWindow.AttachView(EnumViewsName.Read, readView);
                _mainWindow.AttachView(EnumViewsName.Stats, statsView);

                _mainWindow.GoToView(EnumViewsName.Book);
            }
            catch (InitializeStorageException ise)
            {
                _mainWindow.SetErrorMessage(ise.Message);
            }
            
            

        }
        
    }
}