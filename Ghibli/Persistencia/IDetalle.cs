using System.Numerics;

namespace Ghibli.Persistencia;

public interface IDetalle<T, IS> where IS : IBinaryNumber<IS>
{
    T? Detalle(IS indiceSimple);
    Task<T?> DetalleAsync(IS indiceSimple);
}
