using Microsoft.EntityFrameworkCore;
using HomesApi.Models;
using Azure.Security.KeyVault.Secrets;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    var keyVaultEndpoint = new Uri(builder.Configuration["VaulyKey"]);
    var secretClient = new SecretClient(keyVaultEndpoint, new DefaultAzureCredential());

    connection = secretClient.GetSecret("AZURE-SQL-CONNECTION-STRING").ToString();
}

builder.Services.AddDbContext<HomesDbContext>(options =>
    options.UseSqlServer(connection));


var app = builder.Build();



// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
