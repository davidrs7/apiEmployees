using System.Data.SqlClient;
using Api.DTO.Employee;
using Api.Base;
using Api.Interfaces;
using Api.Queries;
using Api.Utils;
using Dapper;
using MySqlConnector;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Repositories
{
    public class EmployeeRepository : FileUploadRepositoryBase, IEmployeeRepository
    {
        private IConfiguration _configuration;
        private EmployeeQueries _employeeQueries;
        private int pageSize = 6;
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _employeeQueries = new EmployeeQueries();
        }

        public async Task<IEnumerable<EmployeeBasicDTO>> EmployeesAll(EmployeeCriteriaDTO employeeCriteria)
        {

            string employeesAllSql = @" 
     SELECT 
         Id, 
         DepartmentId, 
         DepartmentName, 
         JobName, 
         ColorHex, 
         Name, 
         CellPhone, 
         Email, 
         PhotoUrl, 
         paginado AS paginado
     FROM (
         SELECT 
             E.Id, 
             D.RolID AS DepartmentId, 
             D.Nombre AS DepartmentName, 
             J.Nombre AS JobName,
             C.Hex AS ColorHex, 
             E.Name, 
             E.Doc, 
             E.CellPhone, 
             E.Email, 
             IF(E.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', E.PhotoUrl)) AS PhotoUrl, 
             E.Id AS paginado
         FROM  
             usuarios U
         INNER JOIN  employee E ON  E.Id = U.UsuarioIdOpcional 
         LEFT JOIN cargos J ON E.JobId = J.CargoID
         LEFT JOIN roles D ON U.RolID = D.RolID 
         LEFT JOIN color C ON D.ColorId = C.Id
     ) AS Employees ";
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var employeesResponse = await connection.QueryAsync<EmployeeBasicDTO>(employeesAllSql);
                return employeesResponse.ToList();
            }

        }

        public async Task<IEnumerable<EmployeeBasicDTO>> Employees(EmployeeCriteriaDTO employeeCriteria)
        {

            string employeesSql = @" 
                    SELECT 
                        Id, 
                        DepartmentId, 
                        DepartmentName, 
                        JobName, 
                        ColorHex, 
                        Name, 
                        CellPhone, 
                        Email, 
                        PhotoUrl, 
                        paginado AS paginado, 
                        CEILING((Quantity/@PageSize)) AS Pages 
                    FROM (
                        SELECT 
                            E.Id, 
                            D.RolID AS DepartmentId, 
                            D.Nombre AS DepartmentName, 
                            J.Nombre AS JobName,
                            C.Hex AS ColorHex, 
                            E.Name, 
                            E.Doc, 
                            E.CellPhone, 
                            E.Email, 
                            IF(E.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', E.PhotoUrl)) AS PhotoUrl, 
                            ROW_NUMBER() OVER (ORDER BY E.Id)  AS paginado,
                            @Pages AS Quantity 
                        FROM  
                            usuarios U
                        INNER JOIN  employee E ON  E.Id = U.UsuarioIdOpcional 
                        LEFT JOIN cargos J ON E.JobId = J.CargoID
                        LEFT JOIN roles D ON U.RolID = D.RolID 
                        LEFT JOIN color C ON D.ColorId = C.Id ";


            string employeesql2 = ") AS Employees WHERE paginado BETWEEN((@PageSize * @Page) - (@PageSize - 1)) AND(@PageSize * @Page); ";
            string employeeSqlPages = "SELECT CAST(COUNT(E.Id) AS DECIMAL(10,2)) FROM employee E";


            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand(employeeSqlPages, connection))
                {
                    EmployeeCriteriaDTO employeeCriteriaPrep = this.PrepareCriteriaToQuery(employeeCriteria);

                    if (employeeCriteriaPrep.DepartmentId != null && employeeCriteriaPrep.DepartmentId > 0)
                        employeesSql = employeesSql + (employeesSql.Contains("WHERE") ? " AND D.RolID " + employeeCriteriaPrep.DepartmentId : " WHERE D.RolID = " + employeeCriteriaPrep.DepartmentId);
                    if (employeeCriteriaPrep.Name != null && employeeCriteriaPrep.Name != "" && employeeCriteriaPrep.Name != "null")
                        employeesSql = employeesSql + (employeesSql.Contains("WHERE") ? " AND E.Name like'" + employeeCriteriaPrep.Name + "'" : "WHERE E.Name like'" + employeeCriteriaPrep.Name + "'");
                    if (employeeCriteriaPrep.Doc != null && employeeCriteriaPrep.Doc != "" && employeeCriteriaPrep.Doc != "null")
                        employeesSql = employeesSql + (employeesSql.Contains("WHERE") ? " AND E.Doc = " + employeeCriteriaPrep.Doc : "WHERE E.Doc = '" + employeeCriteriaPrep.Doc + "'");

                    employeeCriteriaPrep.Pages = Convert.ToDecimal(await command.ExecuteScalarAsync());

                    var employeesResponse = await connection.QueryAsync<EmployeeBasicDTO>(employeesSql + employeesql2, employeeCriteriaPrep);

                    return employeesResponse.ToList();
                }
            }


        }

        public async Task<IEnumerable<EmployeeDownloadDTO>> EmployeesDownload(EmployeeCriteriaDTO employeeCriteria)
        {
            string employeesSql = _employeeQueries.EmployeesDownloadBase;
            employeesSql = this.QueryWhereCriteria(employeeCriteria, employeesSql);

            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var employeesResponse = await connection.QueryAsync<EmployeeDownloadDTO>(employeesSql, employeeCriteria);
                return employeesResponse.ToList();
            }
        }

        public async Task<IEnumerable<EmployeeBasicDTO>> EmployeesWithoutPages(int excludeEmployeeId)
        {
            string employeesSql = _employeeQueries.EmployeesWithoutPages;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var employeesResponse = await connection.QueryAsync<EmployeeBasicDTO>(employeesSql, new
                {
                    EmployeeId = excludeEmployeeId
                });
                return employeesResponse.ToList();
            }
        }

        public async Task<int> Add(EmployeeMergeDTO employeeAdd)
        {
            string addSql = _employeeQueries.Add;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using (MySqlCommand command = this.createCommandEmployee(addSql, connection, employeeAdd))
                {
                    await connection.OpenAsync();
                    await command.ExecuteScalarAsync();
                    command.CommandText = "SELECT max(id) from employee e;";
                    int employeeId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    if (employeeAdd.Photo != null)
                    {
                        string fileName = (employeeAdd.Doc != null) ? employeeAdd.Doc.ToString() : employeeId.ToString();
                        await this.PhotoUpload(employeeAdd.Photo, fileName);
                    }
                    return employeeId;
                }
            }



        }

        public async Task<int> AddGeneral(EmployeeGeneralDTO employeeGeneralAdd)
        {
            string addSql = _employeeQueries.AddGeneral;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using (MySqlCommand command = this.createCommandEmployeeGeneral(addSql, connection, employeeGeneralAdd))
                {
                    await connection.OpenAsync();
                    int employeeGeneralId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    var employeesResponse = await connection.ExecuteAsync("UPDATE employee SET GeneralId = @GeneralId WHERE Id = @Id", new
                    {
                        GeneralId = employeeGeneralId,
                        Id = employeeGeneralAdd.EmployeeId
                    });
                    return employeeGeneralId;
                }
            }
        }


        public async Task<int> AddAcademic(EmployeeAcademicDTO employeeAcademicAdd)
        {
            string addSql = _employeeQueries.AddAcademic;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using (MySqlCommand command = this.createCommandEmployeeAcademic(addSql, connection, employeeAcademicAdd))
                {
                    await connection.OpenAsync();
                    int employeeAcademicId = Convert.ToInt32(await command.ExecuteScalarAsync());
                    var employeesResponse = await connection.ExecuteAsync("UPDATE employee SET AcademicId = @AcademicId WHERE Id = @Id", new
                    {
                        AcademicId = employeeAcademicId,
                        Id = employeeAcademicAdd.EmployeeId
                    });
                    return employeeAcademicId;
                }
            }
        }

        public async Task<bool> DeleteAcademic(int id)
        { 
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("DELETE FROM employee_academic WHERE Id =" + id);
                return true;
            }
        }

        public async Task<int> AddFile(EmployeeFileMergeDTO employeeFileAdd)
        {
            if (employeeFileAdd.Document != null)
            {
                var urlFile = this.CreateUrlFile(employeeFileAdd);
                employeeFileAdd.Url = urlFile;
                employeeFileAdd.FileName = employeeFileAdd.Document.FileName;

                await this.PhotoUpload(employeeFileAdd.Document, urlFile, employeeFileAdd.Document.FileName);
                string addSql = _employeeQueries.AddFile;
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
                {
                    using (MySqlCommand command = this.createCommandEmployeeFile(addSql, connection, employeeFileAdd))
                    {
                        await connection.OpenAsync();
                        return Convert.ToInt32(await command.ExecuteScalarAsync());
                    }
                }
            }

            return 0;
        }

        public async Task Edit(EmployeeMergeDTO employeeEdit)
        {
            string editSql = _employeeQueries.Edit;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using (MySqlCommand command = this.createCommandEmployee(editSql, connection, employeeEdit))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Id", employeeEdit.Id));
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    if (employeeEdit.Photo != null)
                    {
                        string fileName = (employeeEdit.Doc != null) ? employeeEdit.Doc.ToString() : employeeEdit.Id.ToString();
                        await this.PhotoUpload(employeeEdit.Photo, fileName);
                        var employeesResponse = await connection.ExecuteAsync("UPDATE employee SET PhotoUrl = @PhotoUrl WHERE Id = @Id", new
                        {
                            PhotoUrl = fileName + ".png",
                            Id = employeeEdit.Id
                        });
                    }
                }
            }
        }

        public async Task EditGeneral(EmployeeGeneralDTO employeeGeneralEdit)
        {

            EmployeeGeneralDTO employeeGeneral = await EmployeeGeneral(employeeGeneralEdit.EmployeeId);

            if (employeeGeneral.Id == 0)
            {
                await AddGeneral(employeeGeneralEdit);
            }
            else
            {
                string editSql = _employeeQueries.EditGeneral;
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
                {
                    using (MySqlCommand command = this.createCommandEmployeeGeneral(editSql, connection, employeeGeneralEdit))
                    {
                        command.Parameters.Add(SqlUtils.obtainMySqlParameter("Id", employeeGeneralEdit.Id));
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }


        }

        public async Task EditAcademic(EmployeeAcademicDTO employeeAcademicEdit)
        { 
                string editSql = _employeeQueries.EditAcademic;
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
                {
                    using (MySqlCommand command = this.createCommandEmployeeAcademic(editSql, connection, employeeAcademicEdit))
                    { 
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            
        }

        public async Task MergeKnowledge(EmployeeKnowledgeDTO merge)
        {
            string sql = String.Empty;
            if (merge.Inserted)
            {
                sql = _employeeQueries.AddKnowledge;
            }
            else if (merge.Updated)
            {
                sql = _employeeQueries.EditKnowledge;
            }
            else
            {
                return;
            }

            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("KnowledgeId", merge.KnowledgeId));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("EmployeeId", merge.EmployeeId));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Active", merge.Active));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Rate", merge.Rate));

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task MergeSkill(EmployeeSkillDTO merge)
        {
            string sql = String.Empty;
            if (merge.Inserted)
            {
                sql = _employeeQueries.AddSkill;
            }
            else if (merge.Updated)
            {
                sql = _employeeQueries.EditSkill;
            }
            else
            {
                return;
            }

            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("SkillId", merge.SkillId));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("EmployeeId", merge.EmployeeId));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Active", merge.Active));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Rate", merge.Rate));

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<EmployeeDTO> Employee(int employeeId)
        {

            string employeeSql = _employeeQueries.Employee;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var employeeResponse = await connection.QueryAsync<EmployeeDTO>(employeeSql, new
                {
                    EmployeeId = employeeId
                });
                return employeeResponse.First();
            }


        }

        public async Task<EmployeeGeneralDTO> EmployeeGeneral(int employeeId)
        {

            string employeeSql = _employeeQueries.EmployeeGeneral;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var employeeResponse = await connection.QueryAsync<EmployeeGeneralDTO>(employeeSql, new
                {
                    EmployeeId = employeeId
                });

                return employeeResponse.Count() > 0 ? employeeResponse.First() : new EmployeeGeneralDTO();
            }
        }

        public async Task<List<EmployeeAcademicDTO>> EmployeeAcademic(int employeeId)
        {
            string employeeSql = _employeeQueries.EmployeeAcademic;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var employeeResponse = await connection.QueryAsync<EmployeeAcademicDTO>(employeeSql, new
                {
                    EmployeeId = employeeId
                });
                return employeeResponse.Count() > 0 ? employeeResponse.ToList() : new List<EmployeeAcademicDTO>();
            }
        }

        public async Task<IEnumerable<EmployeeSkillDTO>> Skills()
        {
            string employeesSql = _employeeQueries.EmployeeSkillsNew;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var employeesResponse = await connection.QueryAsync<EmployeeSkillDTO>(employeesSql);
                return employeesResponse.ToList();
            }
        }

        public async Task<IEnumerable<EmployeeKnowledgeDTO>> Knowledges()
        {
            string employeesSql = _employeeQueries.EmployeeKnowledgeNew;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var employeesResponse = await connection.QueryAsync<EmployeeKnowledgeDTO>(employeesSql);
                return employeesResponse.ToList();
            }
        }

        public async Task<IEnumerable<EmployeeFileDTO>> Files(int employeeId)
        {
            string employeesSql = _employeeQueries.EmployeeFile;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var employeesResponse = await connection.QueryAsync<EmployeeFileDTO>(employeesSql, new
                {
                    EmployeeId = employeeId
                });
                return employeesResponse.ToList();
            }
        }

        public async Task<IEnumerable<EmployeeSkillDTO>> Skills(int employeeId)
        {
            string employeesSql = _employeeQueries.EmployeeSkills;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {

                await connection.OpenAsync();
                var employeesResponse = await connection.QueryAsync<EmployeeSkillDTO>(employeesSql, new
                {
                    EmployeeId = employeeId
                });
                return employeesResponse.ToList();


            }
        }

        public async Task<IEnumerable<EmployeeSonsDTO>> Sons(int employeeId)
        {
            string employeesSql = _employeeQueries.EmployeeSons;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("DB")))
            {
                await connection.OpenAsync();
                var employeeResponse = await connection.QueryAsync<EmployeeSonsDTO>(employeesSql, new
                {
                    EmployeeId = employeeId
                });
                return employeeResponse.ToList();
            }
        }


        public async Task<IEnumerable<EmployeeKnowledgeDTO>> Knowledges(int employeeId)
        {
            string employeesSql = _employeeQueries.EmployeeKnowledge;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var employeesResponse = await connection.QueryAsync<EmployeeKnowledgeDTO>(employeesSql, new
                {
                    EmployeeId = employeeId
                });
                return employeesResponse.ToList();
            }
        }

        public async Task<IEnumerable<EmployeeFileTypeDTO>> EmployeeFileTypes()
        {
            string employeesSql = _employeeQueries.EmployeeFileTypes;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var employeesResponse = await connection.QueryAsync<EmployeeFileTypeDTO>(employeesSql);
                return employeesResponse.ToList();
            }
        }

        private MySqlCommand createCommandEmployee(string sql, MySqlConnection connection, EmployeeMergeDTO employeeMerge)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("JobId", employeeMerge.JobId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("StatusId", employeeMerge.StatusId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("MaritalStatusId", employeeMerge.MaritalStatusId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("DocTypeId", employeeMerge.DocTypeId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("DocIssueCityId", employeeMerge.DocIssueCityId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("ContractTypeId", employeeMerge.ContractTypeId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("JobCityId", employeeMerge.JobCityId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("BankingEntityId", employeeMerge.BankingEntityId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Doc", employeeMerge.Doc));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("DocIssueDate", employeeMerge.DocIssueDate));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Name", employeeMerge.Name));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Sex", employeeMerge.Sex));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("BirthDate", employeeMerge.BirthDate));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Rh", employeeMerge.Rh));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("CorpCellPhone", employeeMerge.CorpCellPhone));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("CellPhone", employeeMerge.CellPhone));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Phone", employeeMerge.Phone));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Email", employeeMerge.Email));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("EmploymentDate", employeeMerge.EmploymentDate));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("BankAccount", employeeMerge.BankAccount));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("BankAccountType", employeeMerge.BankAccountType));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("HasVaccine", employeeMerge.HasVaccine));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("VaccineMaker", employeeMerge.VaccineMaker));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("VaccineDose", employeeMerge.VaccineDose));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("HasVaccineBooster", employeeMerge.HasVaccineBooster));
            return command;
        }

        private MySqlCommand createCommandEmployeeGeneral(string sql, MySqlConnection connection, EmployeeGeneralDTO employeeGeneral)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("CityId", employeeGeneral.CityId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("HousingTypeId", employeeGeneral.HousingTypeId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("TransportationId", employeeGeneral.TransportationId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("EmergencyContactName", employeeGeneral.EmergencyContactName));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("EmergencyContactPhone", employeeGeneral.EmergencyContactPhone));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("EmergencyContactRelationship", employeeGeneral.EmergencyContactRelationship));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Dependents", employeeGeneral.Dependents));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("DependentsUnder9", employeeGeneral.DependentsUnder9));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("DependentBirthDate", employeeGeneral.DependentBirthDate));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Address", employeeGeneral.Address));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Neighborhood", employeeGeneral.Neighborhood));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("HousingTime", employeeGeneral.HousingTime));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("SocioeconomicStatus", employeeGeneral.SocioeconomicStatus));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("LicensePlate", employeeGeneral.LicensePlate));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("VehicleMark", employeeGeneral.VehicleMark));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("VehicleModel", employeeGeneral.VehicleModel));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("LicenseNumber", employeeGeneral.LicenseNumber));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("LicenseCategory", employeeGeneral.LicenseCategory));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("LicenseValidity", employeeGeneral.LicenseValidity));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("SOATExpirationDate", employeeGeneral.SOATExpirationDate));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("RTMExpirationDate", employeeGeneral.RTMExpirationDate));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("VehicleOwnerName", employeeGeneral.VehicleOwnerName));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("ContributorType", employeeGeneral.ContributorType));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Eps", employeeGeneral.Eps));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Arl", employeeGeneral.Arl));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Afp", employeeGeneral.Afp));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("RecommendedBy", employeeGeneral.RecommendedBy));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Description", employeeGeneral.Description));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Sons", employeeGeneral.Sons));
            return command;
        }

        private MySqlCommand createCommandEmployeeAcademic(string sql, MySqlConnection connection, EmployeeAcademicDTO employeeAcademic)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("EducationalLevelId", employeeAcademic.EducationalLevelId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Career", employeeAcademic.Career));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("EmployeeId", employeeAcademic.EmployeeId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("academicEndDate", employeeAcademic.academicEndDate));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Id", employeeAcademic.Id));
            return command;
        }

        private string QueryWhereCriteria(EmployeeCriteriaDTO employeeCriteria, string queryBase)
        {
            return this.QueryWhereCriteria(employeeCriteria, queryBase, false, "D.Id", true);
        }

        private string QueryWhereCriteria(EmployeeCriteriaDTO employeeCriteria, string queryBase, bool wherePrint, string departmentFieldName, bool whitAliasEmpleyee)
        {
            return this.QueryWhereCriteria(employeeCriteria, queryBase, wherePrint, departmentFieldName, whitAliasEmpleyee, "");
        }

        private string QueryWhereCriteria(EmployeeCriteriaDTO employeeCriteria, string queryBase, bool wherePrint, string departmentFieldName, bool whitAliasEmpleyee, string whereDepartmentComplement)
        {
            string aliasEmpleyee = whitAliasEmpleyee ? "E." : "";

            if (employeeCriteria.DepartmentId != null && employeeCriteria.DepartmentId > 0)
            {
                if (!wherePrint)
                {
                    queryBase += whereDepartmentComplement + " WHERE";
                    wherePrint = true;
                }
                else
                {
                    queryBase += " AND";
                }
                queryBase += " " + departmentFieldName + " = @DepartmentId";
            }

            if (employeeCriteria.Name != null && employeeCriteria.Name != "" && employeeCriteria.Name != "null")
            {
                if (!wherePrint)
                {
                    queryBase += " WHERE";
                    wherePrint = true;
                }
                else
                {
                    queryBase += " AND";
                }
                queryBase += " " + aliasEmpleyee + "Name LIKE @Name";
            }

            if (employeeCriteria.Doc != null && employeeCriteria.Doc != "" && employeeCriteria.Doc != "null")
            {
                if (!wherePrint)
                {
                    queryBase += " WHERE";
                    wherePrint = true;
                }
                else
                {
                    queryBase += " AND";
                }
                queryBase += " " + aliasEmpleyee + "Doc LIKE @Doc";
            }

            return queryBase;
        }

        private EmployeeCriteriaDTO PrepareCriteriaToQuery(EmployeeCriteriaDTO employeeCriteria)
        {
            employeeCriteria.PageSize = pageSize;
            if (employeeCriteria.Name != null && employeeCriteria.Name != "" && employeeCriteria.Name != "null")
                employeeCriteria.Name = "%" + employeeCriteria.Name + "%";
            if (employeeCriteria.Doc != null && employeeCriteria.Doc != "" && employeeCriteria.Doc != "null")
                employeeCriteria.Doc = employeeCriteria.Doc;
            return employeeCriteria;
        }

        public Task<int> UpdateSons(EmployeeSonsDTO sonData)
        {
            string query = _employeeQueries.EmployeeUpdateSon;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("id", sonData.Id));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("sonName", sonData.SonName));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("sonBornDate", sonData.SonBornDate));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("employeeGeneralId", sonData.EmployeeGeneralId));
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    Console.WriteLine("updated: " + result);
                    return Task.FromResult(result);

                }
            }
        }

        public Task<int> AddSons(EmployeeSonsDTO sonData)
        {
            string query = _employeeQueries.EmployeeAddSon;
            Console.WriteLine("-------------");
            Console.WriteLine(sonData.EmployeeGeneralId);
            Console.WriteLine(sonData.SonName);
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {

                    Console.WriteLine(query);
                    /*command.Parameters.Add(this.obtainMySqlParameter("id", sonData.Id));*/
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("employeeGeneralId", sonData.EmployeeGeneralId));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("sonBornDate", sonData.SonBornDate));
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("sonName", sonData.SonName));
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    Console.WriteLine("added: " + sonData);
                    return Task.FromResult(result);


                }
            }

        }

        public Task<int> DelSons(int sonId)
        {
            string query = _employeeQueries.EmployeeDelSon;
            Console.WriteLine(query);
            Console.WriteLine(sonId);
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("id", sonId.ToString()));
                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return Task.FromResult(result);

                }
            }
            return Task.FromResult(0);
        }
    }
}