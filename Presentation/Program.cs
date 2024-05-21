using System.Data.SqlClient;
using System.Text;
using System.Text.Json.Serialization;
using Application.Interfaces;
using Application.Services;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
AddAuthentication(builder.Services, configuration);

var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("*")
                  .AllowAnyHeader();
        });
});

AddDatabase(builder.Services, configuration);
AddIoC(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
AddSwagger(builder.Services);

var app = builder.Build();

app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);
app.UseRouting();
app.UseAuthentication();
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
                Title = "Financial Management",
                Version = "v1"
            });
        x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer'[space] and then your token in the text input below. \r\n\r\nExample: \"Bearer 123\"",
        });
        x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            Array.Empty<string>()
        }
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
    services.AddScoped<IFinancialGoalService, FinancialGoalService>();

    //Repositories
    services.AddScoped<IUserRepository, UserRepository>();
    services.AddScoped<IFinancialControlRepository, FinancialControlRepository>();
    services.AddScoped<IFinancialGoalRepository, FinancialGoalRepository>();
}

static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
{
    services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        var key = Encoding.UTF8.GetBytes(configuration["JWTKey"]);
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });
}

