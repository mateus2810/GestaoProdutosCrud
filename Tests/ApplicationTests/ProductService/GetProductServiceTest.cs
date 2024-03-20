using Application.Services.ProductServices.GetProductService;
using AutoMapper;
using Domain.DTO;
using Domain.Interfaces;
using Domain.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.ProductService
{
    public class GetProductServiceTest
    {

        [Fact]
        public async Task GetAllProduct_ReturnsListOfProductDTO_WhenSuccess()
        {
            // Arrange
            var products = new List<ProductModel>
                {
                    new ProductModel
                    {
                        Id = 1,
                        Descricao = "Produto 1",
                        Codigo ="COD1003",
                        Situacao = true,
                        DataFabricacao = DateTime.Now.AddDays(-10),
                        DataValidade = DateTime.Now.AddDays(20)
                    },
                    new ProductModel
                    {
                        Id = 2,
                        Descricao = "Produto 2",
                        Codigo = "COD1002",
                        Situacao = true,
                        DataFabricacao = DateTime.Now.AddDays(-5),
                        DataValidade = DateTime.Now.AddDays(25)
                    }
                };
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetAllProduct())
                                 .ReturnsAsync(products);
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ProductDTO>>(products))
                      .Returns(products.Select(product => new ProductDTO
                      {
                          Id = product.Id,
                          Descricao = product.Descricao,
                          Codigo = product.Codigo,
                          Situacao = product.Situacao,
                          DataFabricacao = product.DataFabricacao,
                          DataValidade = product.DataValidade
                      }).ToList());
            var productService = new GetProductService(productRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await productService.GetAllProduct();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetAllProduct_ReturnsEmptyList_WhenNoProductsAvailable()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetAllProduct())
                                 .ReturnsAsync(new List<ProductModel>());
            var mapperMock = new Mock<IMapper>();
            var productService = new GetProductService(productRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await productService.GetAllProduct();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllProduct_ThrowsException_WhenUnexpectedErrorOccurs()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetAllProduct())
                                 .ThrowsAsync(new Exception("Unexpected error"));
            var mapperMock = new Mock<IMapper>();
            var productService = new GetProductService(productRepositoryMock.Object, mapperMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(async () => await productService.GetAllProduct());
        }


        [Fact]
        public async Task GetProductByCode_ReturnsProductsSuccessfully()
        {
            // Arrange
            var codigo = "12345"; // Código válido
            var expectedProducts = new List<ProductSupplierModel>
    {
        new ProductSupplierModel { ProductId = 1, ProductDescription = "Produto 1", ProductCode = "12345", Situation = true, ManufactureDate = DateTime.Now, ExpiryDate = DateTime.Now.AddDays(10), SupplierId = 1, SupplierCode = "001", SupplierDescription = "Fornecedor 1", CNPJ = "123456789", Name = "Fornecedor 1" },
        new ProductSupplierModel { ProductId = 2, ProductDescription = "Produto 2", ProductCode = "54321", Situation = true, ManufactureDate = DateTime.Now, ExpiryDate = DateTime.Now.AddDays(5), SupplierId = 2, SupplierCode = "002", SupplierDescription = "Fornecedor 2", CNPJ = "987654321", Name = "Fornecedor 2" }
    };

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetProductByCode(codigo))
                                 .ReturnsAsync(expectedProducts);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<ProductSupplierDTO>>(It.IsAny<List<ProductSupplierModel>>()))
                      .Returns((List<ProductSupplierModel> products) => products.Select(p => new ProductSupplierDTO
                      {
                          ProductId = p.ProductId,
                          ProductDescription = p.ProductDescription,
                          ProductCode = p.ProductCode,
                          Situation = p.Situation,
                          ManufactureDate = p.ManufactureDate,
                          ExpiryDate = p.ExpiryDate,
                          SupplierId = p.SupplierId,
                          SupplierCode = p.SupplierCode,
                          SupplierDescription = p.SupplierDescription,
                          CNPJ = p.CNPJ,
                          Name = p.Name
                      }).ToList());

            var productService = new GetProductService(productRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await productService.GetProductByCode(codigo);

            // Assert
            Assert.Equal(expectedProducts.Count, result.Count);
            for (int i = 0; i < expectedProducts.Count; i++)
            {
                Assert.Equal(expectedProducts[i].ProductId, result[i].ProductId);
                Assert.Equal(expectedProducts[i].ProductDescription, result[i].ProductDescription);
                Assert.Equal(expectedProducts[i].ProductCode, result[i].ProductCode);
                Assert.Equal(expectedProducts[i].Situation, result[i].Situation);
                Assert.Equal(expectedProducts[i].ManufactureDate, result[i].ManufactureDate);
                Assert.Equal(expectedProducts[i].ExpiryDate, result[i].ExpiryDate);
                Assert.Equal(expectedProducts[i].SupplierId, result[i].SupplierId);
                Assert.Equal(expectedProducts[i].SupplierCode, result[i].SupplierCode);
                Assert.Equal(expectedProducts[i].SupplierDescription, result[i].SupplierDescription);
                Assert.Equal(expectedProducts[i].CNPJ, result[i].CNPJ);
                Assert.Equal(expectedProducts[i].Name, result[i].Name);
            }
        }

        [Fact]
        public async Task GetProductByCode_ReturnsEmptyList_WhenInvalidCodeIsProvided()
        {
            // Arrange
            var invalidCodigo = "invalid_code"; // Código inválido
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(repo => repo.GetProductByCode(invalidCodigo))
                                 .ReturnsAsync(new List<ProductSupplierModel>()); // Código inválido retorna lista vazia

            var mapperMock = new Mock<IMapper>();
            // Configura o mapeamento para retornar uma lista vazia quando não houver produtos
            mapperMock.Setup(mapper => mapper.Map<List<ProductSupplierDTO>>(It.IsAny<List<ProductSupplierModel>>()))
                      .Returns(new List<ProductSupplierDTO>());

            var productService = new GetProductService(productRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await productService.GetProductByCode(invalidCodigo);

            // Assert
            Assert.Empty(result);
        }
    }
}
