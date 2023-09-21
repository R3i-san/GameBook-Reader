using GBReaderCremaL.Domains;

namespace GBReaderCremaL.Repo;

public interface IStorageRepo
{
    List<Book> RetrieveBooks();

    List<Book> Search(string item);

    List<Page> GetPages(string isbn);

}