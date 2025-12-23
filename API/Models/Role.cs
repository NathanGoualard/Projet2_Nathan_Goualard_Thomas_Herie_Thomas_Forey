using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeAPI.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int Id_Roles { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        // Navigation property
        public virtual ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}
