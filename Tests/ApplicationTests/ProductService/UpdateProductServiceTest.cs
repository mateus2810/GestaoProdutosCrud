using Application.Services.ProductServices.UpdateProductService;
using Domain.Input;
using Domain.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.ProductService
{
    public class UpdateProductServiceTest
    {
        [Fact]
        public async Task UpdateProduct_ReturnsSuccess_WhenProductIsUpdatedSuccessfully()
        {
            // Arrange
            var id = 1;
            var productInput = new ProductInput
            {
                Descricao = "Produto atualizado",
                Codigo = "12345",
                Situacao = true,
                DataFabricacao = DateTime.Now.AddDays(-10),
                DataValidade = DateTime.Now.AddDays(20),
                FornecedorId = 1
            };

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.UpdateProduct(id, productInput))
                                 .ReturnsAsync(true);

            var productService = new UpdateProductService(productRepositoryMock.Object);

            // Act
            var result = await productService.UpdateProduct(id, productInput);

            // Assert
            Assert.True(result.Success);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsFailure_WhenValidationFails()
        {
            // Arrange
            var id = 1;
            var invalidProductInput = new ProductInput
            {
                Descricao = "", // Descrição vazia para falhar na validação
                Codigo = "12345",
                Situacao = true,
                DataFabricacao = DateTime.Now.AddDays(-10),
                DataValidade = DateTime.Now.AddDays(20),
                FornecedorId = 1
            };

            var productService = new UpdateProductService(null);

            // Act
            var result = await productService.UpdateProduct(id, invalidProductInput);

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(result.ErrorMessage);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsFailure_WhenRepositoryThrowsException()
        {
            // Arrange
            var id = 1;
            var productInput = new ProductInput
            {
                Descricao = "Produto atualizado",
                Codigo = "12345",
                Situacao = true,
                DataFabricacao = DateTime.Now.AddDays(-10),
                DataValidade = DateTime.Now.AddDays(20),
                FornecedorId = 1
            };

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.UpdateProduct(id, productInput))
                                 .ThrowsAsync(new Exception("Simulated repository exception"));

            var productService = new UpdateProductService(productRepositoryMock.Object);

            // Act
            var result = await productService.UpdateProduct(id, productInput);

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(result.ErrorMessage);
            Assert.Contains("Simulated repository exception", result.ErrorMessage);
        }
    }
}
