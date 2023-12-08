using System;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.DTOs
{
	public class GeneroDTO
	{
		public int id { get; set; }
		[Required]
		[StringLength(40)]
		public string? nombre { get; set; }
	}
}

