namespace GBReaderCremaL.Infrastructure.DTO;

public class DTOPage
{
    private int _numSrc;
    private string _txt;
    private List<DTOPath>? _dst;
    
    public DTOPage(int numSrc, string txt)
    {
        _numSrc = numSrc;
        _txt = txt;
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

    public List<DTOPath> Dst
    {
        get => _dst;
        set => _dst = value ?? throw new ArgumentNullException(nameof(value));
    }
}