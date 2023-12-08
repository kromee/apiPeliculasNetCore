using System;
using ApiPeliculas.DTOs;
using ApiPeliculas.Entidades;
using ApiPeliculas.Helpers;
using ApiPeliculas.Servicios;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Controllers
{
    [ApiController]
    [Route("api/actores")]

    public class ActoresController : CustomBaseController
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "actores";

        public ActoresController(ApplicationDbContext context, IMapper mapper, IAlmacenadorArchivos almacenadorArchivos) :   base(context, mapper)

        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            return await Get<Actor, ActorDTO>(paginacionDTO);
        }

        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            return await Get<Actor, ActorDTO>(id);
        }

        [HttpPost]
		public async Task<ActionResult> Post([FromForm] ActorCrearDTO actorCrearDTO)
        {
            var entidad = mapper.Map<Actor>(actorCrearDTO);

            if (actorCrearDTO.Foto != null) {
                using (var memoryStream = new MemoryStream()) {
                    await actorCrearDTO.Foto. CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCrearDTO.Foto.FileName);
                    entidad.Foto = await almacenadorArchivos.GuardarArchivo(contenido, extension, contenedor, actorCrearDTO.Foto.ContentType);

                }
            }

            context.Add(entidad);
            await context.SaveChangesAsync();
            var actorDTO = mapper.Map<ActorDTO>(entidad);

            return new CreatedAtRouteResult("obtenerActor", new { id = entidad.Id }, actorDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCrearDTO actorCrearDTO)
        {
            var actorDB = await context.Actores.FirstOrDefaultAsync(x => x.Id == id);
            if (actorDB == null) { return NotFound(); }
            actorDB = mapper.Map(actorCrearDTO, actorDB);

            if (actorCrearDTO.Foto != null) {
                using (var memoryStream = new MemoryStream())
                {
                    await actorCrearDTO.Foto.CopyToAsync(memoryStream);
                    var contenido = memoryStream.ToArray();
                    var extension = Path.GetExtension(actorCrearDTO.Foto.FileName);
                    actorDB.Foto = await almacenadorArchivos.EditarArchivo(contenido, extension, contenedor,
                        actorDB.Foto,
                        actorCrearDTO.Foto.ContentType);
                }
            }
            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDTO> patchDocument)
        {
            return await Patch<Actor, ActorPatchDTO>(id, patchDocument);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Actor>(id);
        }
    }
}

