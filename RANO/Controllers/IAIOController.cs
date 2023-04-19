using RANO.Services;
using Microsoft.AspNetCore.Mvc;
using RANO.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Newtonsoft.Json;
using ICMP.Services;
using RANO.ProcessData;
using static RANO.Model.Ticket;
using RAIO.Services;
using System.Net.Sockets;
using System.Runtime.ConstrainedExecution;

namespace RestApi.Controllers
{
	[ApiController]
	[Route("IAIO")]
	public class IAIOController : ControllerBase
	{
        private RAIOService service = new RAIOService();
        private IAIOProcessData processData = new IAIOProcessData();


		[HttpGet]
		[Route("")]
		public async Task<dynamic> ObtenerIAIOGeneral(String fechaInicial, String fechaFinal)
		{
			List<Ticket> Ticket = await service.ObtenerTodosLosRAIOEnUnaFecha(fechaInicial, fechaFinal);

			if (Ticket == null || !Ticket.Any())
			{
				return new
				{
					Error = "No se encontraron tickets en las fechas especificadas"
				};
			}

			var resultado = processData.ObtenerIAIOGeneral(Ticket).CalcularIndicadorIAIO();
			var ticketToShow = processData.ObtenerIAIOGeneral(Ticket);
			Console.WriteLine();
			return new
			{
				Calculo = resultado,
				Tickets = ticketToShow.ToString(),
				Puertas = ticketToShow.getPuertas(),

			};
		}

		[HttpGet]
        [Route("NO-CONTRATISTA")]
        public async Task<dynamic> ObtenerIAIONoContratistaREST(String fechaInicial, String fechaFinal)
        {
            List<Ticket> Ticket = service.ObtenerTodosLosRAIOEnUnaFecha(fechaInicial, fechaFinal).Result;	
			var ticketToShow = processData.ObtenerIAIONoContratista(Ticket);
			var resultado = processData.ObtenerIAIONoContratista(Ticket).CalcularIndicadorIAIO();
			return new
			{
				Calculo = resultado,
				Tickets = ticketToShow.ToString(),
				Puertas = ticketToShow.getPuertas(),

			};
		}
        [HttpGet]
        [Route("CONTRATISTA")]
        public async Task<dynamic> ObtenerIAIOContratista(String fechaInicial, String fechaFinal)
        {
            List<Ticket> Ticket = service.ObtenerTodosLosRAIOEnUnaFecha(fechaInicial, fechaFinal).Result;
			var resultado = processData.ObtenerIAIOContratista(Ticket).CalcularIndicadorIAIO();
			var ticketToShow = processData.ObtenerIAIOContratista(Ticket);
			return new
			{
				Calculo = resultado,
				Tickets = ticketToShow.ToString(),
				Puertas = ticketToShow.getPuertas(),

			};
		}



    }


		
}
