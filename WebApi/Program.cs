using Domain.Domain;
using Domain.Domain.Interfaces;
using Domain.Domain.Interfaces.Service;
using Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Service.Main;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApiContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("sqlConnection"),
            x => x.MigrationsAssembly("WebApi")));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyMethod();
                          policy.AllowAnyHeader();
                      });
});

//Repositories
builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<IHobbyRepository, HobbyRepository>();
builder.Services.AddTransient<IStudentHobbyRepository, StudentHobbyRepository>();

//Services
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IHobbyService, HobbyService>();
builder.Services.AddTransient<IStudentHobbyService, StudentHobbyService>();


var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(MyAllowSpecificOrigins);
app.Run();

