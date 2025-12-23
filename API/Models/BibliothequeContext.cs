using Microsoft.EntityFrameworkCore;

namespace BibliothequeAPI.Models
{
    public class BibliothequeContext : DbContext
    {
        public BibliothequeContext(DbContextOptions<BibliothequeContext> options)
            : base(options)
        {
        }

        public DbSet<Auteur> Auteurs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Livre> Livres { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Emprunt> Emprunts { get; set; }
        public DbSet<Retour> Retours { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Livre>()
                .HasOne(l => l.Auteur)
                .WithMany(a => a.Livres)
                .HasForeignKey(l => l.Id_Auteurs)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Livre>()
                .HasOne(l => l.Genre)
                .WithMany(g => g.Livres)
                .HasForeignKey(l => l.Id_Genres)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Stock>()
                .HasOne(s => s.Livre)
                .WithMany(l => l.Stocks)
                .HasForeignKey(s => s.Id_Livres)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Utilisateur>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Utilisateurs)
                .HasForeignKey(u => u.Id_Roles)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Stock)
                .WithMany(s => s.Emprunts)
                .HasForeignKey(e => e.Id_Stock)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Emprunt>()
                .HasOne(e => e.Utilisateur)
                .WithMany(u => u.Emprunts)
                .HasForeignKey(e => e.Id_Utilisateurs)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Retour>()
                .HasOne(r => r.Stock)
                .WithMany(s => s.Retours)
                .HasForeignKey(r => r.Id_Stock)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Retour>()
                .HasOne(r => r.Utilisateur)
                .WithMany(u => u.Retours)
                .HasForeignKey(r => r.Id_Utilisateurs)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
