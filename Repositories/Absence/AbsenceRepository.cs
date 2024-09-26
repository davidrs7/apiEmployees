using System.Data.SqlClient;
using Api.DTO.Absence;
using Api.DTO.Configuration;
using Api.Base;
using Api.Interfaces;
using Api.Queries;
using Api.Utils;
using Dapper;  
using MySqlConnector;

namespace Api.Repositories
{
    public class AbsenceRepository: FileUploadRepositoryBase, IAbsenceRepository
    {
        private IConfiguration _configuration;
        private AbsenceQueries _absenceQueries;
        private EmployeeQueries _employeeQueries;
        private ConfigurationQueries _configurationQueries;
        public AbsenceRepository(IConfiguration configuration): base(configuration)
        {
            _configuration = configuration;
            _absenceQueries = new AbsenceQueries();
            _employeeQueries = new EmployeeQueries();
            _configurationQueries = new ConfigurationQueries();

        }

        public async Task<IEnumerable<AbsenceUserDTO>> Users(int userId)
        { 
                string userSql = _absenceQueries.UserById;
                string usersSql = _absenceQueries.Users;

                using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
                {
                    await connection.OpenAsync();
                    var userResponse = await connection.QueryAsync<AbsenceUserDTO>(userSql, new { Id = userId });

                    if (userResponse.Count() > 0)
                    {
                        var user = userResponse.First();
                        var usersResponse = await connection.QueryAsync<AbsenceUserDTO>(usersSql, new { Id = user.JobId });
                        var users = usersResponse.ToList();
                        users.Add(user);
                        users.Reverse();
                        return users;
                    }

                    return userResponse.ToList();
                }
            

        }

        public async Task<AbsenceUserDTO> User(int employeeId)
        {
            string userSql = _absenceQueries.User;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var userResponse = await connection.QueryAsync<AbsenceUserDTO>(userSql, new {
                    Id = employeeId
                });
                return userResponse.First();
            }
        }

