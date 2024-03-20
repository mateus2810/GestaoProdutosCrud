using Application.Services.SupplierServices;
using Application.Services.SupplierServices.CreateSupllierService;
using Domain.Input;
using Domain.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.SupplierService
{
    public class CreateSupllierServiceTest
    {
        [Fact]
        public async Task CreateSupplier_ReturnsTrue_WhenSupplierIsCreatedSuccessfully()
        {
            // Arrange
            var supplierInput = new SupplierInput
            {
                Codigo = "123",
                Descricao = "Fornecedor Teste",
                CNPJ = "123456789",
                Nome = "Fornecedor Teste"
                //...
            };

            var supplierRepositoryMock = new Mock<ISupplierRepository>();
            supplierRepositoryMock.Setup(repo => repo.CreateSupplier(supplierInput))
                                  .ReturnsAsync(true);

            var supplierService = new CreateSupllierService(supplierRepositoryMock.Object);

            // Act
            var result = await supplierService.CreateSupplier(supplierInput);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CreateSupplier_ReturnsFalse_WhenSupplierCreationFails()
        {
            // Arrange
            var supplierInput = new SupplierInput
            {
                Codigo = "123",
                Descricao = "Fornecedor Teste",
                CNPJ = "123456789",
                Nome = "Fornecedor Teste"
            };

            var supplierRepositoryMock = new Mock<ISupplierRepository>();
            supplierRepositoryMock.Setup(repo => repo.CreateSupplier(supplierInput))
                                  .ReturnsAsync(false);

            var supplierService = new CreateSupllierService(supplierRepositoryMock.Object);

            // Act
            var result = await supplierService.CreateSupplier(supplierInput);

            // Assert
            Assert.False(result); 
        }
    }
}

