using API_Peliculas.Modelos;

namespace API_Peliculas.Repositorio.IRepositorio
{
    //21º
    public interface IPeliculaRepositorio
    {
        ICollection<Pelicula> GetPeliculas();
        Pelicula GetPelicula(int peliculaId);
        bool ExistePelicula(string nombre);
        bool ExistePelicula(int id);
        bool CrearPelicula(Pelicula pelicula);
        bool ActualizarPelicula(Pelicula pelicula);
        bool BorrarPelicula(Pelicula pelicula);
        bool Guardar();

        //METODOS PARA BUSCAR PELICULAS EN CATEGORIAS Y BUSCAR PELICULA POR NOMBRE
        ICollection<Pelicula> GetPeliculasEnCategorias(int catId);
        ICollection<Pelicula> BuscarPeliculas(string nombre);
    }
}
