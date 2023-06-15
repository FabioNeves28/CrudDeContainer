using Microsoft.EntityFrameworkCore;

namespace CrudeDeContainer.Models
{
    public class BancoDeDados : DbContext
    {
        public DbSet<Cliente> Clientes { get; set;}
        public DbSet<Container> Containers { get; set;}
        public DbSet<Movement> Movements { get; set;}
        public DbSet<Report> Reports { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString: @"Data Source=DESKTOP-3QU9K7J\SQLEXPRESS; Initial Catalog=Controle_Container;Integrated Security=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Container>().HasOne(c => c.Cliente).WithMany().HasForeignKey(c => c.ClienteID);
            modelBuilder.Entity<Movement>().HasOne(c => c.Container).WithMany().HasForeignKey(c => c.ContainerID);

            base.OnModelCreating(modelBuilder);

        }
    }
}
