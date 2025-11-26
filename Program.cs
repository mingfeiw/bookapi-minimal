using System.Reflection;
using bookapi_minimal.Endpoints;
using bookapi_minimal.Services;
using Microsoft.OpenApi.Models;
using bookapi_minimal.Extensions;
using Microsoft.EntityFrameworkCore;
using bookapi_minimal.AppContext;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

string keyVaultUrl = "https://kv-bookapi.vault.azure.net/";

var secretClient = new SecretClient(new Uri(keyVaultUrl), new DefaultAzureCredential());

KeyVaultSecret secret = secretClient.GetSecret("DbConnectionString");
string connectionString = secret.Value;

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(connectionString));

builder.AddApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Mimal API",
        Version = "v1"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGroup("/api/v1/")
    .WithTags(" Book endpoints")
    .MapBookEndPoint();

// Auto-migrate database on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    context.Database.Migrate();
}

app.Run();
public partial class Program { }