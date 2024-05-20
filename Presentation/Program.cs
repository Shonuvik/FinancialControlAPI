using System.Data.SqlClient;
using System.Text.Json.Serialization;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
AddDatabase(builder.Services, configuration);
AddIoC(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
AddSwagger(builder.Services);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("v1/swagger.json", "Financial Management - Authentication API"));
app.Run();


static void AddSwagger(IServiceCollection services)
{
    services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(x =>
    {
        x.SwaggerDoc("v1",
            new OpenApiInfo()
            {
                Description = "Financial Management",
                Version = "v1"
            });
    });
}

static void AddDatabase(IServiceCollection services, IConfiguration configuration)
{
    services.AddScoped<IUnitOfWork>(s => new UnitOfWork(new SqlConnection(configuration["ConnectionString"])));
}

static void AddIoC(IServiceCollection services)
{
    //Services
    services.AddScoped<IFinancialControlService, FinancialControlService>();

    //Repositories
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IFinancialControlRepository, FinancialControlRepository>();
}

