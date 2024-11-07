using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace ShortcutService.Services
{
    public class SqlService
    {
        private readonly string _connectionString;

        public SqlService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public DataTable ExecuteQuery(string query)
        {
            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand(query, connection);

            connection.Open();

            var dataTable = new DataTable();
            using var reader = command.ExecuteReader();

            dataTable.Load(reader);

            return dataTable;
        }
    }
}
