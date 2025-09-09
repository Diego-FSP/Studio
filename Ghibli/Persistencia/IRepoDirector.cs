
using Directores;

namespace Ghibli.Persistencia;

public interface IRepoDirector : IRepoAlta<Director>, IDetalle<Director, int>, IListado<Director>, IModificar<Director>
{
    
}
