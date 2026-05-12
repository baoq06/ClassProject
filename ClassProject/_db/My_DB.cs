using System.Configuration;
using Microsoft.Data.SqlClient;

namespace ClassProject._db
{
    public class My_DB
    {
        private readonly string _connStr = 
            ConfigurationManager.ConnectionStrings["MyDB"]?.ConnectionString 
            ?? throw new InvalidOperationException("Connection string 'MyDB' not found.");

        public SqlConnection GetConnection() => new SqlConnection(_connStr);
    }
}
