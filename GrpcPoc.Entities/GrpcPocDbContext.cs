using Microsoft.EntityFrameworkCore;

namespace GrpcPoc.Entities
{
    public class GrpcPocDbContext : DbContext
    {
        public GrpcPocDbContext() { }
        public GrpcPocDbContext(DbContextOptions<GrpcPocDbContext> options) : base(options) { }

        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=GrpcPoc;Integrated Security=True;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasIndex(e => e.FirstName);
            modelBuilder.Entity<Person>()
                .HasIndex(e => e.LastName);
            modelBuilder.Entity<Person>()
                .HasIndex(e => e.MiddleName);
        }
    }
}
