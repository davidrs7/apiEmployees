using System.Data.SqlClient;
using Api.DTO.Step;
using Api.Interfaces;
using Api.Queries;
using Api.Utils;
using Dapper;
using MySqlConnector;

namespace Api.Repositories
{

    public class StepRepository : IStepRepository
    {
        private IConfiguration _configuration;
        private StepQueries _stepQueries;
        public StepRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _stepQueries = new StepQueries();
        }

        public async Task<IEnumerable<StepDTO>> Steps()
        {
            string stepsSql = _stepQueries.Steps;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var stepsResponse = await connection.QueryAsync<StepDTO>(stepsSql);
                return stepsResponse.ToList();
            }
        }

        public async Task<StepDTO> StepById(int Id)
        {
            string stepSql = _stepQueries.StepById;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var stepResponse = await connection.QueryAsync<StepDTO>(stepSql, new {
                    Id = Id
                });
                return stepResponse.First();
            }
        }

        public async Task<int> Add(StepDTO stepAdd)
        {
            string addSql = _stepQueries.Add;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = this.createCommandStep(addSql, connection, stepAdd))
                {
                    await connection.OpenAsync();
                    int stepId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return stepId;
                }
            }
        }

        public async Task Edit(StepDTO stepEdit)
        {
            string editSql = _stepQueries.Edit;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = this.createCommandStep(editSql, connection, stepEdit))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Id", stepEdit.Id));
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<StepFieldDTO>> Fields()
        {
            string fieldsSql = _stepQueries.Fields;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var fieldsResponse = await connection.QueryAsync<StepFieldDTO>(fieldsSql);
                return fieldsResponse.ToList();
            }
        }

        public async Task<StepFieldDTO> FieldById(int Id)
        {
            string fieldSql = _stepQueries.FieldById;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var fieldResponse = await connection.QueryAsync<StepFieldDTO>(fieldSql, new {
                    Id = Id
                });
                return fieldResponse.First();
            }
        }

        public async Task<IEnumerable<StepFieldRelDTO>> FieldsByStep(int stepId)
        {
            string fieldsSql = _stepQueries.FieldsByStep;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var fieldsResponse = await connection.QueryAsync<StepFieldRelDTO>(fieldsSql, new {
                    StepId = stepId
                });
                return fieldsResponse.ToList();
            }
        }

        public async Task<IEnumerable<StepFieldRelValueDTO>> FieldsByStepRel(int stepId, int vacantId, int postulateId)
        {
            string fieldsSql = _stepQueries.FieldsByStepRel;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var fieldsResponse = await connection.QueryAsync<StepFieldRelValueDTO>(fieldsSql, new {
                    StepId = stepId,
                    VacantId = vacantId,
                    PostulateId = postulateId
                });
                return fieldsResponse.ToList();
            }
        }

        public async Task AddField(StepFieldDTO stepAdd)
        {
            string addFieldSql = _stepQueries.FieldAdd;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(addFieldSql, new {
                    Available = stepAdd.Available,
                    Name = stepAdd.Name,
                    FieldType = stepAdd.FieldType,
                    IsRequired = stepAdd.IsRequired,
                    Config = stepAdd.Config
                });
            }
        }

        public async Task EditField(StepFieldDTO stepEdit)
        {
            string editFieldSql = _stepQueries.FieldEdit;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(editFieldSql, new {
                    Id = stepEdit.Id,
                    Available = stepEdit.Available,
                    Name = stepEdit.Name,
                    FieldType = stepEdit.FieldType,
                    IsRequired = stepEdit.IsRequired,
                    Config = stepEdit.Config
                });
            }
        }

        public async Task MergeStepFieldRel(StepFieldRelDTO relMerge)
        {
            string sql = String.Empty;
            if(relMerge.Updated) {
                sql = _stepQueries.FieldRelEdit;
            } else if(relMerge.Inserted) {
                sql = _stepQueries.FieldRelAdd;
            } else {
                return;
            }

            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("StepFieldId", relMerge.Id));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("StepId", relMerge.StepId));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Active", relMerge.Active));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Weight", relMerge.Weight));

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task MergeStepFieldRelValue(StepFieldRelValueDTO relMerge)
        {
            string sql = String.Empty;
            if(relMerge.Updated) {
                sql = _stepQueries.FieldRelValueEdit;
            } else if(relMerge.Inserted) {
                sql = _stepQueries.FieldRelValueAdd;
            } else {
                return;
            }

            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("FieldValue", relMerge.FieldValue));
                    await connection.OpenAsync();

                    int postulateVacantRelId = Convert.ToInt32(
                        await connection.ExecuteScalarAsync(_stepQueries.StepPostRelFindId, new {
                            VacantId = relMerge.VacantId,
                            PostulateId = relMerge.PostulateId
                        })
                    );
                    int stepFieldRelId = Convert.ToInt32(
                        await connection.ExecuteScalarAsync(_stepQueries.StepFieldRelFindId, new {
                            StepId = relMerge.StepId,
                            StepFieldId = relMerge.Id
                        })
                    );

                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("PostulateVacantRelId", postulateVacantRelId));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("StepFieldRelId", stepFieldRelId));
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private MySqlCommand createCommandStep(string sql, MySqlConnection connection, StepDTO stepMerge)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Name", stepMerge.Name));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Available", stepMerge.Available));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Description", stepMerge.Description));
            return command;
        }
    }
}
