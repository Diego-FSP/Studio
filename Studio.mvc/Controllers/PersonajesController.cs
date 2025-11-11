using System.Threading.Tasks;
using Actores;
using Ghibli.Persistencia;
using Ghibli.PersistenciaDapper;
using Microsoft.AspNetCore.Mvc;
using Personajes;
using Studio.mvc.ViewModel;

namespace Studio.mvc.Controllers;

public class PersonajesController : Controller
{
    IRepoPersonajes repoPersonaje;
    IRepoActor repoActor;
    IRepoPelicula repoPelicula;

    public PersonajesController(IRepoPersonajes per, IRepoActor actor, IRepoPelicula Pelicu)
    {
        repoPersonaje = per;
        repoActor = actor;
        repoPelicula = Pelicu;
    }

    public async Task<IActionResult> Listado()
    {
        var personajes = await repoPersonaje.ListarAsync();
        return View(personajes);
    }

    [HttpGet]
    public async Task<IActionResult> Detalle(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var personaje = await repoPersonaje.DetalleAsync(id.GetValueOrDefault());

        if (personaje is null)
            return NotFound();

        return View(personaje);
    }

    [HttpGet]
    public async Task<IActionResult> Alta()
    {
        VMPersonaje vMPersonaje = new VMPersonaje();
        await vMPersonaje.traerPeliculas(repoPelicula);
        await vMPersonaje.traerActores(repoActor);
        return View("Upsert", vMPersonaje);
    }

    [HttpGet]
    public async Task<IActionResult> Modificar(int? id)
    {
        if (id is null || id == 0)
            return NotFound();

        var personaje = await repoPersonaje.DetalleAsync(id.GetValueOrDefault());

        if (personaje is null)
            return NotFound();

        VMPersonaje vmpersonaje = new VMPersonaje(personaje);
        await vmpersonaje.traerPeliculas(repoPelicula);
        await vmpersonaje.traerActores(repoActor);
        return View("Upsert", vmpersonaje);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(VMPersonaje vMPersonaje)
    {
        if (!ModelState.IsValid)
        {
            return View("Upsert", vMPersonaje);
        }

        if (vMPersonaje.idPersonaje == 0)
        {
            
            await repoPersonaje.AltaAsync(vMPersonaje.DevolverPer());
        }else
        {
            await repoPersonaje.ModificarAsync(vMPersonaje.DevolverPer());
        }
        return RedirectToAction(nameof(Listado));
    }

}

