using System.Numerics;

namespace Ghibli.Persistencia;

public interface IModificar<T>
{
    T Modificar(T elemento);
    Task ModificarAsync(T elemento);
}
