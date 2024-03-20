using Application.Services.SupplierServices.CreateSupllierService.Interface;
using Application.Services.SupplierServices.DeleteSupllierService.Interface;
using Application.Services.SupplierServices.GetSupllierService.Interface;
using Application.Services.SupplierServices.UpdateSupllierService.Interface;
using Domain.Input;
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
        private readonly IUpdateSupllierService _updateSupllierService;
        private readonly IDeleteSupplierService _deleteSupplierService;

        public SupplierController(
            IGetSupllierService getSupllierService,
            ICreateSupllierService createSupllierService,
            IUpdateSupllierService updateSupllierService,
            IDeleteSupplierService deleteSupplierService)
        {
            _getSupllierService = getSupllierService;
            _createSupllierService = createSupllierService;
            _updateSupllierService = updateSupllierService;
            _deleteSupplierService = deleteSupplierService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSuppliers(int pageNumber, int pageSize)
        {
            try
            {
                var suppliers = await _getSupllierService.GetAllSupplier(pageNumber, pageSize);
                return Ok(suppliers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter os fornecedores: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] SupplierInput supplierInput)
        {
            if (supplierInput == null)
            {
                return BadRequest("Dados do fornecedor inválidos");
            }

            var supplier = await _createSupllierService.CreateSupplier(supplierInput);

            if (supplier)
            {
                return Ok(supplier);
            }
            else
            {
                return StatusCode(500, "Erro ao criar o fornecedor");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] SupplierInput supplierInput)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            bool success = await _updateSupllierService.UpdateSupplier(id, supplierInput);

            if (success)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            var success = await _deleteSupplierService.DeleteSupplier(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}


