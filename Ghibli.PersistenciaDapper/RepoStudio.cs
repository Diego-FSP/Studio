using System.Data;
using Dapper;
using Actores;
using Ghibli.Persistencia;
using Peli;
using estudio;
using System.Threading.Tasks;

namespace Ghibli.PersistenciaDapper;

public class RepoStudio : RepoBase, IRepoStudio
{
    static readonly string _listadoStudio =
        @"SELECT id_estudio AS idStudio, nombre as Nombre, fecha_fundacion AS FechaFundacion, ubicacion AS Ubicacion
        FROM    Estudio";

    static readonly string _detalleStudio = _listadoStudio + @"
    Where id_estudio= @idStudio
    Limit 1";
    public RepoStudio(IDbConnection conexion)
        : base(conexion) { }

//SIN ASYNC================================================================================================================================================
    public void Alta(Studio studio)
    {
        //throw new NotImplementedException();

        //Preparo los parametros del Stored Procedure
        var parametros = new DynamicParameters();
        
        parametros.Add("@unnombre", studio.Nombre);
        parametros.Add("@unfechafundacion", studio.FechaFundacion);
        parametros.Add("@unubicacion", studio.Ubicacion);
        parametros.Add("@unidstudio", direction: ParameterDirection.Output);
    
        
        Conexion.Execute("NStudio", parametros);
       
        //Obtengo el valor de parametro de tipo salida
        studio.idStudio = parametros.Get<int>("@unidstudio");
    }

    public Studio? Detalle(int idStudio)
    {
        var studio = Conexion.QueryFirst<Studio>(_detalleStudio, new {idStudio = idStudio});
        return studio;
    }

    public IEnumerable<Studio> Listar()
    {
        var studio = Conexion.Query<Studio>(_listadoStudio);
        return studio;
    }

//CON ASYNC=======================================================================================================================================
    public async Task AltaAsync(Studio studio)
    {
        //throw new NotImplementedException();

        //Preparo los parametros del Stored Procedure
        var parametros = new DynamicParameters();
        
        parametros.Add("@unnombre", studio.Nombre);
        parametros.Add("@unfechafundacion", studio.FechaFundacion);
        parametros.Add("@unubicacion", studio.Ubicacion);
        parametros.Add("@unidstudio", direction: ParameterDirection.Output);
    
        
        await Conexion.ExecuteAsync("NStudio", parametros);
       
        //Obtengo el valor de parametro de tipo salida
        studio.idStudio = parametros.Get<int>("@unidstudio");
    }

    public async Task<Studio?> DetalleAsync(int idStudio)
    {
        var studio = await Conexion.QueryFirstAsync<Studio>(_detalleStudio, new {idStudio = idStudio});
        return studio;
    }

    public async Task<IEnumerable<Studio>> ListarAsync()
    {
        var studio = await Conexion.QueryAsync<Studio>(_listadoStudio);
        return studio;
    }
}
