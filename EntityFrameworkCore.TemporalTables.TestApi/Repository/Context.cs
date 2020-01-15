using EntityFrameworkCore.TemporalTables.Extensions;
using EntityFrameworkCore.TemporalTables.TestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.TemporalTables.TestApi.Repository
{
    public class Context : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public Context(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Two different ways of setting up temporal entities.
            // First: Configure as default for all.
            modelBuilder.UseTemporalTables(); // Create temporal table for all of your entities by default.

            // Second: Table not temporal by default:
            // modelBuilder.PreventTemporalTables(); // Do not create temporal table for none of your entities by default.
            // modelBuilder.Entity<Student>(s => s.UseTemporalTable()); // Setup entity with temporal table.
        }
    }
}
