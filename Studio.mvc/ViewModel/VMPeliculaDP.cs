using Actores;
using Directores;
using Ghibli.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;
using Peli;
using Personajes;

namespace Studio.mvc.ViewModel;
public class VMPeliculaDP
{
    public int IdPelicula  { get; set; }

    public string Nombre { get; set; }

    public DateTime FechaEstreno { get; set; }

    public string Duracion { get; set; }

    public string Genero { get; set; }

    public string Calificacion { get; set; }

    public int Presupuesto { get; set; }

    public string IMG { get; set; }

    public int idStudio { get; set; }

    public Director director { get; set; }
    //List_______________________________________________________
    public SelectList? Directores { get; set; }
    public IEnumerable<Personaje> Personajes { get; set; } = [];
    public int idDirector { get; set; }

    public VMPeliculaDP()
    {
        IdPelicula = 0;
        director = new Director()
        {
            idDirector = 0,
            Nombre = "Guillermo",
            Apellido = "Franchella",
            nacionalidad = "Peru",
            FechaNacimiento = new DateTime(2011, 6, 10),
            IMG = "de",
            descripcion = "FGFG"
        };
    }

    public VMPeliculaDP(Pelicula pelicula)
    {
        IdPelicula = pelicula.IdPelicula;
        Nombre = pelicula.Nombre;
        FechaEstreno = pelicula.FechaEstreno;
        Duracion = pelicula.Duracion;
        Genero = pelicula.Genero;
        Calificacion = pelicula.Calificacion;
        Presupuesto = pelicula.Presupuesto;
        IMG = pelicula.IMG;
        idStudio = 1;
        director = pelicula.director;
        Personajes = pelicula.Personajes;
    }

    public async Task traerDirectores(IRepoDirector repoDirector)
    {
        IEnumerable<Director> directores = await repoDirector.ListarAsync();
        Directores = new SelectList(directores,
                                    dataTextField: nameof(Director.Nombre),
                                    dataValueField: nameof(Director.idDirector));
    }

    public async Task<Personaje> crearPersonaje(int id)
    {
        ActorVoz actores = new ActorVoz()
        {
            Nombre = "Nombre",
            Apellido = "Apellido",
            IMG = "Ninguno",
        };
        Personaje personaje = new Personaje()
        {
            Nombre = "Nombre",
            Actor = actores,
            IMG = "Ninguno"
        };
        return personaje;
    }

    public void agregarPersonaje(Personaje personaje)
    {
        Personajes.Append(personaje);
    }
}
