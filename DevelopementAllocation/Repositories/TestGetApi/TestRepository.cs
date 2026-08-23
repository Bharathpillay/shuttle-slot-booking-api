using Dapper;
using Npgsql;
using DevelopementAllocation.Models;
using DevelopementAllocation.Repositories.TestGetApi;

namespace DevelopementAllocation.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly string _connectionString;

        public TestRepository(IConfiguration configuration)
        {
            var username = configuration["DB_USERNAME"];
            var password = configuration["DB_PASSWORD"];
            var host = configuration["DB_HOST"];
            var database = configuration["DB_NAME"];
            var port = configuration["DB_PORT"];

            _connectionString =
                $"Host={host};" +
                $"Port={port};" +
                $"Database={database};" +
                $"Username={username};" +
                $"Password={password};" +
                $"SSL Mode=Require;" +
                $"Channel Binding=Require;";
        }

        public async Task<IEnumerable<TestUser>> GetTestUsersAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);

            const string sql = @"
                SELECT 
                    id,
                    name,
                    email,
                    created_at AS Created_At
                FROM test_users
                ORDER BY id;
            ";

            return await connection.QueryAsync<TestUser>(sql);
        }
    }
}