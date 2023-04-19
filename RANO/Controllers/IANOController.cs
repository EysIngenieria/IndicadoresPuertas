using RANO.Services;
using Microsoft.AspNetCore.Mvc;
using RANO.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using ICMP.Services;
using RANO.ProcessData;
using static RANO.Model.Ticket;

namespace RestApi.Controllers
{
	[ApiController]
	[Route("IANO")]
	public class IANOController : ControllerBase
	{
        private RANOService service = new RANOService();
        private IANOProcessData processData = new IANOProcessData();


        
		[HttpGet]
		[Route("")]
		public async Task<dynamic> ObtenerIANODesdeREST(String fechaInicial, String fechaFinal)
		{
			List<Ticket> Ticket = await service.ObtenerTodosLosRANOEnUnaFecha(fechaInicial, fechaFinal);
			var ticketToShow = processData.OBtenerIndicadorIANO(Ticket);
			var resultado = processData.OBtenerIndicadorIANO(Ticket).CalcularIndicadorIANO();
			return new
			{
				Calculo = resultado,
				Tickets = ticketToShow.ToString(),
				Puertas = ticketToShow.getPuertas(),

			};
		}
        [HttpGet]
        [Route("NO-CONTRATISTA")]
        public async Task<dynamic> ObtenerIANODesdeRESTNoContratista(String fechaInicial, String fechaFinal)
        {
            List<Ticket> Ticket = await service.ObtenerTodosLosRANOEnUnaFecha(fechaInicial, fechaFinal);
			var ticketToShow = processData.ObtenerIndicadorIANONoContratista(Ticket);
			var resultado = processData.ObtenerIndicadorIANONoContratista(Ticket).CalcularIndicadorIANO();
			return new
			{
				Calculo = resultado,
				Tickets = ticketToShow.ToString(),
				Puertas = ticketToShow.getPuertas(),

			};
        }
        [HttpGet]
        [Route("CONTRATISTA")]
        public async Task<dynamic> ObtenerIANODesdeRESTContratista(String fechaInicial, String fechaFinal)
        {
            List<Ticket> Ticket = await service.ObtenerTodosLosRANOEnUnaFecha(fechaInicial, fechaFinal);
			var ticketToShow = processData.ObtenerIndicadorIANONoContratista(Ticket);
			var resultado = processData.ObtenerIndicadorIANONoContratista(Ticket).CalcularIndicadorIANO();
			return new
			{
				Calculo = resultado,
				Tickets = ticketToShow.ToString(),
				Puertas = ticketToShow.getPuertas(),

			};
        }



    }


		
}
