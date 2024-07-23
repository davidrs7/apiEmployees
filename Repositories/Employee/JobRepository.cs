using System.Data.SqlClient;
using Api.DTO.Job;
using Api.Interfaces;
using Api.Queries;
using Dapper;
using MySql.Data.MySqlClient;

namespace Api.Repositories
{
    public class JobRepository : IJobRepository
    {
        private IConfiguration _configuration;
        private JobQueries _jobQueries;
        public JobRepository(IConfiguration configuration)
        {
            _configuration = configuration;   
            _jobQueries = new JobQueries();
        }

        public async Task<int> Add(JobDTO jobAdd)
        {
            string addJobSql1 = "INSERT INTO Job (Name, DepartmentId, Profile, Functions";
            string addJobSql2 = " VALUES (@Name, @DepartmentId, @Profile, @Functions";

            if(jobAdd.ApproveId != 0) {
                addJobSql1 += ", ApproveId";
                addJobSql2 += ", @ApproveId";
            }

            if(jobAdd.ReportId != 0) {
                addJobSql1 += ", ReportId";
                addJobSql2 += ", @ReportId";
            }

            addJobSql1 += ")";
            addJobSql2 += "); SELECT LAST_INSERT_ID();";

            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = new MySqlCommand(addJobSql1 + addJobSql2, connection))
                {
                    command.Parameters.Add(new MySqlParameter("DepartmentId", jobAdd.DepartmentId));
                    command.Parameters.Add(new MySqlParameter("Name", jobAdd.Name));
                    command.Parameters.Add(new MySqlParameter("Profile", jobAdd.Profile));
                    command.Parameters.Add(new MySqlParameter("Functions", jobAdd.Functions));

                    if(jobAdd.ApproveId != 0)
                        command.Parameters.Add(new MySqlParameter("ApproveId", jobAdd.ApproveId));

                    if(jobAdd.ReportId != 0)
                        command.Parameters.Add(new MySqlParameter("ReportId", jobAdd.ReportId));

                    await connection.OpenAsync();
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }

        public async Task Edit(JobDTO jobEdit)
        {
            string editJobSql = "UPDATE Job SET Name = @Name, DepartmentId = @DepartmentId, Profile = @Profile, Functions = @Functions";
            editJobSql += (jobEdit.ApproveId != 0) ? ", ApproveId = @ApproveId" : ", ApproveId = NULL";
            editJobSql += (jobEdit.ReportId != 0) ? ", ReportId = @ReportId" : ", ReportId = NULL";
            editJobSql += " WHERE Id = @Id";

            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = new MySqlCommand(editJobSql, connection))
                {
                    command.Parameters.Add(new MySqlParameter("DepartmentId", jobEdit.DepartmentId));
                    command.Parameters.Add(new MySqlParameter("Name", jobEdit.Name));
                    command.Parameters.Add(new MySqlParameter("Profile", jobEdit.Profile));
                    command.Parameters.Add(new MySqlParameter("Functions", jobEdit.Functions));

                    if(jobEdit.ApproveId != 0)
                        command.Parameters.Add(new MySqlParameter("ApproveId", jobEdit.ApproveId));

                    if(jobEdit.ReportId != 0)
                        command.Parameters.Add(new MySqlParameter("ReportId", jobEdit.ReportId));

                    await connection.OpenAsync();
                    await command.ExecuteScalarAsync();
                }
            }
        }

        public async Task<IEnumerable<JobBasicDTO>> Jobs()
        {
            try
            {
                string jobSql = _jobQueries.JobsIntegracion;

                using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
                {
                    await connection.OpenAsync();
                    var jobResponse = await connection.QueryAsync<JobBasicDTO>(jobSql);
                    return jobResponse.ToList();
                }
            
            
            }
            catch  (Exception Ex) { 
            
            throw new Exception();}
        
        }
      

        public async Task<JobDTO> JobById(int jobId)
        {
            string jobSql = _jobQueries.Job;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var jobResponse = await connection.QueryAsync<JobDTO>(jobSql, new {
                    Id = jobId
                });
                return jobResponse.First();
            }
        }
    }
}