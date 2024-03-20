using Application.Services.SupplierServices.UpdateSupllierService;
using Domain.Input;
using Domain.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.SupplierService
{
    public class UpdateSupllierServiceTest
    {
        [Fact]
        public async Task UpdateSupplier_ReturnsTrue_WhenSupplierIsUpdatedSuccessfully()
        {
            // Arrange
            int supplierId = 1;
            var supplierInput = new SupplierInput
            {
                Codigo = "001",
                Descricao = "Fornecedor 1",
                CNPJ = "123456789",
                Nome = "Fornecedor 1"
            };

            var supplierRepositoryMock = new Mock<ISupplierRepository>();
            supplierRepositoryMock.Setup(repo => repo.UpdateSupplier(supplierId, supplierInput))
                                  .ReturnsAsync(true); 

            var updateSupplierService = new UpdateSupllierService(supplierRepositoryMock.Object);

            // Act
            var result = await updateSupplierService.UpdateSupplier(supplierId, supplierInput);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateSupplier_ReturnsFalse_WhenUpdateFails()
        {
            // Arrange
            int supplierId = 1;
            var supplierInput = new SupplierInput
            {
                Codigo = "001",
                Descricao = "Fornecedor 1",
                CNPJ = "123456789",
                Nome = "Fornecedor 1"
            };

            var supplierRepositoryMock = new Mock<ISupplierRepository>();
            supplierRepositoryMock.Setup(repo => repo.UpdateSupplier(supplierId, supplierInput))
                                  .ReturnsAsync(false);

            var updateSupplierService = new UpdateSupllierService(supplierRepositoryMock.Object);

            // Act
            var result = await updateSupplierService.UpdateSupplier(supplierId, supplierInput);

            // Assert
            Assert.False(result);
        }
    }
}
