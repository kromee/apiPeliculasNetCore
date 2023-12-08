using System;
namespace ApiPeliculas.DTOs
{
	public class UserToken
	{
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}

