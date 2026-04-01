using Npgsql;
using System.Data;

namespace PayTollCardApi.Core.Services
{
    public class SqlService
    {
        private readonly string _connectionString;

        public SqlService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable ExecuteQuery(string query)
        {
            var dataTable = new DataTable();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand(query, connection))
                using (var adapter = new NpgsqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }
    }
}
