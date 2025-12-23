using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Emprunts")]
    public class Emprunt
    {
        [Key]
        public int Id_Emprunts { get; set; }

        [Required]
        public DateTime Date { get; set; }  

        [Required]
        public int Id_Stock { get; set; }

        [Required]
        public int Id_Utilisateurs { get; set; }

        [ForeignKey("Id_Stock")]
        public Stock? Stock { get; set; }

        [ForeignKey("Id_Utilisateurs")]
        public Utilisateur? Utilisateur { get; set; }
    }
}
