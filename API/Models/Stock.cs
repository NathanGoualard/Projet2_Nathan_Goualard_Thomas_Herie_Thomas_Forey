using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeAPI.Models
{
    [Table("Stock")]
    public class Stock
    {
        [Key]
        public int Id_Stock { get; set; }

        [Required]
        public int Nb { get; set; }

        [Required]
        public int Id_Livres { get; set; }

        // Navigation properties
        [ForeignKey("Id_Livres")]
        public virtual Livre Livre { get; set; }

        public virtual ICollection<Emprunt> Emprunts { get; set; }
        public virtual ICollection<Retour> Retours { get; set; }
    }
}
