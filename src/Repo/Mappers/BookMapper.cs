using GBReaderCremaL.Domains;
using GBReaderCremaL.Infrastructure.DTO;


namespace GBReaderCremaL.Repo.Mappers;

public class BookMapper
{

    public static List<Book> toBooks(List<DTOBook> books)
    {
        List<Book> newBooks = new List<Book>();
        foreach (DTOBook b in books)
        {
            newBooks.Add(toBook(b));
        }

        return newBooks;
    }
    
    public static Book toBook(DTOBook book)
    {
        return new Book(book.Isbn, book.Title, book.Summary, new Author(book.AuthorFname, book.AuthorLname));
    }
}