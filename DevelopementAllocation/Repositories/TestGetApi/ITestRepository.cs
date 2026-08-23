using DevelopementAllocation.Models;

namespace DevelopementAllocation.Repositories.TestGetApi
{
    public interface ITestRepository
    {
        Task<IEnumerable<TestUser>> GetTestUsersAsync();
    }
}