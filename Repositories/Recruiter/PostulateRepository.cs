using System.Data.SqlClient;
using Api.DTO.Postulate;
using Api.Base;
using Api.Interfaces;
using Api.Queries;
using Api.Utils;
using Dapper;
using MySqlConnector;

namespace Api.Repositories
{

    public class PostulateRepository : FileUploadRepositoryBase, IPostulateRepository
    {
        private IConfiguration _configuration;
        private PostulateQueries _postulateQueries;
        private int pageSize = 6;
        public PostulateRepository(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _postulateQueries = new PostulateQueries();
        }

        public async Task<IEnumerable<PostulateBasicDTO>> Postulates()
        {
            string postulatesSql = _postulateQueries.Postulates;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                var postulateResponse = await connection.QueryAsync<PostulateBasicDTO>(postulatesSql);
                return postulateResponse.ToList();
            }
        }

        public async Task<IEnumerable<PostulateBasicDTO>> Postulates(PostulateCriteriaDTO criteria)
        {
            string postulatesSql1 = "WITH Postulates AS (SELECT P.Id, P.EmployeeId, EL.Name AS EducationalLevelName, P.ExpectedSalary, P.FirstName, P.LastName, P.CellPhone, P.Email, '#076AE2' AS ColorHex, P.Career, P.Description, IF(P.PhotoUrl IS NULL, NULL, CONCAT('https://hrprueba.s3.us-east-2.amazonaws.com/', P.PhotoUrl)) AS PhotoUrl, ROW_NUMBER() OVER (ORDER BY P.Id) AS filas, @Pages AS Quantity FROM postulate P LEFT JOIN educational_level EL ON P.EducationalLevelId = EL.Id";
            string postulatesSql2 = ") SELECT Id, EducationalLevelName, ExpectedSalary, FirstName, LastName, CellPhone, Email, ColorHex, Career, Description, PhotoUrl, filas, CEILING((Quantity/@PageSize)) AS Pages FROM Postulates WHERE  filas BETWEEN ((@PageSize * @Page) - (@PageSize - 1)) AND (@PageSize * @Page)";
            string postulatesSqlPages = "SELECT CAST(COUNT(P.Id) AS decimal(10,2)) FROM postulate P"; 

            postulatesSql1 = this.QueryWhereCriteria(criteria, postulatesSql1, true, false, "PVR.VacantId", true, " LEFT JOIN postulate_vacant_rel PVR ON P.Id = PVR.PostulateId ");
            postulatesSql2 = this.QueryWhereCriteria(criteria, postulatesSql2);
            postulatesSqlPages = this.QueryWhereCriteria(criteria, postulatesSqlPages, true, false, "PVR.VacantId", true, " LEFT JOIN postulate_vacant_rel PVR ON P.Id = PVR.PostulateId ");

             
                using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
                {
                    using (MySqlCommand command = new MySqlCommand(postulatesSqlPages, connection))
                    {
                        PostulateCriteriaDTO criteriaPrep = this.prepareCriteriaToQuery(criteria);
                        if (criteriaPrep.VacantId != null && criteriaPrep.VacantId > 0)
                            command.Parameters.Add(SqlUtils.obtainMySqlParameter("VacantId", criteria.VacantId));
                        if (criteriaPrep.Name != null && criteriaPrep.Name != "" && criteriaPrep.Name != "null")
                            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Name", criteriaPrep.Name));
                        if (criteriaPrep.Doc != null && criteriaPrep.Doc != "" && criteriaPrep.Doc != "null")
                            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Doc", criteriaPrep.Doc));

                        await connection.OpenAsync();
                        criteriaPrep.Pages = Convert.ToDecimal(await command.ExecuteScalarAsync());
                        var postulatesResponse = await connection.QueryAsync<PostulateBasicDTO>(postulatesSql1 + postulatesSql2, criteriaPrep);
                        return postulatesResponse.ToList();
                    }
                }
           


        }

