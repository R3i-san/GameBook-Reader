namespace GBReaderCremaL.Infrastructure.DTO;

public class DTOBook
{
    private string _title;
    private string _isbn;
    private string _summary;
    private DTOEditor _author;

    public DTOBook(string isbn, string title, string summary, DTOEditor author)
    {
        _title = title;
        _isbn = isbn;
        _summary = summary;
        _author = author;
    }

    public string Isbn => _isbn;
    public string Title => _title;


    public string Summary => _summary;
    
    public string AuthorFname => _author.Fname;
    
    public string AuthorLname => _author.Lname;
}

