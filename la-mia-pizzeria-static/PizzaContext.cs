using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace la_mia_pizzeria_static
{
    public class PizzaContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<Category> Categories { get; set; } 
        public DbSet<Ingredients> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=PizzaUeh;Integrated Security=True;TrustServerCertificate=True;");
        }
    }
}
