using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int Id_Roles { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; } = string.Empty;
    }
}
