using System.Threading.Tasks;
using Actores;
using Ghibli.Persistencia;
using Ghibli.PersistenciaDapper;
using Microsoft.AspNetCore.Mvc;
using Personajes;

namespace Studio.mvc.Controllers;

public class PersonajesController : Controller
{
    IRepoPersonajes repoPersonaje;
    IRepoActor repoActor;

    public PersonajesController(IRepoPersonajes per, IRepoActor actor)
    {
        repoPersonaje = per;
        repoActor = actor;
    }
}





/*

*/