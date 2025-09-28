using Directores;
using Ghibli.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Peli;
using Studio.mvc.ViewModel;

namespace Studio.mvc.Controllers;

public class PeliculasController : Controller
{
    IRepoPelicula repoPelicula;
    IRepoDirector repoDirector;
    public PeliculasController(IRepoPelicula repoP, IRepoDirector repoD)
    {
        repoPelicula = repoP;
        repoDirector = repoD;
    }

    public async Task<IActionResult> Listado()
    {
        var peliculas = await repoPelicula.ListarAsync();
        return View(peliculas);
    }

    public async Task<IActionResult> Detalle(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var pelicula = await repoPelicula.DetalleAsync(id.GetValueOrDefault());

        if (pelicula is null)
            return NotFound();

        return View(pelicula);
    }

    [HttpGet]
    public async Task<IActionResult> Alta()
    {
        var director = new Director()
        {
            idDirector = 0,
            Nombre = "Nombre",
            Apellido = "Apellido",
            nacionalidad = "Nacionalidad",
            descripcion = "Descripcion",
            FechaNacimiento = new DateTime(2000, 1, 10),
            IMG = "https://i.pinimg.com/originals/53/2a/27/532a270caf5b324c887edd98a5e706d5.gif"
        };
        var pelicula = new Pelicula()
        {
            IdPelicula = 0,
            Nombre = "Nombre",
            Duracion = "Duracion",
            Genero = "Genero",
            Calificacion = "",
            IMG = "https://i.pinimg.com/originals/53/2a/27/532a270caf5b324c887edd98a5e706d5.gif",
            director = director
        };
        VMPeliculaDP vmPelicula= new VMPeliculaDP(pelicula);
        await vmPelicula.traerDirectores(repoDirector);
        vmPelicula.agregarPersonaje(await vmPelicula.crearPersonaje(pelicula.IdPelicula));
        return View("Upsert", vmPelicula);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(Pelicula pelicula)
    {
        if (!ModelState.IsValid)
        {
            return View("Upsert", pelicula);
        }

        if (pelicula.IdPelicula == 0)
        {
            pelicula.IdPelicula = 1;
            await repoPelicula.AltaAsync(pelicula);
        }
        else
        {
            //
        }
        return RedirectToAction(nameof(Listado));
    }
}
