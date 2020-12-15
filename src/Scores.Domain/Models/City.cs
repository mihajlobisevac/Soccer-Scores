using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scores.Domain.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        public bool Deactivated { get; set; }

        public int CountryId { get; set; }
    }
}
