using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Data.Repositories.Base
{
    public class DbSession : IDisposable
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public DbSession(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
            _connection = new SqlConnection(_connectionString);
            _connection.Open(); // Abre a conexão assim que for criada
        }

        public SqlConnection Connection => _connection;

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close(); // Fecha a conexão quando a sessão for descartada
                _connection.Dispose();
                _connection = null;
            }
        }
    }
}