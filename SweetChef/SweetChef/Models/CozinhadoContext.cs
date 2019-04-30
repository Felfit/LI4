using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweetChef.Models
{
    public class CozinhadoContext : DbContext
    {
        public DbSet<Receita> Receitas { set; get; }
        public DbSet<Ingrediente> Ingredientes { set; get; }
        public DbSet<Passo> Passos { set; get; }

        public CozinhadoContext(DbContextOptions<CozinhadoContext> options)
              : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //--------------------------N-N 
            modelBuilder.Entity<ReceitaIngrediente>()
                .HasKey(t => new { t.ReceitaId, t.IngredienteId });

            modelBuilder.Entity<ReceitaIngrediente>()
                .HasOne(pt => pt.Receita)
                .WithMany(p => p.ReceitaIngredientes)
                .HasForeignKey(pt => pt.ReceitaId);

            modelBuilder.Entity<ReceitaIngrediente>()
                .HasOne(pt => pt.Ingrediente)
                .WithMany(t => t.ReceitaIngredientes)
                .HasForeignKey(pt => pt.IngredienteId);

            //-------------------------N-N

            modelBuilder.Entity<PassoIngrediente>()
                .HasKey(t => new { t.PassoId, t.IngredienteId });

            modelBuilder.Entity<PassoIngrediente>()
                .HasOne(pt => pt.Passo)
                .WithMany(p => p.PassoIngredientes)
                .HasForeignKey(pt => pt.PassoId);

            modelBuilder.Entity<PassoIngrediente>()
                .HasOne(pt => pt.Ingrediente)
                .WithMany(t => t.PassoIngredientes)
                .HasForeignKey(pt => pt.IngredienteId);

            //------------------------N-1

            modelBuilder.Entity<Passo>()
                .HasOne(t => t.Receita)
                .WithMany(u => u.Passos)
                .HasForeignKey(t => t.ReceitaId);
                //.HasConstraintName("ForeignKey_Receita_Passo");
        }
    }
    public class CozinhadoContextFactory : IDesignTimeDbContextFactory<CozinhadoContext>
    {
        public CozinhadoContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CozinhadoContext>();
            optionsBuilder.UseSqlServer<CozinhadoContext>("Server = desktop-dql09f3; Database = li4; Trusted_Connection = True; MultipleActiveResultSets = true"); ;

            return new CozinhadoContext(optionsBuilder.Options);
        }
    }
}
