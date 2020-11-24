using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Models.Classes;
using OnBoardingSIGDB1.Models.Classes;

namespace OnboardingSIGDB1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<Carro> Empresas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<RegistroKilometragem> RegistroKilometragens { get; set; }
    }
}
