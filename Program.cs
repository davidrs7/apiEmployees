using Api.Exeption;
using Api.Interfaces; 
using Api.Repositories;
using MiddlewareRetryMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Registrar los repositorios
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

// Configuración de URLs
app.Urls.Add("http://0.0.0.0:5000");

// Orden de middleware
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());   

app.UseMiddleware<RetryMiddleware>();   
app.UseMiddleware<ExceptionMiddleware>();  

app.UseDefaultFiles();  
app.UseStaticFiles();    

app.MapControllers();  
app.MapFallbackToController("Index", "Fallback"); 

app.Run();