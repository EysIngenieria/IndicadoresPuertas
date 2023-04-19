using Microsoft.AspNetCore.Mvc;
using RANO.Model;
using RANO.ProcessData;
using RANO.Services;
using System.Net.Sockets;

namespace RANO.Controllers
{

	[ApiController]
	[Route("IRF")]
	public class IRFController : ControllerBase
	{
		private IRFService service = new IRFService();
		private IRFOProcessData processData = new IRFOProcessData();

		[HttpGet]
		[Route("")]
		public async Task<dynamic> ObtenerIRFGeneral(String fechaInicial, String fechaFinal)
		{
			Console.Write("IRFGEneral");
			List<Ticket> tickets = await service.ObtenerTodosLosAIOyANIOEnUnaFecha(fechaInicial, fechaFinal);
			return new
			{
				Calculo = processData.calculo_IRF_general(tickets).calculoIRF(),
				TotalPuertas = processData.calculo_IRF_general(tickets).getTotalPuertas(),
			};
		}
		[HttpGet]
		[Route("NO-CONTRATISTA")]
		public async Task<dynamic> ObtenerIRFNoContratistaREST(String fechaInicial, String fechaFinal)
		{
			List<Ticket> tickets = service.ObtenerTodosLosAIOyANIOEnUnaFecha(fechaInicial, fechaFinal).Result;
			return new
			{
				Calculo = processData.calculo_IRF_NoContratista(tickets).calculoIRF(),
				TotalPuertas = processData.calculo_IRF_NoContratista(tickets).getTotalPuertas(),
			};
			//return processData.ContarFallasPorPuertaContratista(tickets);
		}
		[HttpGet]
		[Route("CONTRATISTA")]
		public async Task<dynamic> ObtenerIRFContratista(String fechaInicial, String fechaFinal)
		{
			List<Ticket> tickets = service.ObtenerTodosLosAIOyANIOEnUnaFecha(fechaInicial, fechaFinal).Result;
			return new
			{
				Calculo = processData.calculo_IRF_Contratista(tickets).calculoIRF(),
				TotalPuertas = processData.calculo_IRF_Contratista(tickets).getTotalPuertas(),

			};
			//return processData.ContarFallasPorPuertaContratista(tickets);
		}

	}
}
