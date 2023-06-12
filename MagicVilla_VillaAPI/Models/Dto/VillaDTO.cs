using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MagicVilla_VillaAPI.Models.Dto
{
    public class VillaDTO
    {

        [DefaultValue(0)]
        public int Id { get; set; }
        [Required, NotNull, MaxLength(30)]
        public string Name { get; set; }

    }
}