        public async Task<PostulateDTO> PostulateById(int Id)
        {
            string postulateSql = _postulateQueries.PostulateById;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                var postulateResponse = await connection.QueryAsync<PostulateDTO>(postulateSql, new
                {
                    Id = Id
                });
                return postulateResponse.First();
            }
        }

        public async Task<int> Add(PostulateMergeDTO postulateAdd)
        {
            string addSql = _postulateQueries.Add;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            { 
                    using (MySqlCommand command = this.createCommandPostulate(addSql, connection, postulateAdd))
                    {
                        await connection.OpenAsync();
                        int postulateId = Convert.ToInt32(await command.ExecuteScalarAsync());
                        if (postulateAdd.Photo != null)
                        {
                            string fileName = (postulateAdd.Doc != null) ? postulateAdd.Doc.ToString() : postulateId.ToString();
                            await this.PhotoUpload(postulateAdd.Photo, fileName);
                            var postulateResponse = await connection.ExecuteAsync(_postulateQueries.UpdatePhotoUrl, new
                            {
                                PhotoUrl = fileName,
                                Id = postulateId
                            });
                        }
                        return postulateId;
                    }
                

            }
        }

        public async Task Edit(PostulateMergeDTO postulateEdit)
        {
            string editSql = _postulateQueries.Edit;
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                using (MySqlCommand command = this.createCommandPostulate(editSql, connection, postulateEdit))
                {
                    command.Parameters.Add(SqlUtils.obtainMySqlParameter("Id", postulateEdit.Id));
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    if (postulateEdit.Photo != null)
                    {
                        string fileName = (postulateEdit.Doc != null) ? postulateEdit.Doc.ToString() : postulateEdit.Id.ToString();
                        await this.PhotoUpload(postulateEdit.Photo, fileName);
                        var postulateResponse = await connection.ExecuteAsync(_postulateQueries.UpdatePhotoUrl, new
                        {
                            PhotoUrl = fileName,
                            Id = postulateEdit.Id
                        });
                    }
                }
            }
        }

        public async Task ToEmployee(PostulateToEmployeeDTO toEmployee)
        {
            using (var connection = new MySqlConnection(_configuration.GetConnectionString("Db")))
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync(_postulateQueries.UpdateToEmployee, new
                {
                    Id = toEmployee.Id,
                    EmployeeId = toEmployee.EmployeeId
                });
                await connection.ExecuteAsync(_postulateQueries.UpdateToEmployeeRel, new
                {
                    Id = toEmployee.Id,
                    VacantId = toEmployee.VacantId
                });
            }
        }

        private string QueryWhereCriteria(PostulateCriteriaDTO criteria, string queryBase)
        {
            return this.QueryWhereCriteria(criteria, queryBase, false, true, "", false, "");
        }

        private string QueryWhereCriteria(PostulateCriteriaDTO criteria, string queryBase, bool vacantFilterActive, bool wherePrint, string vacantFieldName, bool whitAliasPostulate)
        {
            return this.QueryWhereCriteria(criteria, queryBase, vacantFilterActive, wherePrint, vacantFieldName, whitAliasPostulate, "");
        }

        private string QueryWhereCriteria(PostulateCriteriaDTO criteria, string queryBase, bool vacantFilterActive, bool wherePrint, string vacantFieldName, bool whitAliasPostulate, string whereVacantComplement)
        {
            string aliasPostulate = whitAliasPostulate ? "P." : "";

            if (vacantFilterActive && criteria.VacantId != null && criteria.VacantId > 0)
            {
                if (!wherePrint)
                {
                    queryBase += whereVacantComplement + " WHERE";
                    wherePrint = true;
                }
                else
                {
                    queryBase += " AND";
                }
                queryBase += " " + vacantFieldName + " = @VacantId";
            }

            if (criteria.Name != null && criteria.Name != "" && criteria.Name != "null")
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
                queryBase += " " + aliasPostulate + "Name LIKE @Name";
            }

            if (criteria.Doc != null && criteria.Doc != "" && criteria.Doc != "null")
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
                queryBase += " " + aliasPostulate + "Doc LIKE @Doc";
            }

            return queryBase;
        }

        private PostulateCriteriaDTO prepareCriteriaToQuery(PostulateCriteriaDTO criteria)
        {
            criteria.PageSize = pageSize;
            if (criteria.Name != null && criteria.Name != "" && criteria.Name != "null")
                criteria.Name = "%" + criteria.Name + "%";
            if (criteria.Doc != null && criteria.Doc != "" && criteria.Doc != "null")
                criteria.Doc = "%" + criteria.Doc + "%";
            return criteria;
        }

        private MySqlCommand createCommandPostulate(string sql, MySqlConnection connection, PostulateMergeDTO postulate)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("RecruiterUserId", postulate.RecruiterUserId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("FindOutId", postulate.FindOutId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("DocTypeId", postulate.DocTypeId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("EducationalLevelId", postulate.EducationalLevelId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("OfferedSalary", postulate.OfferedSalary));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("ExpectedSalary", postulate.ExpectedSalary));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Doc", postulate.Doc));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("FirstName", postulate.FirstName));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("LastName", postulate.LastName));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Sex", postulate.Sex));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("BirthDate", postulate.BirthDate));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Rh", postulate.Rh));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("CellPhone", postulate.CellPhone));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Phone", postulate.Phone));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Email", postulate.Email));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Career", postulate.Career));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Description", postulate.Description));
            return command;
        }
    }
}
