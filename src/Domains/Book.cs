namespace GBReaderCremaL.Domains;

public class Book
{

    private string _isbn;
    private string _title;
    private string _summary;
    private Author _author;
    private IList<Page> _pages;

    public Book(string isbn, string title, string summary, Author author)
    {
        _isbn = isbn;
        _title = title;
        _summary = summary;
        _author = author;
        _pages = new List<Page>();
    }
    
    public Book(string isbn, string title, string summary, Author author, List<Page> pages)
    {
        _isbn = isbn;
        _title = title;
        _summary = summary;
        _author = author;
        _pages = new List<Page>(pages);
    }

    public void AddPages(List<Page> pages)
    {
       ((List<Page>)_pages).AddRange(pages);
    }

    public bool isLastPage(int pageIndex)
    {
        return _pages.Last() == GetPage(pageIndex);
    }
    
    public string AsString()
    {
        return Isbn + "\n" +
               Title + "\n" +
               Summary + "\n";
    }
    
    public string Isbn
    {
        get => _isbn;
    }
    
    public string Title
    {
        get => _title;
    }

    public string Summary
    {
        get => _summary;
    }

    public string Author
    {
        get => AuthorFname + " " + AuthorLname;
    }
    
    public string AuthorFname
    {
        get => _author.Fname;
    }
    public string AuthorLname
    {
        get => _author.Lname;
    }

    public List<Page> Pages
    {
        get => (List<Page>)_pages;
    }

    public Page GetPage(int pageIndex)
    {
        return _pages[pageIndex];
    }
}