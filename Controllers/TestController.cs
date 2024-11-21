using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase {
        
        public TestController () {

        }

        [HttpGet("Test")]
        public string Test() {
            return "Hello World";
        }
    }
}