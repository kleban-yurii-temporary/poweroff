using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PowerOff.Core;
using PowerOff.Core.Initializer;

namespace PowerOff.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitDataController : ControllerBase
    {
        private readonly DataInitializer _dbInitializer;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InitDataController(DataInitializer dbInitializer, 
            IWebHostEnvironment webHostEnvironment)
        {
            _dbInitializer = dbInitializer;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<KeyValuePair<string, int>>> Start()
        {
            var dataFilePath = Path.Combine(_webHostEnvironment.WebRootPath, "data", "init_data.json");
            return await _dbInitializer.InitAsync(dataFilePath);
        }
    }
}
