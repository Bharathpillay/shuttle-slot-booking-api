using Dapper;
using DevelopementAllocation.Data;
using DevelopementAllocation.Models;

namespace DevelopementAllocation.Repository.ShuttleSlot
{
    public class ShuttleSlotRepository : IShuttleSlotRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public ShuttleSlotRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<ShuttleSlotEntry>> GetByDateAsync(DateTime date)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                SELECT id AS ""Id"", mobile_number AS ""MobileNumber"", name AS ""Name"",
                       mail_id AS ""MailId"", slot AS ""Slot"",
                       created_at AS ""CreatedAt"", updated_at AS ""UpdatedAt""
                FROM shuttle_slot_entry
                WHERE created_at::date = @Date::date";
            return await connection.QueryAsync<ShuttleSlotEntry>(sql, new { Date = date });
        }

        public async Task<int> CreateAsync(ShuttleSlotEntry entry, DateTime bookingDate)
        {
            using var connection = _connectionFactory.CreateConnection();
            const string sql = @"
                INSERT INTO shuttle_slot_entry (mobile_number, name, mail_id, slot, created_at, updated_at)
                VALUES (@MobileNumber, @Name, @MailId, @Slot, @BookingDate, @BookingDate)
                RETURNING id";
            return await connection.ExecuteScalarAsync<int>(sql, new
            {
                entry.MobileNumber,
                entry.Name,
                entry.MailId,
                entry.Slot,
                BookingDate = bookingDate
            });
        }
    }
}

