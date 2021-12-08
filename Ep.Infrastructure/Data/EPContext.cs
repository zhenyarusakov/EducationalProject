using EP.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EC.Infrastructure.Data
{
    public class EPContext : DbContext
    {
        public EPContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(EPContext).Assembly);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }
    }
}