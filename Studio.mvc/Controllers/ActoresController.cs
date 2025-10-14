using Actores;
using Ghibli.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Studio.mvc.ViewModel;

namespace Studio.mvc.Controllers;

public class ActoresController : Controller
{
    IRepoActor repo;
    IRepoPersonajes repoPersonajes;
    public ActoresController(IRepoActor actores, IRepoPersonajes personajes)
    {
        repo = actores;
        repoPersonajes = personajes;
    }

    public async Task<IActionResult> Listado()
    {
        var actores = await repo.ListarAsync();
        return View(actores);
    }

    [HttpGet]
    public async Task<IActionResult> Detalle(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var actor = await repo.DetalleAsync(id.GetValueOrDefault());

        if (actor is null)
            return NotFound();

        VMActoresPer vmActoresPer = new VMActoresPer(actor);
        await vmActoresPer.traerPersonajes(repoPersonajes);
        return View(vmActoresPer);
    }

    [HttpGet]
    public IActionResult Alta()
    {
        var actor = new ActorVoz()
        {
            Nombre = "Nombre",
            Apellido = "Apellido",
            IdActor = 0,
            IMG = ""
        };
        return View("Upsert", actor);
    }

    [HttpGet]
    public async Task<IActionResult> ModificarAsync(int? id)
    {
        if (id == 0)
            return NotFound();

        var actor = await repo.DetalleAsync(id.GetValueOrDefault());

        if (actor is null)
            return NotFound();

        return View("Upsert", actor);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(ActorVoz actor)
    {
        if (!ModelState.IsValid)
        {
            return View("Upsert", actor);
        }

        if (actor.IdActor == 0)
        {
            actor.IdActor = 1;
            await repo.AltaAsync(actor);
        }
        else
        {
            await repo.ModificarAsync(actor);
        }
        return RedirectToAction(nameof(Listado));
    }
}
