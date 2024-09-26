using System.Data.SqlClient;
using Amazon.S3;
using Amazon.S3.Transfer;
using Api.DTO.Absence;
using Api.DTO.Employee;
using Api.Utils;
using MySqlConnector;

namespace Api.Base
{
    public abstract class FileUploadRepositoryBase
    {
        private readonly IConfiguration _configuration;

        public FileUploadRepositoryBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected string CreateUrlFile(EmployeeFileMergeDTO fileDTO)
        {
            return this.CreateUrlFile(fileDTO.Department, fileDTO.City, fileDTO.Name, fileDTO.Level1, fileDTO.Level2, fileDTO.Level3);
        }

        protected string CreateUrlFile(AbsenceFileDTO fileDTO)
        {
            return this.CreateUrlFile(fileDTO.Department, fileDTO.City, fileDTO.Name, fileDTO.Level1, fileDTO.Level2, fileDTO.Level3);
        }

        protected MySqlCommand createCommandEmployeeFile(string sql, MySqlConnection connection, EmployeeFileMergeDTO employeeFile)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("EmployeeId", employeeFile.EmployeeId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Department", employeeFile.Department));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Name", employeeFile.Name));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("City", employeeFile.City));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Level1", employeeFile.Level1));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Level2", employeeFile.Level2));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Level3", employeeFile.Level3));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Url", employeeFile.Url));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("FileName", employeeFile.FileName));
            return command;
        }

        protected MySqlCommand createCommandEmployeeFile(string sql, MySqlConnection connection, AbsenceFileDTO absenceFile)
        {
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("EmployeeId", absenceFile.EmployeeId));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Department", absenceFile.Department));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Name", absenceFile.Name));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("City", absenceFile.City));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Level1", absenceFile.Level1));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Level2", absenceFile.Level2));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Level3", absenceFile.Level3));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("Url", absenceFile.Url));
            command.Parameters.Add(SqlUtils.obtainMySqlParameter("FileName", absenceFile.FileName));
            return command;
        }

        protected async Task PhotoUpload(IFormFile formFile, string doc) 
        {
            await this.PhotoUpload(formFile, "", doc + ".png");
        }

        protected async Task PhotoUpload(IFormFile formFile, string urlFile, string doc) 
        {
            var client = this.getClient();
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                var uploadRequest = new TransferUtilityUploadRequest
                {
                    InputStream = memoryStream,
                    Key = urlFile + doc,
                    BucketName = "hrprueba",
                    CannedACL = S3CannedACL.PublicRead
                };
                var transferUtility = new TransferUtility(client);
                await transferUtility.UploadAsync(uploadRequest);
            }
        }

        protected AmazonS3Client getClient()
        {
            string access = _configuration.GetSection("AWS:access").Value;  
            string secret = _configuration.GetSection("AWS:pwaws").Value;  
            return new AmazonS3Client(access, secret, Amazon.RegionEndpoint.USEast1);
        }

        private string CreateUrlFile(string department, string city, string name, string level1, string? level2, string? level3)
        {
            var urlFile = department + "/" + city + "/" + name + "/" + level1 + "/";
            if(level2 != null && level2 != "" && level2 != "null")
                urlFile += level2 + "/";
            if(level3 != null && level3 != "" && level3 != "null")
                urlFile += level3 + "/";
            return urlFile;
        }
    }
}