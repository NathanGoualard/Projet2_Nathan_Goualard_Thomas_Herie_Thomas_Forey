using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeAPI.Models
{
    [Table("Utilisateurs")]
    public class Utilisateur
    {
        [Key]
        public int Id_Utilisateurs { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        public int Id_Roles { get; set; }

        [ForeignKey("Id_Roles")]
        public virtual Role Role { get; set; }

        public virtual ICollection<Emprunt> Emprunts { get; set; }
        public virtual ICollection<Retour> Retours { get; set; }
    }
}
