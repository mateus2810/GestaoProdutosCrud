using Application.Services.CreateProductService;
using Domain.Input;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.ProductService
{
    public class CreateProductServiceTest
    {
        [Fact]
        public async Task CreateProduct_ReturnsSuccess_WhenProductIsCreatedSuccessfully()
        {
            // Arrange
            var productInput = new ProductInput
            {
                Descricao = "Descrição do produto",
                Codigo = "12345",
                Situacao = true,
                DataFabricacao = DateTime.Now,
                DataValidade = DateTime.Now.AddDays(30),
                FornecedorId = 6 // ID válido
            };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.CreateProduct(productInput))
                                 .ReturnsAsync(true);
            var productService = new CreateProductServices(productRepositoryMock.Object);

            // Act
            var result = await productService.CreateProduct(productInput);

            // Assert
            Assert.True(result.Success);
            Assert.Null(result.ErrorMessage);
        }

        [Fact]
        public async Task CreateProduct_ReturnsFailure_WhenInputValidationFails()
        {
            // Arrange
            // Arrange
            var invalidProductInput = new ProductInput
            {
                Descricao = "Descrição do produto falha",
                Codigo = "COD11",
                Situacao = true,
                DataFabricacao = DateTime.Now.AddDays(2),
                DataValidade = DateTime.Now,
                FornecedorId = 7 // ID válido
            };
            var productService = new CreateProductServices(null);

            // Act
            var result = await productService.CreateProduct(invalidProductInput);

            // Assert
            Assert.False(result.Success);
            Assert.NotNull(result.ErrorMessage);
            Assert.Contains("A data de fabricação deve ser anterior à data de validade.", result.ErrorMessage);
        }
    }
}
