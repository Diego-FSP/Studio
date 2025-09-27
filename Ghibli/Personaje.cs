using Actores;

namespace Personajes;

public class Personaje
{
    public int idPersonaje { get; set; }
    public required string Nombre { get; set; }
    public int idPelicula { get; set; }
    public required ActorVoz Actor { get; set; }
    public required string IMG { get; set; }
}