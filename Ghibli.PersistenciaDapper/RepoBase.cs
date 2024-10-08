using System.Data;

namespace Ghibli.PersistenciaDapper;

public abstract class RepoBase
{
    protected readonly IDbConnection Conexion;
    protected RepoBase(IDbConnection conexion) => Conexion = conexion;
}