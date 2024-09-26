using System.Data.SqlClient;
using Api.DTO.Department;
using Api.DTO.ParamList;
using Api.Interfaces;
using Api.Queries;
using Dapper;
using MySqlConnector;

namespace Api.Repositories
{
    public class ParamListRepository : IParamListRepository
    {
        private IConfiguration _configuration;
        private ParamListQueries _paramListQueries;
        public ParamListRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _paramListQueries = new ParamListQueries();
        }

        public async Task<IEnumerable<ParamListDTO>> DocTypeParams()
        {
            
                string paramSql = _paramListQueries.DocType;
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
                {
                    await connection.OpenAsync();
                    var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                    return paramResponse.ToList();
                }
            

        }

        public async Task<IEnumerable<ParamListDTO>> CityParams()
        {
            string paramSql = _paramListQueries.City;
            
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
                {
                    await connection.OpenAsync();
                    var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                    return paramResponse.ToList();
                }
             

        }

        public async Task<IEnumerable<ParamListDTO>> MaritalStatusParams()
        {
            string paramSql = _paramListQueries.MaritalStatus;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }

        public async Task<IEnumerable<ParamListDTO>> HousingTypeParams()
        {
            string paramSql = _paramListQueries.HousingType;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }

        public async Task<IEnumerable<ParamListDTO>> EducationalLevelParams()
        {
            string paramSql = _paramListQueries.EducationalLevel;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }

        public async Task<IEnumerable<ParamListDTO>> EmployeeStatusParams()
        {
            string paramSql = _paramListQueries.EmployeeStatus;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }

        public async Task<IEnumerable<ParamListDTO>> ContractTypeParams()
        {
            string paramSql = _paramListQueries.ContractType;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }

        public async Task<IEnumerable<ParamListDTO>> BankingEntityParams()
        {
            string paramSql = _paramListQueries.BankingEntity;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }

        public async Task<IEnumerable<ParamListDTO>> TransportationParams()
        {
            string paramSql = _paramListQueries.Transportation;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }

        public async Task<IEnumerable<ParamListDTO>> VacantStatusParams()
        {
            string paramSql = _paramListQueries.VacantStatus;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }

        public async Task<IEnumerable<ParamListDTO>> PostulateFindOutParams()
        {
            string paramSql = _paramListQueries.PostulateFindOut;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }

        public async Task<IEnumerable<ParamListDTO>> JobSkillsParams()
        {
            string paramSql = _paramListQueries.JobSkills;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }

        public async Task<IEnumerable<ParamListDTO>> DepartmentParams()
        {
            string paramSql = _paramListQueries.Department;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }

        public async Task<IEnumerable<ParamListDTO>> JobParams()
        {
            string paramSql = _paramListQueries.Job;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }

        public async Task<IEnumerable<ParamListDTO>> AbsenceTypeParams()
        {
            string paramSql = _paramListQueries.AbsenceType;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var paramResponse = await connection.QueryAsync<ParamListDTO>(paramSql);
                return paramResponse.ToList();
            }
        }
    }
}