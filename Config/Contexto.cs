using Microsoft.EntityFrameworkCore;

namespace Startup.Config
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Notification> Notifications { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
