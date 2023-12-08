using System;
using ApiPeliculas.DTOs;
using ApiPeliculas.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPeliculas.Controllers
{
	[ApiController]
	[Route("api/generos")]
	public class GenerosController : CustomBaseController
    {
		private readonly ApplicationDbContext context;
		private readonly IMapper mapper;


		public GenerosController(ApplicationDbContext context, IMapper mapper):base(context, mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		[HttpGet]
		public async Task<ActionResult<List<GeneroDTO>>> Get() {
            return await Get<Genero, GeneroDTO>();

        }
		[HttpGet("{id}", Name = "obtenerGenero")]
		public async Task<ActionResult<GeneroDTO>> Get(int id)
		{
            return await Get<Genero, GeneroDTO>(id);
        }

		[HttpPost]
		public async Task<ActionResult> Post([FromBody] GeneroCrearDTO generoCrearDTO) {

            return await Post<GeneroCrearDTO, Genero, GeneroDTO>(generoCrearDTO, "obtenerGenero");
        }

		[HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GeneroCrearDTO generoCrearDTO) {
            return await Put<GeneroCrearDTO, Genero>(id, generoCrearDTO);
        }

		[HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await Delete<Genero>(id);
        }


    }

}

