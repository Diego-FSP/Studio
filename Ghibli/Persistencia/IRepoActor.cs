using Actores;

namespace Ghibli.Persistencia;

public interface IRepoActor : IRepoAlta<ActorVoz>, IDetalle<ActorVoz, int>, IListado<ActorVoz>, IEliminar<int>, IModificar<ActorVoz>
{
    
}
