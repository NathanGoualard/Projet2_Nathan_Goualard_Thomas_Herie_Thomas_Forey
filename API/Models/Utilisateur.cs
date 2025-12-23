using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Utilisateurs")]
    public class Utilisateur
    {
        [Key]
        public int Id_Utilisateurs { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Prenom { get; set; } = string.Empty;

        [Required]
        public int Id_Roles { get; set; }

        [ForeignKey("Id_Roles")]
        public Role? Role { get; set; }

    }
}
