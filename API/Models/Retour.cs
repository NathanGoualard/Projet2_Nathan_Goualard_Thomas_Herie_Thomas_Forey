using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeAPI.Models
{
    [Table("Retours")]
    public class Retour
    {
        [Key]
        public int Id_Retours { get; set; }

        [Required]
        [Column("date_retour")]
        public DateTime DateRetour { get; set; }

        [Required]
        public int Nb { get; set; }

        [Required]
        public int Id_Stock { get; set; }

        [Required]
        public int Id_Utilisateurs { get; set; }

        [ForeignKey("Id_Stock")]
        public virtual Stock Stock { get; set; }

        [ForeignKey("Id_Utilisateurs")]
        public virtual Utilisateur Utilisateur { get; set; }
    }
}
