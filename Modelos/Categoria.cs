using System.ComponentModel.DataAnnotations;

namespace API_Peliculas.Modelos
{
    //1º
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required] 
        public string Nombre { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}
