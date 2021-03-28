using System.Data.SqlClient;

namespace EasyFoodOrder.Api.Restaurant.Migrations
{
    public static class Database
    {
        public static void EnsureDatabase(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            string targetDbName = builder.InitialCatalog;
            builder.InitialCatalog = "master";
            string query = $"IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = '{targetDbName}') CREATE DATABASE [{targetDbName}]";
            using var connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            using var cmd = new SqlCommand { CommandText = query, Connection = connection };
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}