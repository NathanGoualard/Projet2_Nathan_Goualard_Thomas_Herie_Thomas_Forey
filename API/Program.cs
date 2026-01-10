using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using System.Security.Cryptography;
using System.Text;

string HashPassword(string password)
{
    using var sha256 = SHA256.Create();
    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
    return Convert.ToBase64String(hashedBytes);
}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    var retryCount = 0;
    while (retryCount < 30)
    {
        try
        {
            context.Database.CanConnect();
            break;
        }
        catch
        {
            retryCount++;
            Thread.Sleep(1000);
        }
    }

    context.Database.EnsureCreated();

    if (!context.Roles.Any())
    {
        var roles = new List<Role>
        {
            new Role { Nom = "Administrateur" },
            new Role { Nom = "Bibliothécaire" },
            new Role { Nom = "Utilisateur" }
        };
        context.Roles.AddRange(roles);
        context.SaveChanges();
    }

    if (!context.Genres.Any())
    {
        var genres = new List<Genre>
        {
            new Genre { Nom = "Roman" },
            new Genre { Nom = "Science-Fiction" },
            new Genre { Nom = "Policier" },
            new Genre { Nom = "Histoire" },
            new Genre { Nom = "Biographie" }
        };
        context.Genres.AddRange(genres);
        context.SaveChanges();
    }

    if (!context.Auteurs.Any())
    {
        var auteurs = new List<Auteur>
        {
            new Auteur { Nom = "Hugo", Prenom = "Victor" },
            new Auteur { Nom = "Asimov", Prenom = "Isaac" },
            new Auteur { Nom = "Christie", Prenom = "Agatha" },
            new Auteur { Nom = "Tolstoï", Prenom = "Léon" },
            new Auteur { Nom = "Mandela", Prenom = "Nelson" }
        };
        context.Auteurs.AddRange(auteurs);
        context.SaveChanges();
    }

    if (!context.Livres.Any())
    {
        var livres = new List<Livre>
        {
            new Livre
            {
                Nom = "Les Misérables",
                Annee = new DateTime(1862, 1, 1),
                Id_Auteurs = 1,
                Id_Genres = 1
            },
            new Livre
            {
                Nom = "Fondation",
                Annee = new DateTime(1951, 1, 1),
                Id_Auteurs = 2,
                Id_Genres = 2
            },
            new Livre
            {
                Nom = "Le Crime de l'Orient-Express",
                Annee = new DateTime(1934, 1, 1),
                Id_Auteurs = 3,
                Id_Genres = 3
            },
            new Livre
            {
                Nom = "Guerre et Paix",
                Annee = new DateTime(1869, 1, 1),
                Id_Auteurs = 4,
                Id_Genres = 4
            },
            new Livre
            {
                Nom = "Un long chemin vers la liberté",
                Annee = new DateTime(1994, 1, 1),
                Id_Auteurs = 5,
                Id_Genres = 5
            }
        };
        context.Livres.AddRange(livres);
        context.SaveChanges();
    }

    if (!context.Stocks.Any())
    {
        var stocks = new List<Stock>
        {
            new Stock { Nb = 5, Id_Livres = 1 },
            new Stock { Nb = 3, Id_Livres = 2 },
            new Stock { Nb = 7, Id_Livres = 3 },
            new Stock { Nb = 2, Id_Livres = 4 },
            new Stock { Nb = 4, Id_Livres = 5 }
        };
        context.Stocks.AddRange(stocks);
        context.SaveChanges();
    }

    if (!context.Utilisateurs.Any())
    {
        var utilisateurs = new List<Utilisateur>
        {
            new Utilisateur
            {
                Nom = "Dupont",
                Prenom = "Jean",
                Login = "admin",
                MotDePasse = HashPassword("admin123"),
                Id_Roles = 1
            },
            new Utilisateur
            {
                Nom = "Martin",
                Prenom = "Marie",
                Login = "marie.martin",
                MotDePasse = HashPassword("biblio123"),
                Id_Roles = 2
            },
            new Utilisateur
            {
                Nom = "Bernard",
                Prenom = "Pierre",
                Login = "pierre.bernard",
                MotDePasse = HashPassword("user123"),
                Id_Roles = 3
            },
            new Utilisateur
            {
                Nom = "Dubois",
                Prenom = "Sophie",
                Login = "sophie.dubois",
                MotDePasse = HashPassword("user123"),
                Id_Roles = 3
            },
            new Utilisateur
            {
                Nom = "Thomas",
                Prenom = "Luc",
                Login = "luc.thomas",
                MotDePasse = HashPassword("user123"),
                Id_Roles = 3
            }
        };
        context.Utilisateurs.AddRange(utilisateurs);
        context.SaveChanges();
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
