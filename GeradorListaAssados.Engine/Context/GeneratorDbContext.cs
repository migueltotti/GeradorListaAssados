using GeradorListaAssados.Engine.Models;
using Microsoft.EntityFrameworkCore;

namespace GeradorListaAssados.Engine.Context
{
    public class GeneratorDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public GeneratorDbContext(DbContextOptions<GeneratorDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
                entity.Property(e => e.Price)
                    .IsRequired();
                entity.Property(e => e.Quantity)
                    .IsRequired();
                entity.Property(e => e.Index)
                    .IsRequired();
                entity.Property(e => e.HexCodeColor)
                    .IsRequired();

                entity.HasIndex(e => e.Index)
                    .IsUnique();
            });
        }
    }
}
