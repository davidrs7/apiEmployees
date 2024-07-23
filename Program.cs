using Api.Interfaces;
using Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddScoped<IParamListRepository, ParamListRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IConfigurationRepository, ConfigurationRepository>();

builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();

builder.Services.AddScoped<IStepRepository, StepRepository>();
builder.Services.AddScoped<IVacantRepository, VacantRepository>();
builder.Services.AddScoped<IPostulateRepository, PostulateRepository>();

builder.Services.AddScoped<ISurveyRepository, SurveyRepository>();

builder.Services.AddScoped<IAbsenceRepository, AbsenceRepository>();

var app = builder.Build();
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .WithOrigins("http://localhost:4200", "http://localhost:53576"));
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapControllers();
app.MapFallbackToController("Index", "Fallback");
app.Run();