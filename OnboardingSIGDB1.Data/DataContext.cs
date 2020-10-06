using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public DbSet<Empresa> Empresas { get; set; }

   }
}
