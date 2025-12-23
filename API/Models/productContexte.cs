using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class productContexte : DbContext
    {
        public productContexte(DbContextOptions<productContexte> option) 
            : base(option)
        { 

        }

        public DbSet<Product> Products { get; set;}
    }
}
