// Data/IDbConnectionFactory.cs
using System.Data;
using Npgsql;

namespace DevelopementAllocation.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }

    public class NpgsqlConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public NpgsqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("NeonDb")
                ?? throw new InvalidOperationException("Connection string 'NeonDb' not found.");
        }

        public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
    }
}