using System;
using System.ComponentModel.DataAnnotations;

namespace ApiPeliculas.Validaciones
{
	public class TipoArchivoValidacion:ValidationAttribute
	{
		private readonly string[] tipoValidos;
		public TipoArchivoValidacion(string[] tiposValidos)
		{

			this.tipoValidos = tiposValidos;
		}
		public TipoArchivoValidacion(GrupoTipoArchivo grupoTipoArchivo)
		{
			if (grupoTipoArchivo == GrupoTipoArchivo.Imagen) {
				tipoValidos = new string[] { "image/jpeg", "image/png", "image/gif" };
			}
		}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
			if (value == null) {
				return ValidationResult.Success;
			}
			IFormFile formFile = value as IFormFile;
			if (formFile == null) {
				return ValidationResult.Success;
			}
			if (!tipoValidos.Contains(formFile.ContentType)) {
				return new ValidationResult($"Solo se acepta {string.Join(", ", tipoValidos)}");
			}


			return ValidationResult.Success;

        }
    }
}

