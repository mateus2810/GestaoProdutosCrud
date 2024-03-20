using Data.Repositories.Base;
using Domain.Input;
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


        public async Task<bool> CreateSupplier(SupplierInput supplierInput)
        {
            try
            {
                using (var command = _dbSession.Connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO Fornecedor (Codigo, Descricao, CNPJ, Nome) VALUES (@Codigo, @Descricao, @CNPJ, @Nome); SELECT SCOPE_IDENTITY()";
                    command.Parameters.AddWithValue("@Codigo", supplierInput.Codigo);
                    command.Parameters.AddWithValue("@Descricao", supplierInput.Descricao);
                    command.Parameters.AddWithValue("@CNPJ", supplierInput.CNPJ);
                    command.Parameters.AddWithValue("@Nome", supplierInput.Nome);

                    // Executa o comando do fornecedor inserido
                    await command.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Trate a exceção aqui ou relance para ser tratada em níveis superiores
                throw new Exception("Erro ao inserir o fornecedor.", ex);
            }
            finally
            {
                _dbSession.Dispose(); // Garante que a conexão seja fechada
            }
        }
    }
}
