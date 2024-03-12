using API_Peliculas.Modelos;
using API_Peliculas.Modelos.Dtos;

namespace API_Peliculas.Repositorio.IRepositorio
{
    //33º
    public interface IUsuarioRepositorio
    {
        ICollection<Usuario> GetUsuarios();
        Usuario GetUsuario(int usuarioId);
        bool IsUniqueUser(string usuario);
        Task<UsuarioLoginRespuestaDto> Login(UsuarioLoginDto usuarioLoginDto);
        Task<Usuario> Registro(UsuarioRegistroDto usuarioRegistroDto);
    }
}

