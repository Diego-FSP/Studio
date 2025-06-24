namespace Ghibli.Persistencia;

public interface IRepoAlta<T>
{
    void Alta(T elemento);
    Task AltaAsync(T elemento);
}