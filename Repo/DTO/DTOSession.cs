namespace GBReaderCremaL.Infrastructure.DTO;

public class DTOSession
{
    private string _isbn;
    private int _numPage;
    private string _startTime;
    private string _editTime;

    /*public DTOSession(string isbn, int numPage, string start)
    {
        _isbn = isbn;
        _numPage = numPage;
        _startTime = start;
    }*/
    
    public DTOSession(string isbn, int numPage, string edit, string start)
    {
        _isbn = isbn;
        _numPage = numPage;
        _startTime = start;
        _editTime = edit;
    }
    

    public string Isbn
    {
        get => _isbn;
        set => _isbn = value ?? throw new ArgumentNullException(nameof(value));
    }

    public int NumPage
    {
        get => _numPage;
        set => _numPage = value;
    }

    public string StartTime
    {
        get => _startTime;
        set => _startTime = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string EditTime
    {
        get => _editTime;
        set => _editTime = value ?? throw new ArgumentNullException(nameof(value));
    }
}