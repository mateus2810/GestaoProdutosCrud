using Data.Repositories.Base;
using Domain.Input;
using Domain.Interfaces;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbSession _dbSession;

        public ProductRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<List<ProductModel>> GetAllProduct(int pageNumber, int pageSize)
        {
            List<ProductModel> products = new List<ProductModel>();

            try
            {
                using (var command = _dbSession.Connection.CreateCommand())
                {
                    // Calcular o deslocamento com base no número da página e no tamanho da página
                    int offset = (pageNumber - 1) * pageSize;

                    command.CommandText = "SELECT * FROM Produto ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                    command.Parameters.AddWithValue("@Offset", offset);
                    command.Parameters.AddWithValue("@PageSize", pageSize);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ProductModel product = new ProductModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Descricao = reader["Descricao"].ToString(),
                                Codigo = reader["Codigo"].ToString(),
                                Situacao = Convert.ToBoolean(reader["Situacao"]),
                                DataFabricacao = Convert.ToDateTime(reader["DataFabricacao"]),
                                DataValidade = Convert.ToDateTime(reader["DataValidade"]),
                                FornecedorID = reader.IsDBNull(reader.GetOrdinal("FornecedorId")) ? null : (int?)reader.GetInt32(reader.GetOrdinal("FornecedorId"))
                            };

                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao obter os produtos. Por favor, tente novamente mais tarde.", ex);
            }
            finally
            {
                _dbSession.Dispose();
            }

            return products;
        }

        public async Task<bool> CreateProduct(ProductInput product)
        {
            try
            {
                using (var command = _dbSession.Connection.CreateCommand())
                {
                    command.CommandText =
                        "INSERT INTO Produto (" +
                            "Descricao, Codigo, Situacao, DataFabricacao, DataValidade, FornecedorId) " +
                        "VALUES " +
                            "(@Descricao, @Codigo, @Situacao, @DataFabricacao, @DataValidade, @FornecedorId)";
                    command.Parameters.AddWithValue("@Descricao", product.Descricao);
                    command.Parameters.AddWithValue("@Codigo", product.Codigo);
                    command.Parameters.AddWithValue("@Situacao", product.Situacao);
                    command.Parameters.AddWithValue("@DataFabricacao", product.DataFabricacao);
                    command.Parameters.AddWithValue("@DataValidade", product.DataValidade);
                    command.Parameters.AddWithValue("@FornecedorId", product.FornecedorId);
                    await command.ExecuteNonQueryAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao inserir o produto.", ex);
            }
            finally
            {
                _dbSession.Dispose();
            }
        }

        public async Task<bool> UpdateProduct(int productId, ProductInput product)
        {
            try
            {
                using (var command = _dbSession.Connection.CreateCommand())
                {
                    command.CommandText = "UPDATE Produto SET Descricao = @Descricao, Codigo = @Codigo, Situacao = @Situacao, DataFabricacao = @DataFabricacao, DataValidade = @DataValidade WHERE Id = @ProductId";
                    command.Parameters.AddWithValue("@Descricao", product.Descricao);
                    command.Parameters.AddWithValue("@Codigo", product.Codigo);
                    command.Parameters.AddWithValue("@Situacao", product.Situacao);
                    command.Parameters.AddWithValue("@DataFabricacao", product.DataFabricacao);
                    command.Parameters.AddWithValue("@DataValidade", product.DataValidade);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    await command.ExecuteNonQueryAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o produto.", ex);
            }
            finally
            {
                _dbSession.Dispose();
            }
        }


        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
                using (var command = _dbSession.Connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Produto WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao excluir o produto.", ex);
            }
            finally
            {
                _dbSession.Dispose();
            }
        }

        public async Task<List<ProductSupplierModel>> GetProductByCode(string codigo)
        {
            List<ProductSupplierModel> products = new List<ProductSupplierModel>();

            try
            {
                using (var command = _dbSession.Connection.CreateCommand())
                {
                    command.CommandText = @"SELECT p.Id AS ProductId, 
                                                p.Descricao AS ProductDescription, 
                                                p.Codigo AS ProductCode, 
                                                p.Situacao AS Situation, 
                                                p.DataFabricacao AS ManufactureDate, 
                                                p.DataValidade AS ExpiryDate, 
                                                f.Id AS SupplierId, 
                                                f.Codigo AS SupplierCode, 
                                                f.Descricao AS SupplierDescription, 
                                                f.CNPJ AS CNPJ, 
                                                f.Nome AS Name
                                            FROM Produto p
                                            LEFT JOIN Fornecedor f ON p.FornecedorId = f.Id
                                            WHERE p.Codigo = @Codigo";
                    command.Parameters.AddWithValue("@Codigo", codigo);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ProductSupplierModel productSupplier = new ProductSupplierModel
                            {
                                ProductId = reader.IsDBNull(reader.GetOrdinal("ProductId")) ? 0 : reader.GetInt32(reader.GetOrdinal("ProductId")),
                                ProductDescription = reader["ProductDescription"].ToString(),
                                ProductCode = reader["ProductCode"].ToString(),
                                Situation = reader.IsDBNull(reader.GetOrdinal("Situation")) ? false : Convert.ToBoolean(reader["Situation"]),
                                ManufactureDate = reader.IsDBNull(reader.GetOrdinal("ManufactureDate")) ? DateTime.MinValue : Convert.ToDateTime(reader["ManufactureDate"]),
                                ExpiryDate = reader.IsDBNull(reader.GetOrdinal("ExpiryDate")) ? DateTime.MinValue : Convert.ToDateTime(reader["ExpiryDate"]),
                                SupplierId = reader.IsDBNull(reader.GetOrdinal("SupplierId")) ? 0 : reader.GetInt32(reader.GetOrdinal("SupplierId")),
                                SupplierCode = reader.IsDBNull(reader.GetOrdinal("SupplierCode")) ? null : reader["SupplierCode"].ToString(),
                                SupplierDescription = reader.IsDBNull(reader.GetOrdinal("SupplierDescription")) ? null : reader["SupplierDescription"].ToString(),
                                CNPJ = reader.IsDBNull(reader.GetOrdinal("CNPJ")) ? null : reader["CNPJ"].ToString(),
                                Name = reader.IsDBNull(reader.GetOrdinal("Name")) ? null : reader["Name"].ToString()
                            };

                            products.Add(productSupplier);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the product by code.", ex);
            }
            finally
            {
                _dbSession.Dispose();
            }

            return products;
        }
    }
}
