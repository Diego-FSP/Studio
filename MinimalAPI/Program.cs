using Ghibli.PersistenciaDapper;
using Actores;
using System.Data;
using MySqlConnector;
using Scalar.AspNetCore;
using Ghibli.Persistencia;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySQL");

builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));

//Cada vez que necesite la interfaz, se va a instanciar automaticamente AdoDapper y se va a pasar al metodo de la API
builder.Services.AddScoped<IRepoActor, RepoActor>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}

app.MapGet("/Actor", async (IRepoActor actor) =>
    await actor.ListarAsync());

app.MapPost("/Actor/{ID}", async (ActorVoz nuevo , IRepoActor actor) =>
await actor.AltaAsync(nuevo));

app.Run();
