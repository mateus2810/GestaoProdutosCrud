using Data.Repositories.Base;
using Domain.Input;
using Domain.Interfaces;
using Domain.Model;
using System;
using System.Collections.Generic;
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

        public async Task<List<SupplierModel>> GetAllSuppliers(int pageNumber, int pageSize)
        {
            List<SupplierModel> suppliers = new List<SupplierModel>();

            try
            {
                int offset = (pageNumber - 1) * pageSize;

                using (var command = _dbSession.Connection.CreateCommand())
                {
                    // Modifique a consulta SQL para incluir a cláusula de paginação
                    command.CommandText = "SELECT * FROM Fornecedor ORDER BY Id OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                    command.Parameters.AddWithValue("@PageSize", pageSize);
                    command.Parameters.AddWithValue("@Offset", offset);

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
                throw new Exception("Ocorreu um erro ao obter os fornecedores.", ex);
            }
            finally
            {
                _dbSession.Dispose();
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

                    await command.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao inserir o fornecedor.", ex);
            }
            finally
            {
                _dbSession.Dispose();
            }
        }

        public async Task<bool> UpdateSupplier(int id, SupplierInput supplierInput)
        {
            try
            {
                using (var command = _dbSession.Connection.CreateCommand())
                {
                    command.CommandText =
                        "UPDATE Fornecedor " +
                        "SET Codigo = @Codigo, Descricao = @Descricao, CNPJ = @CNPJ, Nome = @Nome WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Codigo", supplierInput.Codigo);
                    command.Parameters.AddWithValue("@Descricao", supplierInput.Descricao);
                    command.Parameters.AddWithValue("@CNPJ", supplierInput.CNPJ);
                    command.Parameters.AddWithValue("@Nome", supplierInput.Nome);
                    command.Parameters.AddWithValue("@Id", id);

                    await command.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao atualizar o fornecedor.", ex);
            }
            finally
            {
                _dbSession.Dispose();
            }
        }


        public async Task<bool> DeleteSupplier(int id)
        {
            try
            {
                using (var command = _dbSession.Connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM Fornecedor WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    await command.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ocorreu um erro ao excluir o fornecedor.", ex);
            }
            finally
            {
                _dbSession.Dispose();
            }
        }
    }
}

