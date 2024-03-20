using Application.Services.ProductServices.CreateProductService.Interface;
using Application.Services.ProductServices.DeleteProductService.Interface;
using Application.Services.ProductServices.GetProductService.Interface;
using Application.Services.ProductServices.UpdateProductService.Interface;
using Domain.DTO;
using Domain.Input;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Controllers;
using Xunit;

namespace Tests.WebApiTests
{
    public class ProductControllerTest
    {
        [Fact]
        public async Task GetAllProduct_ReturnsOkResult_WithProducts()
        {
            // Arrange
            var productServiceMock = new Mock<IGetProductService>();
            productServiceMock.Setup(service => service.GetAllProduct(0, 5))
                              .ReturnsAsync(new List<ProductDTO> { new ProductDTO() });

            var controller = new ProductController(productServiceMock.Object, null, null, null);

            // Act
            var result = await controller.GetAllProduct(0, 5);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var products = Assert.IsType<List<ProductDTO>>(okResult.Value);
            Assert.Single(products);
        }

        [Fact]
        public async Task GetAllProduct_ReturnsNoContent_WhenNoProducts()
        {
            // Arrange
            var productServiceMock = new Mock<IGetProductService>();
            productServiceMock.Setup(service => service.GetAllProduct(0, 5))
                              .ReturnsAsync(new List<ProductDTO>());

            var controller = new ProductController(productServiceMock.Object, null, null, null);

            // Act
            var result = await controller.GetAllProduct(0, 5);

            // Assert
            Assert.IsType<NoContentResult>(result);

        }

        [Fact]
        public async Task CreateProduct_ReturnsOkResult_WhenProductIsCreatedSuccessfully()
        {
            // Arrange
            var productInput = new ProductInput(); // Defina os dados de entrada conforme necessário
            var createProductServiceMock = new Mock<ICreateProductServices>();
            createProductServiceMock.Setup(service => service.CreateProduct(productInput))
                                    .ReturnsAsync((true, null));

            var controller = new ProductController(null, createProductServiceMock.Object, null, null);

            // Act
            var result = await controller.CreateProduct(productInput);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ProductDTO>>(result);
            var okResult = Assert.IsType<OkResult>(actionResult.Result);

            Assert.Equal(okResult.StatusCode, 200);
        }

        [Fact]
        public async Task CreateProduct_ReturnsBadRequestResult_WhenProductCreationFails()
        {
            // Arrange
            var productInput = new ProductInput(); // Defina os dados de entrada conforme necessário
            var errorMessage = "Erro ao criar o produto."; // Defina a mensagem de erro esperada
            var createProductServiceMock = new Mock<ICreateProductServices>();
            createProductServiceMock.Setup(service => service.CreateProduct(productInput))
                                    .ReturnsAsync((false, errorMessage));

            var controller = new ProductController(null, createProductServiceMock.Object, null, null);

            // Act
            var result = await controller.CreateProduct(productInput);

            // Assert
            var badRequestResult = Assert.IsType<ActionResult<ProductDTO>>(result);
            var objectResult = Assert.IsType<BadRequestObjectResult>(badRequestResult.Result);
            Assert.Equal(errorMessage, objectResult.Value);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsNoContent_WhenProductIsUpdatedSuccessfully()
        {
            // Arrange
            var id = 1;
            var product = new ProductInput();
            var updateProductServiceMock = new Mock<IUpdateProductService>();
            updateProductServiceMock.Setup(service => service.UpdateProduct(id, product))
                                    .ReturnsAsync((true, null));
            var controller = new ProductController(null, null, updateProductServiceMock.Object, null);

            // Act
            var result = await controller.UpdateProduct(id, product);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsBadRequest_WhenIdIsNegative()
        {
            // Arrange
            var id = -1;
            var product = new ProductInput();
            var controller = new ProductController(null, null, null, null);

            // Act
            var result = await controller.UpdateProduct(id, product);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdateProduct_ReturnsBadRequestWithErrorMessage_WhenProductUpdateFails()
        {
            // Arrange
            var id = 1;
            var product = new ProductInput();
            var errorMessage = "Error message";
            var updateProductServiceMock = new Mock<IUpdateProductService>();
            updateProductServiceMock.Setup(service => service.UpdateProduct(id, product))
                                    .ReturnsAsync((false, errorMessage));
            var controller = new ProductController(null, null, updateProductServiceMock.Object, null);

            // Act
            var result = await controller.UpdateProduct(id, product) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal(errorMessage, result.Value);
        }


        [Fact]
        public async Task DeleteProduct_ReturnsNoContent_WhenProductIsDeletedSuccessfully()
        {
            // Arrange
            var id = 1;
            var deleteProductServiceMock = new Mock<IDeleteProductService>();
            deleteProductServiceMock.Setup(service => service.DeleteProduct(id))
                                    .ReturnsAsync(true);
            var controller = new ProductController(null, null, null, deleteProductServiceMock.Object);

            // Act
            var result = await controller.DeleteProduct(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsBadRequest_WhenIdIsNegative()
        {
            // Arrange
            var id = -1;
            var controller = new ProductController(null, null, null, null);

            // Act
            var result = await controller.DeleteProduct(id);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteProduct_ReturnsNotFound_WhenProductDeletionFails()
        {
            // Arrange
            var id = 1;
            var deleteProductServiceMock = new Mock<IDeleteProductService>();
            deleteProductServiceMock.Setup(service => service.DeleteProduct(id))
                                    .ReturnsAsync(false);
            var controller = new ProductController(null, null, null, deleteProductServiceMock.Object);

            // Act
            var result = await controller.DeleteProduct(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }


    }

}

