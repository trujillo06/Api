using Microsoft.EntityFrameworkCore;
using TortilleriaSucursales.Models;

namespace TortilleriaSucursales.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Sucursal> Sucursales { get; set; }
    }
}
