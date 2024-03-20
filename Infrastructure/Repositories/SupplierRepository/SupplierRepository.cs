using Data.Repositories.Base;
using Domain.Interfaces;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.SupplierRepository
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly DbSession _dbSession;

        public SupplierRepository(DbSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<List<SupplierModel>> GetAllSuppliers()
        {
            List<SupplierModel> suppliers = new List<SupplierModel>();

            try
            {
                using (var command = _dbSession.Connection.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM Fornecedor";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            SupplierModel supplier = new SupplierModel
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Nome = reader["Nome"].ToString(),
                                Codigo = reader["Codigo"].ToString(),
                                Descricao = reader["Descricao"].ToString(),
                                CNPJ = reader["CNPJ"].ToString()
                            };

                            suppliers.Add(supplier);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Lidar com exceções aqui
                throw new Exception("Ocorreu um erro ao obter os fornecedores.", ex);
            }
            finally
            {
                _dbSession.Dispose(); // Garantir que a conexão seja fechada
            }

            return suppliers;
        }

    }
}
