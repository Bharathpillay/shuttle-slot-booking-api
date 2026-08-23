using DevelopementAllocation.Repositories.TestGetApi;
using Microsoft.AspNetCore.Mvc;

namespace DevelopementAllocation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ITestRepository _testRepository;

        public TestController(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _testRepository.GetTestUsersAsync();

            return Ok(result);
        }
    }
}