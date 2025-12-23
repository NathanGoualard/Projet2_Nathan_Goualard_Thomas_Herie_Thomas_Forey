using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeAPI.Models
{
    [Table("Genres")]
    public class Genre
    {
        [Key]
        public int Id_Genres { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        public virtual ICollection<Livre> Livres { get; set; }
    }
}
