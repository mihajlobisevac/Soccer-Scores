using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scores.Domain.Models
{
    public class Club
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "varchar(3)")]
        public string NameCode { get; set; }

        public int YearFounded { get; set; }

        public int VenueId { get; set; }
        public Venue Venue { get; set; }
    }
}
