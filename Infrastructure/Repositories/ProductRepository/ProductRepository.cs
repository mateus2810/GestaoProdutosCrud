using Data.Repositories.Base;
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

        public async Task<List<ProductModel>> GetAllProduct()
        {
            List<ProductModel> products = new List<ProductModel>();

            try
            {
                using (var command = _dbSession.Connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Produto";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ProductModel product = new ProductModel
                            {
                                Id = reader.GetInt32("Id"),
                                Descricao = reader["Descricao"].ToString(),
                                Codigo = reader["Codigo"].ToString(), 
                                Situacao = Convert.ToBoolean(reader["Situacao"]), 
                                DataFabricacao = Convert.ToDateTime(reader["DataFabricacao"]), 
                                DataValidade = Convert.ToDateTime(reader["DataValidade"])
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
                _dbSession.Dispose(); // Garante que a conexão seja fechada
            }

            return products;
        }
    }
}