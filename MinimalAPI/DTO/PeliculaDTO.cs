using Peli;

namespace MinimalAPI.DTO;

public record struct PeliculaDTO
{
    public int Id { get; set; }
    public string Nombre { get; init; }
    public string Duracion { get; init; }
    public string Genero { get; init; }
    public string Calificacion { get; init; }
    public PeliculaDTO(Pelicula pelicula) =>
        (Id, Nombre, Duracion, Genero, Calificacion) =
        (pelicula.IdPelicula, pelicula.Nombre, pelicula.Duracion, pelicula.Genero, pelicula.Calificacion);
}