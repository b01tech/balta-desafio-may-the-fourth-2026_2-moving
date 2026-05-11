using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class MovingDbContext : DbContext
{
    public MovingDbContext(DbContextOptions<MovingDbContext> options) : base(options) { }

    public DbSet<Caixa> Caixas => Set<Caixa>();
    public DbSet<Item> Itens => Set<Item>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Caixa>(entity =>
        {
            entity.ToTable("Caixas");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Numero).IsRequired();
            entity.Property(e => e.Descricao).HasMaxLength(500);
            entity.HasIndex(e => e.Numero).IsUnique();
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.ToTable("Itens");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Descricao).HasMaxLength(500);
            entity.HasOne(e => e.Caixa)
                  .WithMany(c => c.Itens)
                  .HasForeignKey(e => e.CaixaId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => e.Nome);
        });
    }
}