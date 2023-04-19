 using Microsoft.AspNetCore.Mvc;
using RANO.Model;
using RANO.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static RANO.Model.Ticket;

namespace RANO.Controllers
{
	[ApiController]
	[Route("rano")]
	public class RANOController
	{
		RANOService service = new RANOService();
		RANOProcessData processData = new RANOProcessData();
		//--------------------------------- RAIO -----------------------------------------------------------

		[HttpGet]
		[Route("")]
		public async Task<dynamic> ObtenerRANOGeneral(string fechaInicial, string fechaFinal)
		{
			List<Ticket> Ticket = await service.ObtenerTodosLosRANOEnUnaFecha(fechaInicial, fechaFinal);
			return new
			{
				Calculo = processData.ObtenerRANOGeneral(Ticket).CalcularIndicadorRANO(),
				Procedimiento = processData.ObtenerRANOGeneral(Ticket).ToString(),
				TicketsAbiertos = processData.ObtenerRANOGeneral(Ticket).getTAN(),
				TicketCerrados = processData.ObtenerRANOGeneral(Ticket).getTCN(),

			};
		}






		//--------------------------------- RANO QUE ESTAN A CARGO DEL CONTRATISTA -----------------------------------------------------------


		[HttpGet]
		[Route("contratista")]
		public async Task<dynamic> ObtenerRANOContratista(string fechaInicial, string fechaFinal)
		{
			List<Ticket> Ticket = await service.ObtenerTodosLosRANOEnUnaFecha(fechaInicial, fechaFinal);
			return new
			{
				Calculo = processData.ObtenerRANOContratista(Ticket).CalcularIndicadorRANO(),
				Procedimiento = processData.ObtenerRANOContratista(Ticket).ToString(),
				TicketsAbiertos = processData.ObtenerRANONoContratista(Ticket).getTAN(),
				TicketCerrados = processData.ObtenerRANOContratista(Ticket).getTCN(),

			};
		}



		//--------------------------------- RANO QUE NO ESTAN A CARGO DEL CONTRATISTA -----------------------------------------------------------

		[HttpGet]
		[Route("no_contratista")]
		public async Task<dynamic> ObtenerRANONoContratista(string fechaInicial, string fechaFinal)
		{
			List<Ticket> Ticket = await service.ObtenerTodosLosRANOEnUnaFecha(fechaInicial, fechaFinal);
			return new
			{
				Calculo = processData.ObtenerRANONoContratista(Ticket).CalcularIndicadorRANO(),
				Procedimiento = processData.ObtenerRANONoContratista(Ticket).ToString(),
				TicketsAbiertos = processData.ObtenerRANONoContratista(Ticket).getTAN(),
				TicketCerrados = processData.ObtenerRANONoContratista(Ticket).getTCN(),

			};
		}


	}
}
