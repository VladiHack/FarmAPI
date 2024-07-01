using Microsoft.EntityFrameworkCore;

namespace FarmAPI.Models
{
    public class FarmDBContext:DbContext
    {
        public FarmDBContext(DbContextOptions<FarmDBContext> options) : base(options)
        {
        }

        public DbSet<Crop> Crops { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      => optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=FarmDB;Trusted_Connection=true;TrustServerCertificate=true;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Crop>()
                .HasOne(c => c.Employee)
                .WithMany(e => e.Crops)
                .HasForeignKey(c => c.EmployeeID);

            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Employee)
                .WithMany(e => e.Animals)
                .HasForeignKey(a => a.EmployeeID);

            modelBuilder.Entity<Equipment>()
                .HasOne(e => e.Employee)
                .WithMany(emp => emp.Equipment)
                .HasForeignKey(e => e.EmployeeID);
        }

    }
}
