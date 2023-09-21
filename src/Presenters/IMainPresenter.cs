using GBReaderCremaL.Domains;

namespace GBReaderCremaL.Presenters;

public interface IMainPresenter
{
    void SaveSession(Session session);
    void DestroySession(string isbn);
    
    Session GetSessionOf(string isbn);
    List<Session> RetrieveSessions();

    List<Book> SearchBook(string isbn);
    List<Book> RetrieveBooks();
    List<Page> GetPages(string isbn);
}