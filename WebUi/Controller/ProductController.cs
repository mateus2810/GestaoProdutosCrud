using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUi.Controller
{
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        // Rota GET: api/Exemplo
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Olá, mundo!");
        }
    }
}
