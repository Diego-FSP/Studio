using Ghibli.PersistenciaDapper;
using Actores;
using Peli;
using Directores;
using Personajes;
using System.Data;
using MySqlConnector;
using Scalar.AspNetCore;
using Ghibli.Persistencia;
using MinimalAPI.DTO;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySQL");

builder.Services.AddScoped<IDbConnection>(sp => new MySqlConnection(connectionString));

//Cada vez que necesite la interfaz, se va a instanciar automaticamente AdoDapper y se va a pasar al metodo de la API
builder.Services.AddScoped<IRepoActor, RepoActor>();
builder.Services.AddScoped<IRepoPelicula, RepoPelicula>();
builder.Services.AddScoped<IRepoDirector, RepoDirector>();
builder.Services.AddScoped<IRepoPersonajes, RepoPersonaje>();

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


//_______________________________________________________________________________________________________
//-------------------------------------------ACTOR-------------------------------------------------------
//_______________________________________________________________________________________________________

app.MapGet("/Actor", async (IRepoActor actor) =>
    await actor.ListarAsync());

app.MapGet("/Actor/{id}", async (int id, IRepoActor actor) =>
    await actor.DetalleAsync(id)
    is ActorVoz actor1
    ? Results.Ok(actor1)
    : Results.NotFound());

app.MapPost("/Actor/", async (ActorVoz nuevo, IRepoActor actor) =>
{
    await actor.AltaAsync(nuevo);
    return Results.Created($"/Actor/{nuevo.IdActor}", nuevo);
});

app.MapDelete("/Actor/{id}", async (int id, IRepoActor actor) =>
{
    if (await actor.DetalleAsync(id) is ActorVoz actor1)
    {
        await actor.EliminarAsync(id);
        return Results.NoContent();
    }
    return Results.NotFound();
});
//_______________________________________________________________________________________________________
//----------------------------------------PELICULAS------------------------------------------------------
//_______________________________________________________________________________________________________

app.MapGet("/Pelicula", async (IRepoPelicula repo) =>
{
    var peliculas = await repo.ListarAsync();
    return Results.Ok(peliculas.Select(p => new PeliculaDTO(p)));
});
    

app.MapGet("/Pelicula/{id}", async (int id, IRepoPelicula pelicula) =>
    await pelicula.DetalleAsync(id)
    is Pelicula peli1
    ? Results.Ok(peli1)
    : Results.NotFound());

app.MapPost("/Pelicula/", async (Pelicula nuevo, IRepoPelicula pelicula) =>
{
    await pelicula.AltaAsync(nuevo);
    return Results.Created($"/Pelicula/{nuevo.IdPelicula}", nuevo);
});

app.MapDelete("/Pelicula/{id}", async (int id, IRepoPelicula pelicula) =>
{
    if (await pelicula.DetalleAsync(id) is Pelicula pelicula1)
    {
        await pelicula.EliminarAsync(id);
        return Results.NoContent();
    }
    return Results.NotFound();
});


//_______________________________________________________________________________________________________
//----------------------------------------DIRECTORES-----------------------------------------------------
//_______________________________________________________________________________________________________

app.MapGet("/Director", async (IRepoDirector repo) =>
    await repo.ListarAsync());

app.MapPost("/Director/", async (Director nuevo, IRepoDirector repo) =>
{
    await repo.AltaAsync(nuevo);
    return Results.Created($"/Director/{nuevo.idDirector}", nuevo);
});

//_______________________________________________________________________________________________________
//----------------------------------------PERSONAJES-----------------------------------------------------
//_______________________________________________________________________________________________________

