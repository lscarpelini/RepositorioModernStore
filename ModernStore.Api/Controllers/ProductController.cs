using Microsoft.AspNetCore.Mvc;

namespace ModernStore.Api.Controllers
{
    public class ProductController : Controller
    {
        [HttpGet]
        [Route("Products")]
        public IActionResult Get()
        {
            return Ok($"Listando todos os produtos");
        }

        [HttpGet]
        [Route("Products/{number}")] // Rota com parametro
        public IActionResult Get(guid id)
        {
            return Ok($"Produto - {id}"); 
        }

        [HttpPost]
        [Route("Products")] // Rora com parametro
        public IActionResult Post()
        {
            return Ok($"Criando um novo produto");
        }


    }
}
