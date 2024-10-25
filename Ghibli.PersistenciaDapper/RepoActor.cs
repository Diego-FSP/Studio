using System.Data;
using Dapper;
using Actores;
using Ghibli.Persistencia;

namespace Ghibli.PersistenciaDapper;

public class RepoActor : RepoBase, IRepoActor
{
    static readonly string _listadoActores =
        @"SELECT id_actor AS IdActor, nombre, apellido
        FROM    Actor_voz";
    
    static readonly string _detalleActor = _listadoActores + @"
    WHERE id_actor = @idactor
    LIMIT 1";
    public RepoActor(IDbConnection conexion)
        : base(conexion) { }

    public void Alta(ActorVoz actor)
    {
        //throw new NotImplementedException();

        //Preparo los parametros del Stored Procedure
        var parametros = new DynamicParameters();
        parametros.Add("@unnombre", actor.Nombre);
        parametros.Add("@unapellido", actor.Apellido);
        parametros.Add("@unidactor", direction: ParameterDirection.Output);
    
        
        Conexion.Execute("nuevoActor", parametros);
       
        //Obtengo el valor de parametro de tipo salida
        actor.IdActor = parametros.Get<int>("@unidactor");
    }

    public ActorVoz? Detalle(int idActor)
    {
         var actor = Conexion.QueryFirst<ActorVoz>(
            _detalleActor,
            new {idActor = idActor});
        return actor;
    }

    public IEnumerable<ActorVoz> Listar()
    {
        var actores = Conexion.Query<ActorVoz>(_listadoActores);
        return actores;
    }
}
