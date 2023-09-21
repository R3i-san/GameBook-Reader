namespace GBReaderCremaL.Infrastructure.DTO;

public class DTOPath
{
    private int _dst;
    private string _txt;

    public DTOPath(int dst, string txt)
    {
        _dst = dst;
        _txt = txt;
    }

    public int Dst
    {
        get => _dst;
        set => _dst = value;
    }

    public string Txt
    {
        get => _txt;
        set => _txt = value ?? throw new ArgumentNullException(nameof(value));
    }
}