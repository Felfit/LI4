using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SweetChef.ModelsNew
{
    public partial class SweetContext : DbContext
    {
        public SweetContext()
        {
        }

        public SweetContext(DbContextOptions<SweetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CookieAuth> CookieAuth { get; set; }
        public virtual DbSet<Dislikes> Dislikes { get; set; }
        public virtual DbSet<Duvida> Duvida { get; set; }
        public virtual DbSet<EmentaSemanal> EmentaSemanal { get; set; }
        public virtual DbSet<Execucao> Execucao { get; set; }
        public virtual DbSet<Ingrediente> Ingrediente { get; set; }
        public virtual DbSet<Likes> Likes { get; set; }
        public virtual DbSet<Lojas> Lojas { get; set; }
        public virtual DbSet<Nutricao> Nutricao { get; set; }
        public virtual DbSet<Opiniao> Opiniao { get; set; }
        public virtual DbSet<Passo> Passo { get; set; }
        public virtual DbSet<PassoDúvida> PassoDúvida { get; set; }
        public virtual DbSet<PassoIngrediente> PassoIngrediente { get; set; }
        public virtual DbSet<Receita> Receita { get; set; }
        public virtual DbSet<ReceitaIngrediente> ReceitaIngrediente { get; set; }
        public virtual DbSet<RestricoesAlimentares> RestricoesAlimentares { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<TagReceita> TagReceita { get; set; }
        public virtual DbSet<Unidade> Unidade { get; set; }
        public virtual DbSet<Utensilio> Utensilio { get; set; }
        public virtual DbSet<UtensilioPasso> UtensilioPasso { get; set; }
        public virtual DbSet<UtensilioReceita> UtensilioReceita { get; set; }
        public virtual DbSet<Utilizador> Utilizador { get; set; }
        public virtual DbSet<UtilizadorPasso> UtilizadorPasso { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\ProjectsV13;Database=Sweet;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CookieAuth>(entity =>
            {
                entity.HasKey(e => e.Cookie);

                entity.Property(e => e.Cookie).HasColumnName("cookie");

                entity.HasOne(d => d.Utilizador)
                    .WithMany(p => p.CookieAuth)
                    .HasForeignKey(d => d.Utilizadorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKCookieAuth326630");
            });

            modelBuilder.Entity<Dislikes>(entity =>
            {
                entity.HasKey(e => new { e.Utilizadorid, e.Tagid });

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Dislikes)
                    .HasForeignKey(d => d.Tagid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKDislikes606690");

                entity.HasOne(d => d.Utilizador)
                    .WithMany(p => p.Dislikes)
                    .HasForeignKey(d => d.Utilizadorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKDislikes309285");
            });

            modelBuilder.Entity<Duvida>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Explicacao)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ImagemLink)
                    .HasColumnName("imagemLink")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Linkexterno)
                    .HasColumnName("linkexterno")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasColumnName("titulo")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.VideoLink)
                    .HasColumnName("videoLink")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmentaSemanal>(entity =>
            {
                entity.HasKey(e => new { e.Data, e.Receitaid, e.Utilizadorid });

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.HasOne(d => d.Receita)
                    .WithMany(p => p.EmentaSemanal)
                    .HasForeignKey(d => d.Receitaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKEmentaSema403694");

                entity.HasOne(d => d.Utilizador)
                    .WithMany(p => p.EmentaSemanal)
                    .HasForeignKey(d => d.Utilizadorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKEmentaSema418924");
            });

            modelBuilder.Entity<Execucao>(entity =>
            {
                entity.HasKey(e => new { e.Receitaid, e.Utilizadorid, e.Data });

                entity.Property(e => e.Data).HasColumnType("datetime");

                entity.Property(e => e.Dificuldade).HasColumnName("dificuldade");

                entity.Property(e => e.DuracaoTotal).HasColumnName("duracaoTotal");

                entity.Property(e => e.Satisfacao).HasColumnName("satisfacao");

                entity.HasOne(d => d.Receita)
                    .WithMany(p => p.Execucao)
                    .HasForeignKey(d => d.Receitaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKExecucao951778");

                entity.HasOne(d => d.Utilizador)
                    .WithMany(p => p.Execucao)
                    .HasForeignKey(d => d.Utilizadorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKExecucao967008");
            });

            modelBuilder.Entity<Ingrediente>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ImageLink)
                    .IsRequired()
                    .HasColumnName("imageLink")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Unidade)
                    .WithMany(p => p.Ingrediente)
                    .HasForeignKey(d => d.Unidadeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKIngredient791622");
            });

            modelBuilder.Entity<Likes>(entity =>
            {
                entity.HasKey(e => new { e.Utilizadorid, e.Tagid });

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.Tagid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKLikes76896");

                entity.HasOne(d => d.Utilizador)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.Utilizadorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKLikes779490");
            });

            modelBuilder.Entity<Lojas>(entity =>
            {
                entity.HasKey(e => e.StoreNum);

                entity.Property(e => e.StoreNum).HasColumnName("storeNum");

                entity.Property(e => e.Latitude).HasColumnName("latitude");

                entity.Property(e => e.Longitude).HasColumnName("longitude");
            });

            modelBuilder.Entity<Nutricao>(entity =>
            {
                entity.HasKey(e => e.Receitaid);

                entity.Property(e => e.Receitaid).ValueGeneratedNever();

                entity.Property(e => e.Energia).HasColumnName("energia");

                entity.Property(e => e.Gordura).HasColumnName("gordura");

                entity.Property(e => e.HidratosCarbono).HasColumnName("hidratosCarbono");

                entity.HasOne(d => d.Receita)
                    .WithOne(p => p.Nutricao)
                    .HasForeignKey<Nutricao>(d => d.Receitaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKNutricao138526");
            });

            modelBuilder.Entity<Opiniao>(entity =>
            {
                entity.HasKey(e => new { e.Receitaid, e.Utilizadorid });

                entity.Property(e => e.Blacklist).HasColumnName("blacklist");

                entity.Property(e => e.Favorito).HasColumnName("favorito");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.HasOne(d => d.Receita)
                    .WithMany(p => p.Opiniao)
                    .HasForeignKey(d => d.Receitaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOpiniao535745");

                entity.HasOne(d => d.Utilizador)
                    .WithMany(p => p.Opiniao)
                    .HasForeignKey(d => d.Utilizadorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKOpiniao520515");
            });

            modelBuilder.Entity<Passo>(entity =>
            {
                entity.HasKey(e => new { e.Numero, e.Receitaid });

                entity.HasIndex(e => e.Numero)
                    .HasName("Passo_numero")
                    .IsUnique();

                entity.Property(e => e.Numero).HasColumnName("numero");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Duracao).HasColumnName("duracao");

                entity.Property(e => e.ImagemLink)
                    .HasColumnName("imagemLink")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LinkExterno)
                    .HasColumnName("linkExterno")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.VideoLink)
                    .HasColumnName("videoLink")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Receita)
                    .WithMany(p => p.Passo)
                    .HasForeignKey(d => d.Receitaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPasso258598");
            });

            modelBuilder.Entity<PassoDúvida>(entity =>
            {
                entity.HasKey(e => new { e.Passoid, e.PassoReceitaid, e.Dúvidaid });

                entity.ToTable("Passo_Dúvida");

                entity.Property(e => e.Questao)
                    .IsRequired()
                    .HasColumnName("questao")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Dúvida)
                    .WithMany(p => p.PassoDúvida)
                    .HasForeignKey(d => d.Dúvidaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPasso_Dúvi371016");

                entity.HasOne(d => d.Passo)
                    .WithMany(p => p.PassoDúvida)
                    .HasForeignKey(d => new { d.Passoid, d.PassoReceitaid })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPasso_Dúvi626074");
            });

            modelBuilder.Entity<PassoIngrediente>(entity =>
            {
                entity.HasKey(e => new { e.Passoid, e.PassoReceitaid, e.Ingredienteid });

                entity.ToTable("Passo_Ingrediente");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.HasOne(d => d.Ingrediente)
                    .WithMany(p => p.PassoIngrediente)
                    .HasForeignKey(d => d.Ingredienteid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPasso_Ingr655863");

                entity.HasOne(d => d.Passo)
                    .WithMany(p => p.PassoIngrediente)
                    .HasForeignKey(d => new { d.Passoid, d.PassoReceitaid })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKPasso_Ingr754952");
            });

            modelBuilder.Entity<Receita>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasColumnName("descricao")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Dificuldade).HasColumnName("dificuldade");

                entity.Property(e => e.ImagemLink)
                    .IsRequired()
                    .HasColumnName("imagemLink")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Porcoes).HasColumnName("porcoes");

                entity.Property(e => e.Tempodeespera).HasColumnName("tempodeespera");

                entity.Property(e => e.Tempodepreparacao).HasColumnName("tempodepreparacao");

                entity.Property(e => e.VideoLink)
                    .IsRequired()
                    .HasColumnName("videoLink")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReceitaIngrediente>(entity =>
            {
                entity.HasKey(e => new { e.Receitaid, e.Ingredienteid });

                entity.ToTable("Receita_Ingrediente");

                entity.Property(e => e.Quantidade).HasColumnName("quantidade");

                entity.HasOne(d => d.Ingrediente)
                    .WithMany(p => p.ReceitaIngrediente)
                    .HasForeignKey(d => d.Ingredienteid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReceita_In758818");

                entity.HasOne(d => d.Receita)
                    .WithMany(p => p.ReceitaIngrediente)
                    .HasForeignKey(d => d.Receitaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKReceita_In101998");
            });

            modelBuilder.Entity<RestricoesAlimentares>(entity =>
            {
                entity.HasKey(e => new { e.Utilizadorid, e.Ingredienteid });

                entity.HasOne(d => d.Ingrediente)
                    .WithMany(p => p.RestricoesAlimentares)
                    .HasForeignKey(d => d.Ingredienteid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKRestricoes283234");

                entity.HasOne(d => d.Utilizador)
                    .WithMany(p => p.RestricoesAlimentares)
                    .HasForeignKey(d => d.Utilizadorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKRestricoes611183");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Tag1)
                    .IsRequired()
                    .HasColumnName("tag")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TagReceita>(entity =>
            {
                entity.HasKey(e => new { e.Receitaid, e.Tagid });

                entity.ToTable("Tag_Receita");

                entity.HasOne(d => d.Receita)
                    .WithMany(p => p.TagReceita)
                    .HasForeignKey(d => d.Receitaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTag_Receit644400");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TagReceita)
                    .HasForeignKey(d => d.Tagid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKTag_Receit609365");
            });

            modelBuilder.Entity<Unidade>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Utensilio>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ImageLink)
                    .HasColumnName("imageLink")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UtensilioPasso>(entity =>
            {
                entity.HasKey(e => new { e.Passoid, e.PassoReceitaid, e.Utensilioid });

                entity.ToTable("Utensilio_Passo");

                entity.HasOne(d => d.Utensilio)
                    .WithMany(p => p.UtensilioPasso)
                    .HasForeignKey(d => d.Utensilioid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUtensilio_366619");

                entity.HasOne(d => d.Passo)
                    .WithMany(p => p.UtensilioPasso)
                    .HasForeignKey(d => new { d.Passoid, d.PassoReceitaid })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUtensilio_754016");
            });

            modelBuilder.Entity<UtensilioReceita>(entity =>
            {
                entity.HasKey(e => new { e.Receitaid, e.Utensilioid });

                entity.ToTable("Utensilio_Receita");

                entity.HasOne(d => d.Receita)
                    .WithMany(p => p.UtensilioReceita)
                    .HasForeignKey(d => d.Receitaid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUtensilio_681330");

                entity.HasOne(d => d.Utensilio)
                    .WithMany(p => p.UtensilioReceita)
                    .HasForeignKey(d => d.Utensilioid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUtensilio_804784");
            });

            modelBuilder.Entity<Utilizador>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("Utilizador");

                entity.HasIndex(e => e.Password)
                    .HasName("UQ__Utilizad__6E2DBEDEED74F154")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DataNascimento)
                    .HasColumnName("dataNascimento")
                    .HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UtilizadorPasso>(entity =>
            {
                entity.HasKey(e => new { e.Utilizadorid, e.Passoid, e.PassoReceitaid });

                entity.ToTable("Utilizador_Passo");

                entity.Property(e => e.Comentario)
                    .IsRequired()
                    .HasColumnName("comentario")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Utilizador)
                    .WithMany(p => p.UtilizadorPasso)
                    .HasForeignKey(d => d.Utilizadorid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUtilizador291789");

                entity.HasOne(d => d.Passo)
                    .WithMany(p => p.UtilizadorPasso)
                    .HasForeignKey(d => new { d.Passoid, d.PassoReceitaid })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKUtilizador479350");
            });
        }
    }
}
