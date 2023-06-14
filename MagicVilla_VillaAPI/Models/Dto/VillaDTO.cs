using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaDTO
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public int SqureFeet { get; set; }
        public int Occupecy { get; set; }
        public String ImageUrl { get; set; }
        public String Amenity { get; set; }

    }
}

