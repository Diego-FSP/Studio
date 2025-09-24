using System.Threading.Tasks;
using Actores;
using Ghibli.Persistencia;
using Microsoft.AspNetCore.Mvc;
using Personajes;

namespace Studio.mvc.Controllers;

    public class PersonajesController : Controller
    {
        IRepoPersonajes repoPersonaje;
        IRepoActor repoActor;
        public PersonajesController(IRepoPersonajes repoPersonaje, IRepoActor repoActor)
            => (this.repoPersonaje, this.repoActor) = (repoPersonaje, repoActor);

        public async Task<IActionResult> DetalleActor(int? idActor)
        {
            if (idActor is null || idActor == 0)
                return NotFound();

            var actores = await repoActor.DetalleAsync(idActor.GetValueOrDefault());

            if (actores is null)
                return NotFound();

            return View();
        }
    }





/*
class Lista
    {
        ActorVoz actor;
        IEnumerable<Personaje> personajes;

        public Lista(ActorVoz a, IEnumerable<Personaje> p)
        {
            actor = a;
            personajes = p;
        }

    }

    if (actor is null)
                return NotFound();            
            var personajes = await repoPersonaje.PersonajesDeAsync(actor);
*/