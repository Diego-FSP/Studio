namespace Ghibli.Persistencia;

public interface IListado<T>
{
    IEnumerable<T> Listar();
    Task<IEnumerable<T>> ListarAsync();
}
