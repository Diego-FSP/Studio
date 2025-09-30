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
        
        VMPeliculaDP vmPelicula= new VMPeliculaDP();
        await vmPelicula.traerDirectores(repoDirector);
        return View("Upsert", vmPelicula);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(VMPeliculaDP vmPelicula)
    {
        if (!ModelState.IsValid)
        {
            return View("Upsert", vmPelicula);
        }

        if (vmPelicula.IdPelicula == 0)
        {
            var pelicula = new Pelicula()
            {
                IdPelicula= 1,
                Nombre = vmPelicula.Nombre,
                idStudio = 1,
                FechaEstreno = vmPelicula.FechaEstreno,
                Duracion= vmPelicula.Duracion,
                Genero= vmPelicula.Genero,
                Calificacion= vmPelicula.Calificacion,
                Presupuesto= vmPelicula.Presupuesto,
                IMG= vmPelicula.IMG,
                director= vmPelicula.director
            };
            pelicula.director.idDirector = vmPelicula.idDirector;
            await repoPelicula.AltaAsync(pelicula);
        }
        else
        {
            //
        }
        return RedirectToAction(nameof(Listado));
    }
}
