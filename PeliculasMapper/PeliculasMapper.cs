using API_Peliculas.Modelos;
using API_Peliculas.Modelos.Dtos;
using AutoMapper;

//El mapper sirve para relacional cada modelo con su dto y viceversa
namespace API_Peliculas.PeliculasMapper
{
    //10º
    public class PeliculasMapper : Profile 
    {
        public PeliculasMapper()
        {
            CreateMap<Categoria, CategoriaDto>().ReverseMap();
            CreateMap<Categoria, CrearCategoriaDto>().ReverseMap();
            //20º
            CreateMap<Pelicula, PeliculaDto>().ReverseMap();
            //32º
            CreateMap<Usuario, UsuarioDto>().ReverseMap();
        }
    }
}
