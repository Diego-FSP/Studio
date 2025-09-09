using System.Diagnostics;
using Ghibli.Persistencia;
using Ghibli.PersistenciaDapper;
using Microsoft.AspNetCore.Mvc;
using Studio.mvc.Models;

namespace Studio.mvc.Controllers;

public class DirectoresController : Controller
{
    IRepoDirector repo;
    public DirectoresController(IRepoDirector director) => repo = director;

    public async Task<IActionResult> Listado()
    {
        var Directores = await repo.ListarAsync();
        return View(Directores);
    }

    public async Task<IActionResult> Detalle(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var director = await repo.DetalleAsync(id.GetValueOrDefault());

        if (director is null)
            return NotFound();

        return View(director);
    }
}
/* */