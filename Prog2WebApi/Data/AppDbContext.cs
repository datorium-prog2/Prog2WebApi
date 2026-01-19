using Microsoft.EntityFrameworkCore;
using Prog2WebApi.Models;

namespace Prog2WebApi.Data
{
    // AppDbContext aprakta mūsu datubāzi
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        // Norādam kādas tabulas būs mūsu datubāzē
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<User> Users => Set<User>();
    }
}
