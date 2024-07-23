using System.Data.SqlClient;
using Api.DTO.Survey;
using Api.Interfaces;
using Api.Queries;
using Api.Utils;
using Dapper;
using MySql.Data.MySqlClient;

namespace Api.Repositories
{

    public class SurveyRepository : ISurveyRepository
    {
        private IConfiguration _configuration;
        private SurveyQueries _surveyQueries;
        public SurveyRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _surveyQueries = new SurveyQueries();
        }

        public async Task<IEnumerable<SurveyDTO>> Surveys()
        {
            string surveysSql = _surveyQueries.Surveys;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var surveysResponse = await connection.QueryAsync<SurveyDTO>(surveysSql);
                return surveysResponse.ToList();
            }
        }

        public async Task<SurveyDTO> SurveyById(int Id)
        {
            string surveySql = _surveyQueries.SurveyById;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var surveyResponse = await connection.QueryAsync<SurveyDTO>(surveySql, new {
                    Id = Id
                });
                return surveyResponse.First();
            }
        }

        public async Task<int> Add(SurveyDTO surveyAdd)
        {
            string addSql = _surveyQueries.Add;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = this.createCommandSurvey(addSql, connection, surveyAdd))
                {
                    await connection.OpenAsync();
                    int surveyId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return surveyId;
                }
            }
        }

        public async Task Edit(SurveyDTO surveyEdit)
        {
            string editSql = _surveyQueries.Edit;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = this.createCommandSurvey(editSql, connection, surveyEdit))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Id", surveyEdit.Id));
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        
        public async Task<IEnumerable<SurveyHeaderDTO>> HeadersBySurvey(int surveyId) {
            string surveysSql = _surveyQueries.HeadersBySurvey;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var surveysResponse = await connection.QueryAsync<SurveyHeaderDTO>(surveysSql, new {
                    SurveyId = surveyId
                });
                return surveysResponse.ToList();
            }
        }
        
        public async Task<IEnumerable<SurveyHeaderDTO>> HeadersByUser(int userId) {
            string surveysSql = _surveyQueries.HeadersByUser;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var surveysResponse = await connection.QueryAsync<SurveyHeaderDTO>(surveysSql, new {
                    UserId = userId
                });
                return surveysResponse.ToList();
            }
        }
        
        public async Task<SurveyHeaderDTO> HeaderBySurveyAndUser(int surveyId, int userId) {
            string surveySql = _surveyQueries.HeaderBySurveyAndUser;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var surveyResponse = await connection.QueryAsync<SurveyHeaderDTO>(surveySql, new {
                    SurveyId = surveyId,
                    UserId = userId
                });
                return surveyResponse.First();
            }
        }
        
        public async Task<int> AddHeader(SurveyHeaderDTO surveyAdd) {
            string addSql = _surveyQueries.AddHeader;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = this.createCommandSurvey(addSql, connection, surveyAdd))
                {
                    await connection.OpenAsync();
                    int surveyId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return surveyId;
                }
            }
        }

        public async Task EditHeader(SurveyHeaderDTO surveyEdit) {
            string editSql = _surveyQueries.EditHeader;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = this.createCommandSurvey(editSql, connection, surveyEdit))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Id", surveyEdit.Id));
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<SurveyFieldDTO>> Fields()
        {
            string fieldsSql = _surveyQueries.Fields;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var fieldsResponse = await connection.QueryAsync<SurveyFieldDTO>(fieldsSql);
                return fieldsResponse.ToList();
            }
        }

        public async Task<IEnumerable<SurveyFieldDTO>> FieldsBySurvey(int surveyId) {
            string fieldsSql = _surveyQueries.FieldsBySurvey;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var fieldsResponse = await connection.QueryAsync<SurveyFieldDTO>(fieldsSql, new {
                    SurveyId = surveyId
                });
                return fieldsResponse.ToList();
            }
        }
        
        public async Task<IEnumerable<SurveyFieldDTO>> FieldsByHeader(int headerId) {
            string fieldsSql = _surveyQueries.FieldsByHeader;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var fieldsResponse = await connection.QueryAsync<SurveyFieldDTO>(fieldsSql, new {
                    HeaderId = headerId
                });
                return fieldsResponse.ToList();
            }
        }

        public async Task<SurveyFieldDTO> FieldById(int Id)
        {
            string fieldSql = _surveyQueries.FieldById;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var fieldResponse = await connection.QueryAsync<SurveyFieldDTO>(fieldSql, new {
                    Id = Id
                });
                return fieldResponse.First();
            }
        }

        public async Task AddField(SurveyFieldDTO surveyAdd)
        {
            string addFieldSql = _surveyQueries.FieldAdd;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(addFieldSql, new {
                    Available = surveyAdd.Available,
                    Name = surveyAdd.Name,
                    FieldType = surveyAdd.FieldType,
                    IsRequired = surveyAdd.IsRequired,
                    Config = surveyAdd.Config
                });
            }
        }

        public async Task EditField(SurveyFieldDTO surveyEdit)
        {
            string editFieldSql = _surveyQueries.FieldEdit;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(editFieldSql, new {
                    Id = surveyEdit.Id,
                    Available = surveyEdit.Available,
                    Name = surveyEdit.Name,
                    FieldType = surveyEdit.FieldType,
                    IsRequired = surveyEdit.IsRequired,
                    Config = surveyEdit.Config
                });
            }
        }

        public async Task MergeSurveyFieldRel(SurveyFieldRelDTO relMerge)
        {
            string sql = String.Empty;
            if(relMerge.Updated) {
                sql = _surveyQueries.FieldRelEdit;
            } else if(relMerge.Inserted) {
                sql = _surveyQueries.FieldRelAdd;
            } else {
                return;
            }

            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("SurveyFieldId", relMerge.Id));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("SurveyId", relMerge.SurveyId));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Active", relMerge.Active));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Weight", relMerge.Weight));

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<SurveyUserDTO>> UsersByHeader(int headerId)
        {
            string usersSql = _surveyQueries.UsersByHeader;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var usersResponse = await connection.QueryAsync<SurveyUserDTO>(usersSql, new {
                    HeaderId = headerId
                });
                return usersResponse.ToList();
            }
        }

        public async Task<int> AddUserRel(SurveyUserRelDTO userRel)
        {
            string userSql = _surveyQueries.AddUserRel;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(userSql, new {
                    UserId = userRel.UserId,
                    SurveyId = userRel.SurveyId,
                    HeaderId = userRel.HeaderId
                });
                return userRel.UserId;
            }
        }

        public async Task<int> AddUserRelByDepartment(SurveyUserRelDTO userRel)
        {
            string userSql = _surveyQueries.AddUserRelByDepartment;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(userSql, new {
                    ParamId = userRel.ParamId,
                    HeaderId = userRel.HeaderId
                });
                return userRel.ParamId;
            }
        }

        public async Task<int> AddUserRelByJob(SurveyUserRelDTO userRel)
        {
            string userSql = _surveyQueries.AddUserRelByJob;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(userSql, new {
                    ParamId = userRel.ParamId,
                    HeaderId = userRel.HeaderId
                });
                return userRel.ParamId;
            }
        }

        public async Task<int> AddUserRelByCity(SurveyUserRelDTO userRel)
        {
            string userSql = _surveyQueries.AddUserRelByCity;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(userSql, new {
                    ParamId = userRel.ParamId,
                    HeaderId = userRel.HeaderId
                });
                return userRel.ParamId;
            }
        }

        public async Task<int> DeleteUserRel(SurveyUserRelDTO userRel)
        {
            string userSql = _surveyQueries.DeleteUserRel;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(userSql, new {
                    UserId = userRel.UserId,
                    SurveyId = userRel.SurveyId,
                    HeaderId = userRel.HeaderId
                });
                return userRel.UserId;
            }
        }

        private MySqlCommand createCommandSurvey(string sql, MySqlConnection connection, SurveyDTO surveyMerge)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Name", surveyMerge.Name));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Available", surveyMerge.Available));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Description", surveyMerge.Description));
            return command;
        }

        private MySqlCommand createCommandSurvey(string sql, MySqlConnection connection, SurveyHeaderDTO surveyMerge)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("SurveyId", surveyMerge.SurveyId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Started", surveyMerge.Started));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Finished", surveyMerge.Finished));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Title", surveyMerge.Title));
            return command;
        }
    }
}
