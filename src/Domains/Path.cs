namespace GBReaderCremaL.Domains;

public class Path
{
    private int _dst;
    private string _txt;

    public Path(int dst, string txt)
    {
        _dst = dst;
        _txt = txt;
    }

    public int Dst
    {
        get => _dst;
    }

    public string Txt
    {
        get => _txt;
    }
}