using System.Data;
using GBReaderCremaL.Domains;
using GBReaderCremaL.Infrastructure.DTO;
using GBReaderCremaL.Infrastructure.Exceptions;
using GBReaderCremaL.Repo;
using GBReaderCremaL.Repo.Mappers;
using MySql.Data.MySqlClient;

namespace GBReaderCremaL.Infrastructure;

public class GameBookStorage : IDisposable, IStorageRepo
{
        private readonly IDbConnection _con;

        public GameBookStorage(IDbConnection con)
        {
            this._con = con;
        }

        public List<Page> GetPages(string isbn)
        {
            List<DTOPage> res = new List<DTOPage>();
            if(_con.State == ConnectionState.Closed)_con.Open();

            try
            {
                const string query = "SELECT pageNum, texte FROM page WHERE isbn = @isbn ORDER BY pageNum ASC";
                using (var cmd = _con.CreateCommand())
                {
                    cmd.CommandText = query;
                    var isbnParam = cmd.CreateParameter();
                    isbnParam.ParameterName = "@isbn";
                    isbnParam.Value = isbn;
                    isbnParam.DbType = DbType.String;
                    cmd.Parameters.Add(isbnParam);

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res.Add(new DTOPage(reader.GetInt32(0), reader.GetString(1)));
                        }
                    }
                    
                    foreach (DTOPage p in res)
                    {
                        p.Dst = GetPaths(isbn, p.NumSrc);
                    }
                    
                    return PageMapper.ToPages(res);
                }
            }
            catch (MySqlException)
            {
                throw new ResourceLoadingException("Impossible de récupérer les pages du livre");
            } finally

            {
                _con.Close();
            }
        }

        private List<DTOPath> GetPaths(string isbn, int pageNum)
        {
            List<DTOPath> res = new List<DTOPath>();
            if (_con.State == ConnectionState.Closed) _con.Open();
                
            const string query = 
                    "SELECT (SELECT p.pageNum FROM page p JOIN path pa ON pa.pIdTo = p.pId WHERE pat.pathId = pa.pathId) as pageNum, " +
                    "pat.texte FROM page pag " +
                    "JOIN path pat ON pat.pIdFrom = pag.pId " +
                    "WHERE pag.isbn = @isbn AND pag.pageNum = @pageNum";
            try
            {
                using var cmd = _con.CreateCommand();
                cmd.CommandText = query;
                var isbnParam = cmd.CreateParameter();
                isbnParam.ParameterName = "@isbn";
                isbnParam.Value = isbn;
                isbnParam.DbType = DbType.String;
                cmd.Parameters.Add(isbnParam);
                    
                var pageNumParam = cmd.CreateParameter();
                pageNumParam.ParameterName = "@pageNum";
                pageNumParam.Value = pageNum;
                pageNumParam.DbType = DbType.Int32;
                cmd.Parameters.Add(pageNumParam);
                    
                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(new DTOPath(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
                
                return res;

            }
            catch (MySqlException)
            {
                throw new ResourceLoadingException("Impossible de récupérer les pages du livre");
            } 
            finally
            {
                _con.Close();
            }
            
            
        }
        

        public List<Book> Search(string item)
        {
            List<DTOBook> res = new List<DTOBook>();
            if(_con.State == ConnectionState.Closed)_con.Open();
            
            const string query = "SELECT b.isbn, b.title, b.summary, a.firstname, a.lastname FROM books b " +
                                   "JOIN author a ON a.aId = b.aId " +
                                   "WHERE b.published = 1 " +
                                   "AND (b.isbn = @isbn OR " +
                                   "b.title LIKE @item OR " +
                                   "b.summary LIKE @item OR " +
                                   "a.firstname LIKE @item OR " +
                                   "a.lastname LIKE @item)";
            try
            {
                using var cmd = _con.CreateCommand();
                cmd.CommandText = query;
                var isbnParam = cmd.CreateParameter();
                isbnParam.ParameterName = "@isbn";
                isbnParam.Value = item;
                isbnParam.DbType = DbType.String;
                cmd.Parameters.Add(isbnParam);

                cmd.CommandText = query;
                var itemParam = cmd.CreateParameter();
                itemParam.ParameterName = "@item";
                itemParam.Value = "%" + item + "%";
                itemParam.DbType = DbType.String;
                cmd.Parameters.Add(itemParam);

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(new DTOBook(reader.GetString(0), reader.GetString(1), reader.GetString(2),
                            new DTOEditor(reader.GetString(3), reader.GetString(4))));
                    }
                }
                
                return BookMapper.toBooks(res);
            }
            catch (MySqlException)
            {
                throw new ResourceLoadingException("Erreur lors de la recherche de livre");
            }
            finally
            {
                _con.Close();
            }
        }

        public List<Book> RetrieveBooks()
        {
            List<DTOBook> dtoBook = new List<DTOBook>();

            if(_con.State == ConnectionState.Closed)_con.Open();
            using (IDbCommand cmd = _con.CreateCommand())
            {
                cmd.CommandText = "SELECT b.isbn, b.title, b.summary, a.firstname, a.lastname FROM books b " +
                                  "JOIN author a ON a.aId = b.aId " +
                                  "WHERE b.published = 1";

                using (IDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        dtoBook.Add(
                            new DTOBook(reader.GetString(0), reader.GetString(1), reader.GetString(2),
                                new DTOEditor(reader.GetString(3), reader.GetString(4)))
                        );
                    }
                }
            }
            _con.Close();
            return BookMapper.toBooks(dtoBook);
        }

        public void Dispose()
        {
            _con.Dispose();
        }
}