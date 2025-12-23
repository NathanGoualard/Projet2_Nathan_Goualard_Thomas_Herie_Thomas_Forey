using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliothequeAPI.Models
{
    [Table("Livres")]
    public class Livre
    {
        [Key]
        public int Id_Livres { get; set; }

        [Required]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [Column("Année")]
        public int Annee { get; set; }

        [Required]
        public int Id_Auteurs { get; set; }

        [Required]
        public int Id_Genres { get; set; }

        [ForeignKey("Id_Auteurs")]
        public virtual Auteur Auteur { get; set; }

        [ForeignKey("Id_Genres")]
        public virtual Genre Genre { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
