namespace Studio.mvc.Controllers
{
    public class PersonajesController : Controllers
    {
        IRepoPersonajes repo;
        public PersonajesController(IRepoPersonajes personajes) => repo = personajes;
    }
    
    public async Task<IActionResult> ListardeActor(ActorVoz actorVoz)
    {
        
        return View(actores);
    }
}