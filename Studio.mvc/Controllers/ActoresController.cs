using Ghibli.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Actores;
using Personajes;

namespace Studio.mvc.Controllers;

public class ActoresController : Controller
{
    IRepoActor repo;
    public ActoresController(IRepoActor actores) => repo = actores;

    public async Task<IActionResult> Listado()
    {
        var Actores = await repo.ListarAsync();
        return View(Actores);
    }

    public async Task<IActionResult> Detalle(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var actores = await repo.DetalleAsync(id.GetValueOrDefault());

        if (actores is null)
            return NotFound();

        return View(actores);
    }

}