using System.Data.SqlClient;
using Api.DTO.Department;
using Api.Interfaces;
using Api.Queries;
using Dapper;
using MySqlConnector;

namespace Api.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private IConfiguration _configuration;
        private DepartmentQueries _departmentQueries;
        public DepartmentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _departmentQueries = new DepartmentQueries();
        }

        public async Task Add(DepartmentDTO departmentAdd)
        {
            string addColorSql = _departmentQueries.ColorAdd;
            string addDepartmantSql = _departmentQueries.DepartmentAdd;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using(MySqlCommand command = new MySqlCommand(addColorSql, connection))
                {
                    command.Parameters.Add(new MySqlParameter("Hex", departmentAdd.ColorHex));
                    await connection.OpenAsync();
                    int colorId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    var employeesResponse = await connection.ExecuteAsync(addDepartmantSql, new {
                        ColorId = colorId,
                        Name = departmentAdd.Name
                    });
                }
            }
        }

        public async Task Edit(DepartmentDTO departmentEdit)
        {
            string addColorSql = _departmentQueries.ColorAdd;
            string editDepartmantNameSql = _departmentQueries.DepartmentEditName;
            string editDepartmantColorSql = _departmentQueries.DepartmentEditColor;

            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                if(departmentEdit.ChangeColorHex) {
                    using(MySqlCommand commandAddColor = new MySqlCommand(addColorSql, connection))
                    {
                        commandAddColor.Parameters.Add(new MySqlParameter("Hex", departmentEdit.ColorHex));
                        int colorId = Convert.ToInt32(await commandAddColor.ExecuteScalarAsync());
                        var employeesResponse = await connection.ExecuteAsync(editDepartmantColorSql, new {
                            ColorId = colorId,
                            Id = departmentEdit.Id
                        });
                    }
                }

                using(MySqlCommand command = new MySqlCommand(editDepartmantNameSql, connection))
                {
                    command.Parameters.Add(new MySqlParameter("Name", departmentEdit.Name));
                    command.Parameters.Add(new MySqlParameter("Id", departmentEdit.Id));
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<DepartmentDTO>> Departments()
        { 
                string departmentsSql = _departmentQueries.DepartmentsIntegracion;
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
                {
                    await connection.OpenAsync();
                    var departmentsResponse = await connection.QueryAsync<DepartmentDTO>(departmentsSql);
                    return departmentsResponse.ToList();
                } 
        }

        public async Task<DepartmentDTO> DepartmentById(int departmentId)
        {
            string employeeSql = _departmentQueries.Department;
            using(var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var departmentsResponse = await connection.QueryAsync<DepartmentDTO>(employeeSql, new {
                    Id = departmentId
                });
                return departmentsResponse.First();
            }
        }
    }
}