        public async Task<IEnumerable<AbsenceDTO>> Absences(int employeeId)
        {
            string absencesSql = _absenceQueries.Absences;
            string jobIdSql = _absenceQueries.FindJobId;
            string configSql = _configurationQueries.ConfigurationItems;
            string configUserSql = _configurationQueries.ConfigurationItemValue;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();

                int jobId = 0;
                var jobIdResponse = await connection.QueryAsync<int>(jobIdSql, new { Id = employeeId });
                if(jobIdResponse.Count() > 0)
                    jobId = jobIdResponse.First();

                var configUserResponse = await connection.QueryAsync<ConfigurationItemDTO>(configUserSql, new {
                    ConfigKey = "absence.users.allowed",
                    Value = employeeId.ToString()
                });

                if(configUserResponse.Count() > 0) {
                    var configResponse = await connection.QueryAsync<ConfigurationItemDTO>(configSql, new { ConfigKey = "absence.types.allowed" });
                    if(configResponse.Count() > 0) {
                        var configObj = configResponse.First();
                        absencesSql += " OR AbsenceTypeId IN (" + configObj.Value1 + ")";
                    }
                }

                var absenceResponse = await connection.QueryAsync<AbsenceDTO>(absencesSql, new {
                    Id = employeeId,
                    JobId = jobId
                });
                return absenceResponse.ToList();
            }
        }

        public async Task<AbsenceDTO> Absence(int Id)
        {
            string absenceSql = _absenceQueries.Absence;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var abesenceResponse = await connection.QueryAsync<AbsenceDTO>(absenceSql, new {
                    Id = Id
                });
                return abesenceResponse.First();
            }
        }

        public async Task<int> Add(AbsenceDTO absenceAdd)
        {
            string addSql = _absenceQueries.Add;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = this.createCommandAbsence(addSql, connection, absenceAdd))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Created", DateTime.Now));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Updated", DateTime.Now));
                    await connection.OpenAsync();
                    int absenceId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return absenceId;
                }
            }
        }

        public async Task Edit(AbsenceDTO absenceEdit)
        {
            string editSql = _absenceQueries.Edit;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = this.createCommandAbsence(editSql, connection, absenceEdit))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Id", absenceEdit.Id));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Updated", DateTime.Now));
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<AbsenceApprovalDTO>> Approvals(int absenceId)
        {
            string approvalsSql = _absenceQueries.Approvals;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var approvalsResponse = await connection.QueryAsync<AbsenceApprovalDTO>(approvalsSql, new {
                    Id = absenceId
                });
                return approvalsResponse.ToList();
            }
        }

        public async Task<int> AddApproval(AbsenceApprovalDTO approvalAdd)
        {
            string addSql = _absenceQueries.AddApproval;
            string validateSql = _absenceQueries.ValidateApproval;
            string updateAbsenceSql = _absenceQueries.UpdateApproval;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = this.createCommandApproval(addSql, connection, approvalAdd))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Created", DateTime.Now));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Updated", DateTime.Now));
                    await connection.OpenAsync();
                    int approvalId = Convert.ToInt32(await command.ExecuteScalarAsync());

                    var validateResponse = await connection.QueryAsync(validateSql, new { Id = approvalAdd.AbsenceId });
                    var updateResponse = await connection.QueryAsync(updateAbsenceSql, new {
                        Status = (validateResponse.Count() > 0 ? 0 : 1),
                        Updated = DateTime.Now,
                        Id = approvalAdd.AbsenceId
                    });
                    return approvalId;
                }
            }
        }

        public async Task EditApproval(AbsenceApprovalDTO approvalEdit)
        {
            string editSql = _absenceQueries.EditApproval;
            string validateSql = _absenceQueries.ValidateApproval;
            string updateAbsenceSql = _absenceQueries.UpdateApproval;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = this.createCommandApproval(editSql, connection, approvalEdit))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Id", approvalEdit.Id));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Updated", DateTime.Now));
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    var validateResponse = await connection.QueryAsync(validateSql, new { Id = approvalEdit.AbsenceId });
                    var updateResponse = await connection.QueryAsync(updateAbsenceSql, new {
                        Status = (validateResponse.Count() > 0 ? 0 : 1),
                        Updated = DateTime.Now,
                        Id = approvalEdit.AbsenceId
                    });
                }
            }
        }

        public async Task<IEnumerable<AbsenceFileDTO>> Files(int absenceId)
        {
            string approvalsSql = _absenceQueries.Files;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var approvalsResponse = await connection.QueryAsync<AbsenceFileDTO>(approvalsSql, new {
                    AbsenceId = absenceId
                });
                return approvalsResponse.ToList();
            }
        }

        public async Task<int> AddFile(AbsenceFileDTO fileDTO)
        {
            if(fileDTO.Document != null)
            {
                var urlFile = this.CreateUrlFile(fileDTO);
                fileDTO.Url = urlFile;
                fileDTO.FileName = fileDTO.Document.FileName;

                await this.PhotoUpload(fileDTO.Document, urlFile, fileDTO.Document.FileName);
                string addSql = _employeeQueries.AddFile;
                using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
                {
                    using(MySqlCommand command = this.createCommandEmployeeFile(addSql, connection, fileDTO))
                    {
                        await connection.OpenAsync();
                        fileDTO.Id = Convert.ToInt32(await command.ExecuteScalarAsync());

                        string absenceFileSql = _absenceQueries.AddFile;
                        var absenceResponse = await connection.QueryAsync(absenceFileSql, new {
                            AbsenceId = fileDTO.AbsenceId,
                            EmployeeFileId = fileDTO.Id
                        });
                        return fileDTO.Id;
                    }
                }
            }

            return 0;
        }

        private MySqlCommand createCommandAbsence(string sql, MySqlConnection connection, AbsenceDTO absenceMerge)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("AbsenceTypeId", absenceMerge.AbsenceTypeId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("UserId", absenceMerge.UserId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("EmployeeId", absenceMerge.EmployeeId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Started", absenceMerge.Started));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Finished", absenceMerge.Finished));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("BusinessDays", absenceMerge.BusinessDays));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Active", absenceMerge.Active));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Status", absenceMerge.Status));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Description", absenceMerge.Description));
            return command;
        }

        private MySqlCommand createCommandApproval(string sql, MySqlConnection connection, AbsenceApprovalDTO absenceMerge)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("AbsenceId", absenceMerge.AbsenceId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("UserId", absenceMerge.UserId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Approval", absenceMerge.Approval));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Description", absenceMerge.Description));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("IsHRApproval", absenceMerge.IsHRApproval));
            return command;
        }
    }
}