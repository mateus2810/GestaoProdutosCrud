using Application.Services.SupplierServices.CreateSupllierService.Interface;
using Application.Services.SupplierServices.GetSupllierService.Interface;
using Domain.Input;
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
        private readonly ICreateSupllierService _createSupllierService;

        public SupplierController(
            IGetSupllierService getSupllierService,
            ICreateSupllierService createSupllierService)
        {
            _getSupllierService = getSupllierService;
            _createSupllierService = createSupllierService;
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


        // Rota POST: /Supplier
        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierInput supplierInput)
        {
            if (supplierInput == null)
            {
                return BadRequest("Dados do fornecedor inválidos");
            }

            var supplier = await _createSupllierService.CreateSupplier(supplierInput);

            // Verifica se o ID retornado é válido
            if (supplier)
            {
                return Ok(supplier);
                //return CreatedAtRoute(nameof(GetSupplierById), new { id = supplierId }, null);
            }
            else
            {
                return StatusCode(500, "Erro ao criar o fornecedor");
            }
        }
    }
}

