using Actores;
using Personajes;

namespace Ghibli.Persistencia;

public interface IRepoPersonajes : IRepoAlta<Personaje>, IDetalle<Personaje, int>, IListado<Personaje>, IModificar<Personaje>
{
    Task<IEnumerable<Personaje>> PersonajesDeAsync(ActorVoz actorVoz);
}
