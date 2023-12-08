using System;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.DTOs
{
	public class GeneroCrearDTO
	{
        [Required]
        [StringLength(40)]
        public string? nombre { get; set; }
    }
}

