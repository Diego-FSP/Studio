using Peli;
using Personajes;

namespace Ghibli.Persistencia;

public interface IRepoPelicula : IRepoAlta<Pelicula>, IDetalle<Pelicula, int>, IListado<Pelicula>, IEliminar<int>
{
    
}
