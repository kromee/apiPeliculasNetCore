using System;
using System.ComponentModel.DataAnnotations;
using ApiPeliculas.Helpers;
using ApiPeliculas.Validaciones;
using Microsoft.AspNetCore.Mvc;

namespace ApiPeliculas.DTOs
{
    public class PeliculaCrearDTO : PeliculaPatchDTO
    {

        [PesoArchivoValidacion(PesoMaximoEnMegasBytes: 4)]
        [TipoArchivoValidacion(GrupoTipoArchivo.Imagen)]
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType=typeof(TypeBinder<List<int>>))]
        public List<int> GenerosIDs { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorPelicularCrearDTO>>))]
        public List<ActorPelicularCrearDTO> Actores { get; set; }


    }
}

