namespace GBReaderCremaL.Domains;

public class Page
{
    private int _numSrc;
    private string _txt;
    private List<Path> _dst;


    public Page(int numSrc, string txt, List<Path> dst)
    {
        _numSrc = numSrc;
        _txt = txt;
        _dst = dst;
    }
    
    public int NumSrc
    {
        get => _numSrc;
        set => _numSrc = value;
    }

    public string Txt
    {
        get => _txt;
        set => _txt = value ?? throw new ArgumentNullException(nameof(value));
    }

    public List<Path> Dst
    {
        get => _dst;
        set => _dst = value ?? throw new ArgumentNullException(nameof(value));
    }
    
}