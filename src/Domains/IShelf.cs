using System.Collections.Specialized;

namespace GBReaderCremaL.Domains;

public interface IShelf : INotifyCollectionChanged
{
    List<Book> Books { get; set; }

    Book GetBook(string isbn);
    
    int Size();

    bool IsEmpty();
    
    void Clear();
    
    

}