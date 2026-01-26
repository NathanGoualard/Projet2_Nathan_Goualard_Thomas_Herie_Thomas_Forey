namespace front.Models
{
    public class Emprunt
{
    public int Id_Emprunts { get; set; }
    public DateTime Date { get; set; }

    public int Id_Stock { get; set; }
    public Stock Stock { get; set; }

    public int Id_Utilisateurs { get; set; }
    public Utilisateur Utilisateur { get; set; }
}

}