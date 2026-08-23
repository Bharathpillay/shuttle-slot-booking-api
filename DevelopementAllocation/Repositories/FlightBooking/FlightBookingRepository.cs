// Repository/FlightBooking/FlightBookingRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using DevelopementAllocation.Data;
using DevelopementAllocation.Models;

namespace DevelopementAllocation.Repository.FlightBooking
{
    public class FlightBookingRepository : IFreightBookingRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public FlightBookingRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<TestUser>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = "SELECT * FROM test_users";
            return await connection.QueryAsync<TestUser>(sql);
        }
    }
}