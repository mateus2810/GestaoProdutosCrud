using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Data.Repositories.Base;
using Domain.Interfaces;
using Domain.Model;

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
                //_dbSession.ValidaConexao(); // Garante que a conexão esteja aberta

                using (var command = _dbSession.Connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Produto";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            ProductModel product = new ProductModel
                            {
                                // Preencha o objeto ProductModel com os dados do leitor
                                Descricao = reader["Descricao"].ToString()
                            };

                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Trate exceções aqui
            }
            finally
            {
                _dbSession.Dispose(); // Garante que a conexão seja fechada
            }

            return products;
        }
    }
}