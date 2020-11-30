using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scores.Domain.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(3)")]
        public string NameCode { get; set; }

        [Required]
        [Column(TypeName = "varchar(80)")]
        public string Flag { get; set; }
    }
}
