namespace GBReaderCremaL.Infrastructure.DTO;

public class DTOEditor
{
    private string _fname;
    private string _lname;

    public DTOEditor(string fname, string lname)
    {
        _fname = fname;
        _lname = lname;
    }

    public string Lname => _lname;

    public string Fname => _fname;
}