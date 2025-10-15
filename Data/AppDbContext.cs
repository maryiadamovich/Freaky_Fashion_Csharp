using Microsoft.EntityFrameworkCore;
using Freaky_Fashion_Api.Domain;

namespace Freaky_Fashion_Api.Data;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { 
    
        }

    public DbSet<Product> Products => Set<Product>();
    }

