using Directores;
using Ghibli.Persistencia;

namespace Ghibli.PersistenciaDapper.Test;

public class RepoDirectorTest : TestBase
{
    readonly IRepoDirector _repoDirector;

    public RepoDirectorTest() : base()
        => _repoDirector = new RepoDirector(Conexion);

    //SIN ASYNC=======================================================================================================================================
    [Fact]
    public void TraerDirector()
    {
        //llamamos a la funcion listar 
        var directors = _repoDirector.Listar();
        // Aseguramos de que haya guardado algo en directors
        Assert.NotEmpty(directors);
        //Pregunto por los directores que se dan de alta "
        Assert.Contains(directors, c => c.Nombre == "Hayao" && c.Apellido == "Miyazaki");
    }

    [Fact]
    public void AltaOK()
    {
        var guillermo = new Director()
        {
            idDirector = 122,
            Nombre = "Guillermo",
            Apellido = "Del Toro",
            nacionalidad = "Peru",
            FechaNacimiento = new DateTime(2011, 6, 10)
        };

        _repoDirector.Alta(guillermo);

        Assert.NotEqual(0, guillermo.idDirector);
    }

    [Fact]
    public void DetalleOK()
    {
        var hayao = _repoDirector.Detalle(1);
        Assert.NotNull(hayao);
        Assert.True(hayao.Nombre == "Hayao" && hayao.Apellido == "Miyazaki");
    }

    //CON ASYNC========================================================================================================================
    [Fact]
    public async Task TraerDirectorAsync()
    {
        //llamamos a la funcion listar 
        var directors = await _repoDirector.ListarAsync();
        // Aseguramos de que haya guardado algo en directors
        Assert.NotEmpty(directors);
        //Pregunto por los directores que se dan de alta "
        Assert.Contains(directors, c => c.Nombre == "Hayao" && c.Apellido == "Miyazaki");
    }

    [Fact]
    public async Task AltaOKAsync()
    {
        var guillermo = new Director()
        {
            idDirector = 122,
            Nombre = "Guillermo Async",
            Apellido = "Del Toro Async",
            nacionalidad = "Mexico",
            FechaNacimiento = new DateTime(2011, 6, 10)
        };

        await _repoDirector.AltaAsync(guillermo);

        Assert.NotEqual(0, guillermo.idDirector);
    }

    [Fact]
    public async Task DetalleOKAsync()
    {
        var hayao = await _repoDirector.DetalleAsync(1);
        Assert.NotNull(hayao);
        Assert.True(hayao.Nombre == "Hayao" && hayao.Apellido == "Miyazaki");
    }
}
