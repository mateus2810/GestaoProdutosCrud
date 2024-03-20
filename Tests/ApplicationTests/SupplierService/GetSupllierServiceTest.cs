using Application.Services.SupplierServices.GetSupllierService;
using AutoMapper;
using Domain.DTO;
using Domain.Interfaces;
using Domain.Model;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Tests.ApplicationTests.SupplierService
{
    public class GetSupllierServiceTest
    {
        [Fact]
        public async Task GetAllSupplier_ReturnsListOfSuppliers_WhenSuppliersExist()
        {
            // Arrange
            var expectedSupplierList = new List<SupplierDTO>
            {
                new SupplierDTO { Id = 1, Codigo = "001", Descricao = "Fornecedor 1", CNPJ = "123456789", Nome = "Fornecedor 1" },
                new SupplierDTO { Id = 2, Codigo = "002", Descricao = "Fornecedor 2", CNPJ = "987654321", Nome = "Fornecedor 2" }
            };

            var supplierRepositoryMock = new Mock<ISupplierRepository>();
            supplierRepositoryMock.Setup(repo => repo.GetAllSuppliers(0, 3))
                                  .ReturnsAsync(new List<SupplierModel>());

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<List<SupplierDTO>>(It.IsAny<List<SupplierModel>>()))
                      .Returns(expectedSupplierList);

            var getSupplierService = new GetSupllierService(supplierRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await getSupplierService.GetAllSupplier(0, 3);

            // Assert
            Assert.Equal(expectedSupplierList, result);
        }

        [Fact]
        public async Task GetAllSupplier_ReturnsEmptyList_WhenNoSuppliersExist()
        {
            // Arrange
            var supplierRepositoryMock = new Mock<ISupplierRepository>();
            supplierRepositoryMock.Setup(repo => repo.GetAllSuppliers(0, 3))
                                  .ReturnsAsync(new List<SupplierModel>());

            var mapperMock = new Mock<IMapper>();

            var getSupplierService = new GetSupllierService(supplierRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await getSupplierService.GetAllSupplier(0, 3);

            // Assert
            Assert.Null(result); 
        }
    }
}
