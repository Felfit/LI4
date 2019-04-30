using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SweetChef.Models
{
    public class Utilizador
    {
        [Key]
        public int UtilizadorId { set; get; }
        [Required]
        [StringLength(60)]
        public string Nome { set; get; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { set; get; }
        [Required]
        [DataType(DataType.Password)]
        public string Pass { set; get; }
        [DataType(DataType.Date)]
        public string DataNascimento { set; get; }

        // TODO: Adicionar coleções
        // public virtual ICollection<Ementa> EmentaSemanal { get; set; }
        // public virtual ICollection<Opiniao> Opiniao { get; set; }
        // ...
    }

    public class UtilizadorContext : DbContext
    {
        public UtilizadorContext(DbContextOptions<UtilizadorContext> options)
            : base(options)
        {

        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Gerar as foreign keys.
            
            modelBuilder.Entity<Ementa>()
                    .HasOne(t => t.utilizador)
                    .WithMany(u => u.ementa)
                    .HasForeignKey(t => t.user_id)
                    .HasConstraintName("ForeignKey_Utilizador_Ementa");
            
        }
    */
        public DbSet<Utilizador> Utilizadores { get; set; }
    }
}
