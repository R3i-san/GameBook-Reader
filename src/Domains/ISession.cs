using System.ComponentModel;

namespace GBReaderCremaL.Domains;

public interface ISession : INotifyPropertyChanged
{
    string Isbn { get; }

    int NumPage { get; set; }

    void UpdateValues(Session s);

}