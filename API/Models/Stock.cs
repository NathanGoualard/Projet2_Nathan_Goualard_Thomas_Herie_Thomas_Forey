using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Stocks")]
    public class Stock
    {
        [Key]
        public int Id_Stock { get; set; }

        [Required]
        public int Nb { get; set; }

        [Required]
        public int Id_Livres { get; set; }

        [ForeignKey("Id_Livres")]
        public Livre? Livre { get; set; }
    }
}
