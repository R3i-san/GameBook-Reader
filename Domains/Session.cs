using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GBReaderCremaL.Domains;

public class Session : ISession
{
    private string _isbn;
    private int _numPage;
    private string _startTime;
    private string _editTime;

    public Session()
    {
        _isbn = "2000001010";
        _numPage = 1;
        _startTime = "01-01-70 00:00:00";
        _editTime = "01-01-70 00:00:00";
    }
    
    public Session(string isbn, int numPage, string start, string edit)
    {
        _isbn = isbn;
        _numPage = numPage;
        _startTime = start;
        _editTime = edit;
    }

    public void UpdateValues(Session s)
    {
        _isbn = s.Isbn;
        _numPage = s.NumPage;
        _startTime = s.StartTime;
        _editTime = s._editTime;
    }

    public string Isbn
    {
        get => _isbn;
    }

    public int NumPage
    {
        get => _numPage;
        set => SetField(ref _numPage, value);
    }

    private void SetNumPage(int value)
    {
        _numPage = value;
        OnPropertyChanged();
    }

    public string StartTime
    {
        get => _startTime;
    }

    public string EditTime
    {
        get => _editTime;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}