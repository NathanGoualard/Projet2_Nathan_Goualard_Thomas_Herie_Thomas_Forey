using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Livres")]
    public class Livre
    {
        [Key]
        public int Id_Livres { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        public DateTime Annee { get; set; }

        [Required]
        public int Id_Auteurs { get; set; }

        [Required]
        public int Id_Genres { get; set; }

        [ForeignKey("Id_Auteurs")]
        public Auteur? Auteur { get; set; }

        [ForeignKey("Id_Genres")]
        public Genre? Genre { get; set; }

    }
}
