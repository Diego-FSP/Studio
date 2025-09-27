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
}
