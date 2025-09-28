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

}

/*
<table class="ListadoTabla">
        <thead>
            <tr>
                <th>IMG</th>
                <th>personaje</th>
            </tr>
        </thead>
        <tbody>
            @foreach(var personaje in Model.personajes)
            {
                <tr>
                    <td>
                        <img src="@personaje.IMG" class="ImgPerfil" alt="Imagen de @personaje.Nombre">
                    </td>
                    <td>
                        <label for="">@personaje.Nombre</label>
                    </td>
                </tr>
            }
        </tbody>
    </table>
*/