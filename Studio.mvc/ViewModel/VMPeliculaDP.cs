using Actores;
using Directores;
using Ghibli.Persistencia;
using Microsoft.AspNetCore.Mvc.Rendering;
using Personajes;

namespace Studio.mvc.ViewModel;
public class VMPeliculaDP
{
    public int IdPelicula = 0;

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
