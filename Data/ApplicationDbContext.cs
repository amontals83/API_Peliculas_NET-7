using API_Peliculas.Modelos;
using Microsoft.EntityFrameworkCore;

namespace API_Peliculas.Data
{
    //3º
    //public class ApplicationDbContext : DbContext
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option) 
        {
        }

        //AGREGAR LOS MODELOS AQUI
        public DbSet<Categoria> Categoria { get; set; } 

        public DbSet<Pelicula> Pelicula { get; set; } //17º -> siguiente paso es un txt
        
        public DbSet<Usuario> Usuario { get; set; } //26º -> siguiente paso es un txt
    }
}
