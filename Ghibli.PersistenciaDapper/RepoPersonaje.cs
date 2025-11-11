using System.Data;
using Dapper;
using Actores;
using Ghibli.Persistencia;
using Personajes;
using System.Threading.Tasks;

namespace Ghibli.PersistenciaDapper;

public class RepoPersonaje : RepoBase, IRepoPersonajes
{
    static readonly string _listadoPersonajes =
        @"SELECT id_personaje AS idPersonaje, Nombre, id_pelicula AS idPelicula, IMG
        FROM    Personajes";

    static readonly string _listadoPersonajesfrom =
        @"SELECT id_personaje AS idPersonaje, Nombre, id_pelicula AS idPelicula, IMG
        FROM    Personajes
        WHERE id_pelicula =";

    
        
    static readonly string _listadoPersonajesfromActor =
        @"SELECT P.id_personaje AS idPersonaje, P.Nombre, P.id_pelicula AS idPelicula, IMG
        FROM    Personajes P
        inner join personaje_voz PV ON P.id_personaje = PV.id_personaje
        WHERE PV.id_actor = @idActor";

    static readonly string _detallePersonajes = _listadoPersonajes + @"
    where id_personaje = @idPersonaje
    limit 1";
//
    static readonly string _Actor =
    @"Select id_actor
    from personaje_voz
    where id_personaje= @idpersonaje";

    public RepoPersonaje(IDbConnection conexion)
        : base(conexion) { }

    //SIN ASYNC===========================================================================================================
    public void Alta(Personaje personaje)
    {
        //throw new NotImplementedException();

        //Preparo los parametros del Stored Procedure
        var parametros = new DynamicParameters();
        parametros.Add("@unidpersonaje", direction: ParameterDirection.Output);
        parametros.Add("@unidpelicula", personaje.idPelicula);
        parametros.Add("@unnombre", personaje.Nombre);
        parametros.Add("@unIMG", personaje.IMG);
        //parametros.Add("actor", personaje.Actor);

        Conexion.Execute("agregarPer", parametros);
        personaje.idPersonaje = parametros.Get<int>("@unidpersonaje");
        
        var parametrosR = new DynamicParameters();
        parametrosR.Add("@actor", personaje.Actor.IdActor);
        parametrosR.Add("@personaje", personaje.idPersonaje);
        Conexion.Execute("asignarAP", parametrosR);
        //Obtengo el valor de parametro de tipo salida
    }

    public Personaje? Detalle(int idPersonaje)
    {
        var personaje = Conexion.QueryFirst<Personaje>(
            _detallePersonajes,
            new { idPersonaje = idPersonaje });
        return personaje;
    }

    public IEnumerable<Personaje> Listar()
    {
        var personajes = Conexion.Query<Personaje>(_listadoPersonajes);
        return personajes;
    }

    //CON ASYNC=====================================================================================================================================
    public async Task AltaAsync(Personaje personaje)
    {
        //throw new NotImplementedException();

        //Preparo los parametros del Stored Procedure
        var parametros = new DynamicParameters();
        parametros.Add("@unidpersonaje", direction: ParameterDirection.Output);
        parametros.Add("@unidpelicula", personaje.idPelicula);
        parametros.Add("@unnombre", personaje.Nombre);
        parametros.Add("@unIMG", personaje.IMG);
        //parametros.Add("actor", personaje.Actor);

        await Conexion.ExecuteAsync("agregarPer", parametros);
        personaje.idPersonaje = parametros.Get<int>("@unidpersonaje");
        
        var parametrosR = new DynamicParameters();
        parametrosR.Add("@actor", personaje.Actor.IdActor);
        parametrosR.Add("@personaje", personaje.idPersonaje);
        await Conexion.ExecuteAsync("asignarAP", parametrosR);
        //Obtengo el valor de parametro de tipo salida
    }

    public async Task<Personaje?> DetalleAsync(int idPersonaje)
    {
        var personaje = await Conexion.QueryFirstAsync<Personaje>(
            _detallePersonajes,
            new { idPersonaje = idPersonaje });


        RepoActor repoActor = new RepoActor(Conexion);

        int aux = await Conexion.QueryFirstOrDefaultAsync<int>(_Actor,
            new { idPersonaje = personaje.idPersonaje });

            personaje.Actor = (await repoActor.DetalleAsync(aux))!;

        return personaje;
    }

    public async Task<IEnumerable<Personaje>> ListarAsync()
    {
        var personajes = await Conexion.QueryAsync<Personaje>(_listadoPersonajes);
        return personajes;
    }

    public async Task<IEnumerable<Personaje>> ListarfromAsync(int id)
    {
        var personajes = await Conexion.QueryAsync<Personaje>(_listadoPersonajesfrom + id);
        
        RepoActor repoActor = new RepoActor(Conexion);
        foreach (Personaje p in personajes)
        {
            int aux = await Conexion.QueryFirstOrDefaultAsync<int>(_Actor,
            new { idPersonaje = p.idPersonaje });

            p.Actor = (await repoActor.DetalleAsync(aux))!;
        }
        
        return personajes;
    }

    public Task<IEnumerable<Personaje>> ListarfromAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Personaje>> PersonajesDeAsync(ActorVoz actorVoz)
    {
        var personajes = await Conexion.QueryAsync<Personaje>(_listadoPersonajesfromActor, new { idActor = actorVoz.IdActor });
        return personajes;
    }

    public Personaje Modificar(Personaje elemento)
    {
        throw new NotImplementedException();
    }

    public async Task ModificarAsync(Personaje elemento)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidpersonaje", direction: ParameterDirection.Output);
        parametros.Add("@unidpelicula", elemento.idPelicula);
        parametros.Add("@unnombre", elemento.Nombre);
        parametros.Add("@unIMG", elemento.IMG);
        parametros.Add("@unidActor", elemento.Actor.IdActor);
        
        await Conexion.ExecuteAsync("actualizarPer", parametros);
        
    }
}

/*

int aux = await Conexion.QueryFirstOrDefaultAsync<int>(_Actor,
        new { idPersonaje = idPersonaje });

        RepoActor repoActor = new RepoActor(base.Conexion);
        personaje.Actor = await repoActor.DetalleAsync(aux);
______________________________________________________________________________
RepoActor repoActor = new RepoActor(base.Conexion);
        foreach (Personaje p in personajes)
        {
            int aux = await Conexion.QueryFirstOrDefaultAsync<int>(_Actor,
            new { idPersonaje = p.idPersonaje });

            p.Actor = await repoActor.DetalleAsync(aux);
        }

*/