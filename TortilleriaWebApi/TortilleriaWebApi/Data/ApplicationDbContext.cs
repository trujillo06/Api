using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TortilleriaWebApi.Models;

namespace TortilleriaWebApi.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
