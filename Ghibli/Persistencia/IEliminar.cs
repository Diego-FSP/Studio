namespace Ghibli.Persistencia;

public interface IEliminar<T>
{
    void Eliminar(int id);
    Task EliminarAsync(int id);
}