namespace front.Models
{
    public class Livre
    {
        public int Id_Livres { get; set; }

        public string Nom { get; set; } = string.Empty;

        public DateTime Annee { get; set; }

        public int Id_Auteurs { get; set; }

        public int Id_Genres { get; set; }

        public Auteur Auteur { get; set; } = new();
        public Genre Genre { get; set; } = new();

        public Stock? Stock { get; set; }

    }
}
