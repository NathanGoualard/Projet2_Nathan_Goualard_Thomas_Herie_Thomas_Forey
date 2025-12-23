using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeAPI.Models
{
    [Table("Auteurs")]
    public class Auteur
    {
        [Key]
        public int Id_Auteurs { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenom { get; set; }

        public virtual ICollection<Livre> Livres { get; set; }
    }
}
