using Application.Services.SupplierServices.DeleteSupllierService;
using Domain.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.SupplierService
{
    public class DeleteSupllierServiceTest
    {
        [Fact]
        public async Task DeleteSupplier_ReturnsTrue_WhenDeletionIsSuccessful()
        {
            // Arrange
            int supplierId = 1;

            var supplierRepositoryMock = new Mock<ISupplierRepository>();
            supplierRepositoryMock.Setup(repo => repo.DeleteSupplier(supplierId))
                                  .ReturnsAsync(true); 

            var deleteSupplierService = new DeleteSupllierService(supplierRepositoryMock.Object);

            // Act
            var result = await deleteSupplierService.DeleteSupplier(supplierId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteSupplier_ReturnsFalse_WhenDeletionFails()
        {
            // Arrange
            int supplierId = 1;

            var supplierRepositoryMock = new Mock<ISupplierRepository>();
            supplierRepositoryMock.Setup(repo => repo.DeleteSupplier(supplierId))
                                  .ReturnsAsync(false);

            var deleteSupplierService = new DeleteSupllierService(supplierRepositoryMock.Object);

            // Act
            var result = await deleteSupplierService.DeleteSupplier(supplierId);

            // Assert
            Assert.False(result);
        }
    }
}
