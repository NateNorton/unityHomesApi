using HomesApi.Data;
using HomesApi.Data.Repositories;
using HomesApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS services
builder
    .Services
    .AddCors(options =>
    {
        options.AddPolicy(
            "AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }
        );
    });

builder.Services.AddScoped<IUserRepository, UserRepository>();

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    // builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("LocalDB");
}
else
{
    connection = builder.Configuration.GetConnectionString("LocalDB");
}

builder.Services.AddDbContext<HomesDbContext>(options => options.UseNpgsql(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();
