using System;
using System.ComponentModel.DataAnnotations;
using ApiPeliculas.Validaciones;

namespace ApiPeliculas.DTOs
{
    public class ActorCrearDTO : ActorPatchDTO
    {
    
        [PesoArchivoValidacion(PesoMaximoEnMegasBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo:GrupoTipoArchivo.Imagen)]
        public IFormFile Foto{ get; set; }


    }
}

