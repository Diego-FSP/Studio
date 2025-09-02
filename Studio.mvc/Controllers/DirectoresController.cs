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
        var paises = await repo.ListarAsync();
        return View(paises);
    }
}
/* */