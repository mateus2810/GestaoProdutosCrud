using Application.Services.SupplierServices.GetSupllierService.Interface;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {
        private readonly IGetSupllierService _getSupllierService;

        public SupplierController(IGetSupllierService getSupllierService)
        {
            _getSupllierService = getSupllierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers()
        {
            try
            {
                var suppliers = await _getSupllierService.GetAllSupplier();
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os fornecedores: {ex.Message}");
            }
        }
    }
}

