

using APBD.Repositories;
using APBD.Services;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarRentalRepository, CarRentalRepository>();

builder.Services.AddScoped<IClientService, ClientService>();

var connectionString = builder.Configuration.GetConnectionString("RentalDB");
builder.Services.AddTransient(_ => new SqlConnection(connectionString));

var app = builder.Build();