using Ghibli.PersistenciaDapper;
using Ghibli.Persistencia;
using Actores;
using System.Data;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySQL");

builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));

//Cada vez que necesite la interfaz, se va a instanciar automaticamente AdoDapper y se va a pasar al metodo de la API
builder.Services.AddScoped<RepoBase, RepoActor>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapGet("/", () => "hola mundo");

app.Run();
