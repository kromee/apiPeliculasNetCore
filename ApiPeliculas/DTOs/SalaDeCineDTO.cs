﻿using System;
using ApiPeliculas.Entidades;

namespace ApiPeliculas.DTOs
{
	public class SalaDeCineDTO: IId
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
    }
}

