using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TemplateBase.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExemploController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok("Este é um exemplo de um controller puro.");
        }
    }
}
