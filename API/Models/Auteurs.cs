using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Auteurs")]
    public class Auteur
    {
        [Key]
        public int Id_Auteurs { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Prenom { get; set; } = string.Empty;
    }
}
