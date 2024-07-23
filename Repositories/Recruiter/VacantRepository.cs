using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using Api.DTO.Vacant;
using Api.Interfaces;
using Api.Queries;
using Api.Utils;
using Dapper;
using MySql.Data.MySqlClient;

namespace Api.Repositories
{

    public class VacantRepository : IVacantRepository
    {
        private IConfiguration _configuration;
        private VacantQueries _vacantQueries; 

        public VacantRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _vacantQueries = new VacantQueries();
        }

        public async Task<IEnumerable<VacantDTO>> Vacants()
        {
            string vacantsSql = _vacantQueries.Vacants;
            var _connect_db = new MySqlConnection(_configuration.GetConnectionString("Db")); 
            using (var connection = _connect_db)
            { 
                await connection.OpenAsync();
                var vacantsResponse = await connection.QueryAsync<VacantDTO>(vacantsSql);
                return vacantsResponse.ToList();
            }
        }

        public async Task<VacantDTO> VacantById(int Id)
        {
            string vacantSql = _vacantQueries.VacantById;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var vacantResponse = await connection.QueryAsync<VacantDTO>(vacantSql, new {
                    Id = Id
                });
                return vacantResponse.First();
            }
        }

        public async Task<int> Add(VacantDTO vacantAdd)
        {
            string addSql = _vacantQueries.Add;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = this.createCommandVacant(addSql, connection, vacantAdd))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Created", DateTime.Now));
                    await connection.OpenAsync();
                    int vacantId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return vacantId;
                }
            }
        }

        public async Task Edit(VacantDTO vacantEdit)
        {
            string editSql = _vacantQueries.Edit;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = this.createCommandVacant(editSql, connection, vacantEdit))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Id", vacantEdit.Id));
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<VacantStepDTO>> VacantSteps(int Id)
        {
            string vacantsSql = _vacantQueries.VacantSteps;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var vacantsResponse = await connection.QueryAsync<VacantStepDTO>(vacantsSql, new {
                    VacantId = Id
                });
                return vacantsResponse.ToList();
            }
        }

        public async Task<IEnumerable<VacantStepPostulateRelDTO>> VacantStepsByPostulateRel(int vacantId, int postulateId)
        {
            string vacantsSql = _vacantQueries.VacantStepsByPostulateRel;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var vacantsResponse = await connection.QueryAsync<VacantStepPostulateRelDTO>(vacantsSql, new {
                    VacantId = vacantId,
                    PostulateId = postulateId
                });
                return vacantsResponse.ToList();
            }
        }

        public async Task<IEnumerable<VacantPostulateRelDTO>> VacantsByPostulate(int postulateId)
        {
            string vacantsSql = _vacantQueries.VacantsByPostulate;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var vacantsResponse = await connection.QueryAsync<VacantPostulateRelDTO>(vacantsSql, new {
                    PostulateId = postulateId
                });
                return vacantsResponse.ToList();
            }
        }

        public async Task<IEnumerable<VacantPostulateRelDTO>> VacantsByVacantRel(int vacantId)
        {
            string vacantsSql = _vacantQueries.VacantsByVacantRel;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var vacantsResponse = await connection.QueryAsync<VacantPostulateRelDTO>(vacantsSql, new {
                    VacantId = vacantId
                });
                return vacantsResponse.ToList();
            }
        }

        public async Task MergeVacantStepRel(VacantStepDTO relMerge)
        {
            string sql = String.Empty;
            if(relMerge.Updated) {
                sql = _vacantQueries.StepRelEdit;
            } else if(relMerge.Inserted) {
                sql = _vacantQueries.StepRelAdd;
            } else {
                return;
            }

            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("StepId", relMerge.Id));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("VacantId", relMerge.VacantId));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Active", relMerge.Active));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Weight", relMerge.Weight));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("IsRequired", relMerge.IsRequired));

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task MergeVacantStepPostulateRel(VacantStepPostulateRelDTO relMerge)
        {
            string sql = String.Empty;
            Boolean setCreated = true;
            if(relMerge.Updated) {
                sql = _vacantQueries.StepPostRelEdit;
                setCreated = false;
            } else if(relMerge.Inserted) {
                sql = _vacantQueries.StepPostRelAdd;
            } else {
                return;
            }

            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    if(setCreated)
                        command.Parameters.Add(SqlUtils.obtainMySqlParameter("Created", DateTime.Now));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("StepId", relMerge.Id));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Approved", relMerge.Approved));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Reason", relMerge.Reason));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Updated", DateTime.Now));

                    await connection.OpenAsync();
                    int postulateVacantRelId = Convert.ToInt32(
                        await connection.ExecuteScalarAsync(_vacantQueries.StepPostRelFindId, new {
                            VacantId = relMerge.VacantId,
                            PostulateId = relMerge.PostulateId
                        })
                    );
                    
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("PostulateVacantRelId", postulateVacantRelId));
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> AddVacantPostulateRel(VacantPostulateRelDTO relMerge)
        {
            string vacantsSql = _vacantQueries.AddVacantPostulateRel;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = new MySqlCommand(vacantsSql, connection))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("VacantId", relMerge.Id));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("PostulateId", relMerge.PostulateId));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Active", relMerge.Active));
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    return relMerge.Id;
                }
            }
        }

        public async Task<IEnumerable<VacantDTO>> HistoricalVacants()
        {
            string vacantsSql = _vacantQueries.HistVacants;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var vacantsResponse = await connection.QueryAsync<VacantDTO>(vacantsSql);
                return vacantsResponse.ToList();
            }
        }

        public async Task<VacantDTO> HistoricalVacantById(int Id)
        {
            string vacantSql = _vacantQueries.HistVacantById;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var vacantResponse = await connection.QueryAsync<VacantDTO>(vacantSql, new {
                    Id = Id
                });
                return vacantResponse.First();
            }
        }

        private MySqlCommand createCommandVacant(string sql, MySqlConnection connection, VacantDTO vacantMerge)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("VacantStatusId", vacantMerge.VacantStatusId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("ContractTypeId", vacantMerge.ContractTypeId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("JobId", vacantMerge.JobId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("UserId", vacantMerge.UserId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("VacantNum", vacantMerge.VacantNum));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Description", vacantMerge.Description));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Updated", DateTime.Now));
            return command;
        }
    }
}
