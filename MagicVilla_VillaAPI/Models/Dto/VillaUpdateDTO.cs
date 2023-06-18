using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }

        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int SqureFeet { get; set; }
        [Required]
        public int Occupecy { get; set; }
        [Required]
        public String ImageUrl { get; set; }
        public String Amenity { get; set; }

    }
}

