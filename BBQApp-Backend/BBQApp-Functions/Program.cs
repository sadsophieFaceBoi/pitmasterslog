using Backend_DB;
using BackendDB;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);
builder.Configuration.AddUserSecrets<Program>();
var config = builder.Configuration;
var endpoint = config["CosmosDB:Endpoint"];
var key = config["CosmosDB:PrimaryKey"];
var databaseName = config["CosmosDB:DatabaseName"];
builder.Services.AddSingleton<CosmosClient>(s => new CosmosClient(endpoint, key));
builder.Services.AddScoped<BBQEquipmentTypeService>(s => new BBQEquipmentTypeService(s.GetRequiredService<CosmosClient>(), databaseName));
builder.Services.AddScoped<RecipeeDBService>(s => new RecipeeDBService(s.GetRequiredService<CosmosClient>(), databaseName));

builder.ConfigureFunctionsWebApplication();

// Application Insights isn't enabled by default. See https://aka.ms/AAt8mw4.
// builder.Services
//     .AddApplicationInsightsTelemetryWorkerService()
//     .ConfigureFunctionsApplicationInsights();

builder.Build().Run();
