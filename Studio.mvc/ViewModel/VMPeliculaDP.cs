using System.Linq;
using System.Threading.Tasks;
using Actores;
using Directores;
using Ghibli.Persistencia;
using Peli;
using Personajes;

namespace Studio.mvc.ViewModel;

public class VMPeliculaDP
{
    public Pelicula pelicula;
    public IEnumerable<Director> directores { get; set; } = [];
    public IEnumerable<Personaje> Personajes { get; set; } = [];

    public VMPeliculaDP(Pelicula peli)
    {
        pelicula = peli;
    }

    public async Task traerDirectores(IRepoDirector repoDirector)
    {
        directores = await repoDirector.ListarAsync();
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
