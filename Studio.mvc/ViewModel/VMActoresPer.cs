using System.Threading.Tasks;
using Actores;
using Ghibli.Persistencia;
using Personajes;

namespace Studio.mvc.ViewModel;

public class VMActoresPer
{
    public ActorVoz actor;
    public IEnumerable<Personaje> personajes { get; set; } = [];

    public VMActoresPer(ActorVoz repoActor)
    {
        actor=repoActor;
    }
    public async Task traerPersonajes(IRepoPersonajes repoPersonajes)
    {
        personajes = await repoPersonajes.PersonajesDeAsync(actor);
    }
}
