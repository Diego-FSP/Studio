using Directores;
using Ghibli.Persistencia;
using Microsoft.AspNetCore.Mvc;

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
            FechaNacimiento = new DateTime(2000, 1, 10)
        };
        return View("Upsert", director);
    }
    
    [HttpGet]
    public async Task<IActionResult> Modificar(int id)
    {
        if (id == 0)
            return NotFound();

        var director = await repo.DetalleAsync(id);

        if (director is null)
            return NotFound();

        return View("Upsert", director);
    }


    [HttpPost]
    public async Task<IActionResult> Upsert(Director director)
    {
        if (!ModelState.IsValid)
        {
            return View("Upsert", director);
        }

        if (director.idDirector == 0)
        {
            director.idDirector = 1;
            await repo.AltaAsync(director);
        }
        else
        {
            await repo.ModificarAsync(director);
        }
        return RedirectToAction(nameof(Listado));
    }
    

}
/* */