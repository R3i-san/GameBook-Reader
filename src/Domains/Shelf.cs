using System.Collections.Specialized;

namespace GBReaderCremaL.Domains;

public class Shelf : IShelf
{
    private IList<Book> _books;

    public Shelf()
    {
        _books = new List<Book>();
    }

    public int Size()
    {
        return _books.Count;
    }

    public bool IsEmpty()
    {
        return Size() == 0;
    }

    public void Clear()
    {
        _books.Clear();
    }


    public List<Book> Books
    {
        get => (List<Book>)_books;
        set => AddBook(value);
    }


    public Book GetBook(string isbn)
    {
        foreach (Book b in _books)
        {
            if (b.Isbn == isbn)
            {
                return b;
            }
        }

        return new Book("sample", "sample", "sample", new Author("sample", "sample"));
    }

    private void AddBook(List<Book> value)
    {
        _books = value;
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
}