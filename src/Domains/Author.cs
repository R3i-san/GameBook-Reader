namespace GBReaderCremaL.Domains;

public class Author
{
    private String _fname;
    private String _lname;
    
    public Author(String fname, String lname)
    {
        _fname = fname;
        _lname = lname;
    }
    

    public string Fname
    {
        get => _fname;
        set => _fname = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Lname
    {
        get => _lname;
        set => _lname = value ?? throw new ArgumentNullException(nameof(value));
    }
}