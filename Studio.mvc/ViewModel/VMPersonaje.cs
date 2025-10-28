using Actores;
using Ghibli.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;
using Peli;
using Personajes;

namespace Studio.mvc.ViewModel;

public class VMPersonaje
{
    public SelectList? Actores { get; set; }
    public SelectList? Peliculas { get; set; }

    public int idPersonaje { get; set; }
    public int idPelicula { get; set; }
    public int idActor { get; set; }
    public string Nombre { get; set; }
    public string IMG { get; set; }
    public ActorVoz actor;
    public Pelicula pelicula;

    public VMPersonaje()
    {
        var guill = new ActorVoz()
        {
            Nombre = "Nombre",
            Apellido = "Nombre",
            IdActor = 0,
            IMG = ""
        };
        Nombre = "Nombre";
        idPelicula = 0;
        idPersonaje = 0;
        actor = guill;
        IMG = "";
    }

    public Personaje DevolverPer()
    {
        actor.IdActor = idActor;
        var personaje = new Personaje()
        {
            Nombre = Nombre,
            idPelicula = idPelicula,
            idPersonaje = idPersonaje,
            Actor = actor,
            IMG = IMG
        };

        return personaje;
    }

    public VMPersonaje(Personaje personaje)
    {
        Nombre = personaje.Nombre;
        idPelicula = personaje.idPelicula;
        idPersonaje = personaje.idPersonaje;
        actor = personaje.Actor;
        IMG = personaje.IMG;
    }

    public async Task traerPeliculas(IRepoPelicula repoPelicula)
    {
        IEnumerable<Pelicula> peliculas = await repoPelicula.ListarAsync();
        Peliculas = new SelectList(peliculas,
                                    dataTextField: nameof(Pelicula.Nombre),
                                    dataValueField: nameof(Pelicula.IdPelicula));
    }

    public async Task traerActores(IRepoActor repoActor)
    {
        IEnumerable<ActorVoz> actores = await repoActor.ListarAsync();
        Actores = new SelectList(actores,
                                    dataTextField: nameof(ActorVoz.Nombre),
                                    dataValueField: nameof(ActorVoz.IdActor));
    }

}
