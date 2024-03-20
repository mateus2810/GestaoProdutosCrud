using System;
using Application.Services.ProductServices.DeleteProductService;
using Domain.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.ProductService
{
    public class DeleteProductServiceTest
    {
        [Fact]
        public async Task DeleteProduct_ReturnsTrue_WhenProductIsDeletedSuccessfully()
        {
            // Arrange
            var productId = 1; // ID válido
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.DeleteProduct(productId))
                                 .ReturnsAsync(true);
            var deleteProductService = new DeleteProductService(productRepositoryMock.Object);

            // Act
            var result = await deleteProductService.DeleteProduct(productId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsFalse_WhenProductDeletionFails()
        {
            // Arrange
            var productId = 2; // ID válido, mas produto não encontrado para exclusão
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.DeleteProduct(productId))
                                 .ReturnsAsync(false);
            var deleteProductService = new DeleteProductService(productRepositoryMock.Object);

            // Act
            var result = await deleteProductService.DeleteProduct(productId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteProduct_ThrowsException_WhenUnexpectedErrorOccurs()
        {
            // Arrange
            var productId = 3; // ID válido
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.DeleteProduct(productId))
                                 .ThrowsAsync(new Exception("Unexpected error"));
            var deleteProductService = new DeleteProductService(productRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await deleteProductService.DeleteProduct(productId));
        }
    }
}
