namespace Api.Queries
{
    public class JobsQueries
    {
        public string Jobs { get; } = "select Id, DepartmentId, ApproveId, ReportId, Name, Profile, Functions from job order by Name";
        public string JobByID { get; } = "select Id, DepartmentId, ApproveId, ReportId, Name, Profile, Functions from job where Id=@Id";
        public string JobByDepartament { get; }= "select Id, DepartmentId, ApproveId, ReportId, Name, Profile, Functions from job where DepartamentId=@Id";
        public string JobDel { get; } = "delete from job where Id=@Id";
        public string JobUpdate { get; } = "update job set DepartmentId=@DepartmentId, ApproveId=@ApproveId, ReportId=@ReportId, Name=@Name, Profile=@Profile, Functions=@Functions where Id=@Id returning *";
        public string JobAdd { get; } = "insert into job (DepartmentId, ApproveId, ReportId, Name, Profile, Functions) VALUES (@DepartmentId, @ApproveId, @ReportId, @Name, @Profile, @Functions) RETURNING Id";

        public string JobSkills { get; } = "select Id, Active, Name, Description from job_Skills order by Name";
        public string JobSkillsByID { get; } = "select Id, Active, Name, Description from job_Skills where Id=@Id";
        public string JobSkillsByJobID { get; } = "select Id, Active, Name, Description from job_Skills where Id=@Id";
        public string JobSkillsDel { get; } = "delete from job_Skills where Id=@Id";
        public string JobSkillsUpdate { get; } = "update job_Skills set Active=@Active, Name=@Name, Description=@Description where Id=@Id returning *";
        public string JobSkillsAdd { get; } = "insert into (Active, Name, Description) values (@Active, @Name, @Description) RETURNING Id";

        public string JobGoals { get; } = "select Id, JobGoalHeaderId, EmployeeId, LeaderEmployeeId, Status, CreateAt, LastUpdateAt, StartDate, FinishDate, Name, Description, Weigh, EvaluationDate, Comments, AdvancePerc from job_Goal";
        public string JobGoalsByID { get; } = "select Id, JobGoalHeaderId, EmployeeId, LeaderEmployeeId, Status, CreateAt, LastUpdateAt, StartDate, FinishDate, Name, Description, Weigh, EvaluationDate, Comments, AdvancePerc from job_Goal where Id=@Id";
        public string JobGoalsDel { get; } = "delete from job_Goal where Id=@Id";
        public string JobGoalsUpdate { get; } = "update job_Goal set JobGoalHeaderId=@JobGoalHeaderId, EmployeeId=@EmployeeId, LeaderEmployeeId=@LeaderEmployeeId, Status=@Status, LastUpdateAt=@LastUpdateAt, StartDate=@StartDate, FinishDate=@FinishDate, Name=@Name, Description=@Description, Weigh=@Weigh, EvaluationDate=@EvaluationDate, Comments=@Comments, AdvancePerc=@AdvancePerc where Id=@Id where Id=@Id returning Id";
        public string JobGoalsAdd { get; } = "insert into job_Goal (JobGoalHeaderId, EmployeeId, LeaderEmployeeId, Status, CreateAt, LastUpdateAt, StartDate, FinishDate, Name, Description, Weigh, EvaluationDate, Comments, AdvancePerc) values (@JobGoalHeaderId, @EmployeeId, @LeaderEmployeeId, @Status, @CreateAt, @LastUpdateAt, @StartDate, @FinishDate, @Name, @Description, @Weigh, @EvaluationDate, @Comments, @AdvancePerc) returning Id";

        public string JobGoalsHeaders { get; } = "select Id, Available, Name from job_Goal_Header";
        public string JobGoalsHeadersByID { get; } = "select Id, Available, Name from job_Goal_Header where Id=@Id";
        public string JobGoalsHeadersDel { get; } = "delete from job_Goal_Header where Id=@Id";
        public string JobGoalsHeadersUpdate { get; } = "update job_Goal_Header set Available=@Available, Name=@Name where Id=@Id where returning *";
        public string JobGoalsHeadersAdd { get; } = "insert into job_Goal_Header (Available, Name) values (@Available, @Name) returning Id";


    }
}
