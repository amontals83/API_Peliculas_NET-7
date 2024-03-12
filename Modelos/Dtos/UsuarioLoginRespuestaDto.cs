namespace API_Peliculas.Modelos.Dtos
{
    //31º
    public class UsuarioLoginRespuestaDto //ES PARA OBTENER EL USUARIO Y EL TOKEN UNA VEZ LOGUEADO CORRECTAMENTE 
    {
        public Usuario Usuario { get; set; }
        public string Token { get; set; }
    }
}
