using System.Threading.Tasks;
using Actores;
using Ghibli.Persistencia;

namespace Ghibli.PersistenciaDapper.Test;

public class RepoActorTest : TestBase
{
    readonly IRepoActor _repoActor;

    public RepoActorTest() : base()
        => _repoActor = new RepoActor(Conexion);
    [Fact]
//SIN ASYNC========================================================================================
    public void TraerActor()
    {
        var actor = _repoActor.Listar();

        Assert.NotEmpty(actor);
        //Pregunto por rubros que se dan de alta en "scripts/bd/MySQL/03 Inserts.sql"
        Assert.Contains(actor, c => c.Nombre == "Midred" && c.Apellido == "Barrera");
    }

    [Fact]
    public void AltaOK()
    {
        var guillermo = new ActorVoz()
        {
            Nombre = "Guillermo",
            Apellido = "Del Toro",
            IdActor= 122
        };

        _repoActor.Alta(guillermo);

        Assert.NotEqual(0, guillermo.IdActor);
    }
    [Fact]
    public void DetalleOK()
    {
        var  Midred = _repoActor.Detalle(1);
        Assert.NotNull(Midred);
        Assert.True( Midred.Nombre == "Midred" && Midred.Apellido == "Barrera" );
    }
//CON ASYNC=======================================================================================================
    [Fact]
    public async Task TraerActorAsync()
    {
        var actor = await _repoActor.ListarAsync();

        Assert.NotEmpty(actor);
        //Pregunto por rubros que se dan de alta en "scripts/bd/MySQL/03 Inserts.sql"
        Assert.Contains(actor, c => c.Nombre == "Midred" && c.Apellido == "Barrera");
    }

    [Fact]
    public async Task AltaOKAsync()
    {
        var guillermo = new ActorVoz()
        {
            Nombre = "Guillermo",
            Apellido = "Del Toro",
            IdActor= 122
        };

        await _repoActor.AltaAsync(guillermo);

        Assert.NotEqual(0, guillermo.IdActor);
    }
    [Fact]
    public async Task DetalleOKAsync()
    {
        var  Midred = await _repoActor.DetalleAsync(1);
        Assert.NotNull(Midred);
        Assert.True( Midred.Nombre == "Midred" && Midred.Apellido == "Barrera" );
    }    
}
