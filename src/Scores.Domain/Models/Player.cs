using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scores.Domain.Models
{
    public class Player
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [Column(TypeName = "varchar(50)")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2)]
        [Column(TypeName = "varchar(50)")]
        public string LastName { get; set; }

        public DateTime DoB { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Position { get; set; }

        public int ShirtNumber { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Foot { get; set; }

        public int ClubId { get; set; }
        public Club Club { get; set; }
        public int NationalityId { get; set; }
        public Country Nationality { get; set; }
        public int PoBId { get; set; }
        public City PoB { get; set; }
    }
}
