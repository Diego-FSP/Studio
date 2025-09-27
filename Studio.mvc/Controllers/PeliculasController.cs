using Ghibli.Persistencia;
using Microsoft.AspNetCore.Mvc;

namespace Studio.mvc.Controllers;

public class PeliculasController : Controller
{
    IRepoPelicula repoPelicula;
    public PeliculasController(IRepoPelicula repoP) => repoPelicula = repoP;

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
}
