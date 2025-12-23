using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeAPI.Models
{
    [Table("Emprunts")]
    public class Emprunt
    {
        [Key]
        public int Id_Emprunts { get; set; }

        [Required]
        [Column("date_emprunt")]
        public DateTime DateEmprunt { get; set; }

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
