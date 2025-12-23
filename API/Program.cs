using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Server=mysql;Port=3306;Database=bibliotheque;User=root;Password=root_password;";
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        if (!context.Auteurs.Any())
        {
            var auteurs = new List<Auteur>
            {
                new Auteur { Nom = "Hugo", Prenom = "Victor" },
                new Auteur { Nom = "Zola", Prenom = "Émile" },
                new Auteur { Nom = "Dumas", Prenom = "Alexandre" },
                new Auteur { Nom = "Verne", Prenom = "Jules" },
                new Auteur { Nom = "Balzac", Prenom = "Honoré" }
            };
            context.Auteurs.AddRange(auteurs);
            context.SaveChanges();

            var genres = new List<Genre>
            {
                new Genre { Nom = "Roman" },
                new Genre { Nom = "Science-Fiction" },
                new Genre { Nom = "Aventure" },
                new Genre { Nom = "Poésie" },
                new Genre { Nom = "Théâtre" }
            };
            context.Genres.AddRange(genres);
            context.SaveChanges();

            var roles = new List<Role>
            {
                new Role { Nom = "Administrateur" },
                new Role { Nom = "Bibliothécaire" },
                new Role { Nom = "Lecteur" }
            };
            context.Roles.AddRange(roles);
            context.SaveChanges();

            var livres = new List<Livre>
            {
                new Livre { Nom = "Les Misérables", Annee = new DateTime(1862, 1, 1), Id_Auteurs = 1, Id_Genres = 1 },
                new Livre { Nom = "Notre-Dame de Paris", Annee = new DateTime(1831, 1, 1), Id_Auteurs = 1, Id_Genres = 1 },
                new Livre { Nom = "Germinal", Annee = new DateTime(1885, 1, 1), Id_Auteurs = 2, Id_Genres = 1 },
                new Livre { Nom = "Le Comte de Monte-Cristo", Annee = new DateTime(1844, 1, 1), Id_Auteurs = 3, Id_Genres = 3 },
                new Livre { Nom = "Les Trois Mousquetaires", Annee = new DateTime(1844, 1, 1), Id_Auteurs = 3, Id_Genres = 3 },
                new Livre { Nom = "Vingt Mille Lieues sous les mers", Annee = new DateTime(1870, 1, 1), Id_Auteurs = 4, Id_Genres = 2 },
                new Livre { Nom = "Le Tour du monde en 80 jours", Annee = new DateTime(1873, 1, 1), Id_Auteurs = 4, Id_Genres = 3 },
                new Livre { Nom = "Le Père Goriot", Annee = new DateTime(1835, 1, 1), Id_Auteurs = 5, Id_Genres = 1 }
            };
            context.Livres.AddRange(livres);
            context.SaveChanges();

            var utilisateurs = new List<Utilisateur>
            {
                new Utilisateur { Nom = "Dupont", Prenom = "Jean", Id_Roles = 1 },
                new Utilisateur { Nom = "Martin", Prenom = "Marie", Id_Roles = 2 },
                new Utilisateur { Nom = "Bernard", Prenom = "Pierre", Id_Roles = 3 },
                new Utilisateur { Nom = "Dubois", Prenom = "Sophie", Id_Roles = 3 },
                new Utilisateur { Nom = "Thomas", Prenom = "Luc", Id_Roles = 3 }
            };
            context.Utilisateurs.AddRange(utilisateurs);
            context.SaveChanges();

            var stocks = new List<Stock>();
            for (int i = 1; i <= livres.Count; i++)
            {
                stocks.Add(new Stock { Nb = Random.Shared.Next(1, 10), Id_Livres = i });
            }
            context.Stocks.AddRange(stocks);
            context.SaveChanges();

            var emprunts = new List<Emprunt>
            {
                new Emprunt { Date = DateTime.Now.AddDays(-10), Id_Stock = 1, Id_Utilisateurs = 3 },
                new Emprunt { Date = DateTime.Now.AddDays(-5), Id_Stock = 2, Id_Utilisateurs = 4 },
                new Emprunt { Date = DateTime.Now.AddDays(-3), Id_Stock = 3, Id_Utilisateurs = 5 }
            };
            context.Emprunts.AddRange(emprunts);
            context.SaveChanges();

            var retours = new List<Retour>
            {
                new Retour { Date = DateTime.Now.AddDays(-2), Id_Stock = 1, Id_Utilisateurs = 3 }
            };
            context.Retours.AddRange(retours);
            context.SaveChanges();

        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erreur: {ex.Message}");
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
