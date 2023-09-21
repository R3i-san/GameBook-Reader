using System.Data;
using System.Data.Common;
using GBReaderCremaL.Infrastructure.Exceptions;
using MySql.Data.MySqlClient;


namespace GBReaderCremaL.Infrastructure;

public class GameBookStorageFactory
{
    private DbProviderFactory _factory;
    private String _connectionString;
    
    public GameBookStorageFactory(string connectionString)
    {
        try
        {
            _factory = MySqlClientFactory.Instance;
            _connectionString = connectionString;
        }
        catch (ArgumentException ex)
        {
            throw new Exception("Unable to load provider MySqlClient", ex);
        }
    }

    public GameBookStorage SetConnection()
    {
        try
        {
            IDbConnection con = _factory.CreateConnection()!;
            con.ConnectionString = _connectionString;
            con.Open();
            return new GameBookStorage(con);
        }
        catch (MySqlException mse)
        {
            throw new ConnectionException("Impossible de se connecter à la base de données", mse);
        }
    }
}