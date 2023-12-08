using System;
using ApiPeliculas.DTOs;
using ApiPeliculas.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;

namespace ApiPeliculas.Helpers
{
	public class AutoMapperProfiles: Profile
	{
		public AutoMapperProfiles(GeometryFactory geometryFactory)
		{
			CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCrearDTO, Genero>();

			CreateMap<Actor, ActorDTO>().ReverseMap();
			CreateMap<ActorCrearDTO, Actor>()
                .ForMember(x => x.Foto, options => options.Ignore());
			CreateMap<ActorPatchDTO, Actor>().ReverseMap();


            CreateMap<Review, ReviewDTO>()
          .ForMember(x => x.NombreUsuario, x => x.MapFrom(y => y.Usuario.UserName));

            CreateMap<ReviewDTO, Review>();
            CreateMap<ReviewCreacionDTO, Review>();



            CreateMap<IdentityUser, UsuarioDTO>();




            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();

            CreateMap<PeliculaCrearDTO, Pelicula>()
                .ForMember(x => x.Poster, options => options.Ignore())
				.ForMember(x=>x.PeliculasGeneros, options=>options.MapFrom(MapPeliculasGeneros))
                .ForMember(x=>x.PeliculasActores, options=>options.MapFrom(MapPeliculasActores));

            CreateMap<Pelicula, PeliculaDetallesDTO>()
                .ForMember(x => x.Generos, options => options.MapFrom(MapPeliculasGeneros))
                .ForMember(x => x.Actores, options => options.MapFrom(MapPeliculasActores));


            CreateMap<PeliculaPatchDTO, Pelicula>().ReverseMap();


            CreateMap<SalaDeCine, SalaDeCineDTO>()
                           .ForMember(x => x.Latitud, x => x.MapFrom(y => y.Ubicacion.Y))
                           .ForMember(x => x.Longitud, x => x.MapFrom(y => y.Ubicacion.X));

            CreateMap<SalaDeCineDTO, SalaDeCine>()
                .ForMember(x => x.Ubicacion, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));

            CreateMap<SalaDeCineCreacionDTO, SalaDeCine>()
                 .ForMember(x => x.Ubicacion, x => x.MapFrom(y =>
                geometryFactory.CreatePoint(new Coordinate(y.Longitud, y.Latitud))));






        }

        private List<ActorPeliculaDetalleDTO> MapPeliculasActores(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO)
        {
            var resultado = new List<ActorPeliculaDetalleDTO>();
            if (pelicula.PeliculasActores == null)
            {
                return resultado;
            }
            foreach (var actorPelicula in pelicula.PeliculasActores)
            {
                resultado.Add(new ActorPeliculaDetalleDTO()
                {
                    ActorId = actorPelicula.ActorId,
                    Personaje = actorPelicula.Personaje,
                    NombrePersona = actorPelicula.Actor.Nombre
                });
            }
            return resultado;
        }



        private List<GeneroDTO> MapPeliculasGeneros(Pelicula pelicula, PeliculaDetallesDTO peliculaDetallesDTO) {
            var resultado = new List<GeneroDTO>();
            if (pelicula.PeliculasActores == null) {
                return resultado;  
            }
            foreach (var generopelicula in pelicula.PeliculasGeneros)
            {
                resultado.Add(new GeneroDTO() { id = generopelicula.GeneroId, nombre = generopelicula.Genero.Nombre });
            }
            return resultado;
        }


        private List<PeliculasGeneros> MapPeliculasGeneros(PeliculaCrearDTO peliculaCrearDTO, Pelicula peliculas)
        {
            var resultado = new List<PeliculasGeneros>();
            if (peliculaCrearDTO.GenerosIDs == null) { return resultado; }
            foreach (var id in peliculaCrearDTO.GenerosIDs)
            {
                resultado.Add(new PeliculasGeneros() { GeneroId = id });

            }

            return resultado;
        }

        private List<PeliculasActores> MapPeliculasActores(PeliculaCrearDTO peliculaCrearDTO, Pelicula peliculas) {
            var resultado = new List<PeliculasActores>();
            if (peliculaCrearDTO.Actores == null) { return resultado; }
            foreach (var actor in peliculaCrearDTO.Actores)
            {
                resultado.Add(new PeliculasActores() { ActorId = actor.ActorId, Personaje =actor.Personaje });

            }
            return resultado;
        }
    }
}

