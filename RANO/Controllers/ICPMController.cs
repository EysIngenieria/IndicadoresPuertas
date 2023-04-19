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
	[Route("ICPM")]
	public class ICPMController : ControllerBase
	{
		static string COMPONENTE_PUERTA = "Puerta";
		static string COMPONENTE_ITS = "Componente ITS";
		static string COMPONENTE_RFID = "Componente RFID";

		private ICPMServiceImp serviceImp = new ICPMServiceImp();
		private ICPMProcessData processData = new ICPMProcessData();
		private ImportJSONService processJSON = new ImportJSONService();

		[HttpGet]
		[Route("ObtenerICPM")]
		public async Task<dynamic> ObtenerLosTicketsICPMDesdeUnaFecha(String fechaInicial, String fechaFinal)
		{
			List<Ticket>Ticket = await serviceImp.ObtenerTodosLosTicketsDesdeUnaFecha(fechaInicial, fechaFinal);
			if (Ticket != null)
			{
				return  processData.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOAsync(Ticket).CalcularIndicadorIEPM();
			}
			//List<Ticket> Ticket = serviceImp.GetAll(fechaInicial,fechaFinal).Result;
			return NotFound();
		}

		[HttpGet]
		[Route("ObtenerICPMPuertas")]
		public async Task<dynamic> ObtenerICPMComponentePuerta(String fechaInicial, String fechaFinal)
		{
			List<Ticket> Ticket =  await serviceImp.ObtenerTodosLosTicketsDesdeUnaFecha(fechaInicial, fechaFinal);
			return  processData.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(Ticket, COMPONENTE_PUERTA).CalcularIndicadorIEPM();
		}
		[HttpGet]
		[Route("ObtenerICPMITS")]
		public async Task<dynamic> ObtenerICPMComponenteITS(String fechaInicial, String fechaFinal)
		{
			List<Ticket> Ticket = await  serviceImp.ObtenerTodosLosTicketsDesdeUnaFecha(fechaInicial, fechaFinal);
			return  processData.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(Ticket, COMPONENTE_ITS).CalcularIndicadorIEPM();
		}
		[HttpGet]
		[Route("ObtenerICPMRFID")]
		public async Task<dynamic> ObtenerICPMComponenteRFID(String fechaInicial, String fechaFinal)
		{
			List<Ticket> Ticket = await serviceImp.ObtenerTodosLosTicketsDesdeUnaFecha(fechaInicial, fechaFinal);
			return  processData.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(Ticket, COMPONENTE_RFID).CalcularIndicadorIEPM();
		}

	}


		
}
