using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Genres")]
    public class Genre
    {
        [Key]
        public int Id_Genres { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; } = string.Empty;
    }
}
