using RANO.Services;
using Microsoft.AspNetCore.Mvc;
using RANO.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using ICMP.Services;

namespace RestApi.Controllers
{
	[ApiController]
	[Route("ICPM")]
	public class ICPMController : ControllerBase
	{
		static string urlMantenimientoPreventivo = "https://192.168.0.79:5022/ticket/GetMantenimientoPreventivo";
		static string urlFechas = "https://192.168.0.79:5022/ticket/GetByDate";
		static string COMPONENTE_PUERTA = "Puerta";
		static string COMPONENTE_ITS = "Componente ITS";
		static string COMPONENTE_RFID = "Componente RFID";
		static string ESTADO_CERRADO = "Cerrado";

		private ICPMServiceImp serviceImp = new ICPMServiceImp();
		[HttpGet]
		[Route("ObtenerTodosLosTickesEnUnRangoDeFechas")]
		public async Task<dynamic> getTicketsAsync(String fechaInicial, String fechaFinal)
		{
			return await serviceImp.GetAll(fechaInicial, fechaFinal);
		}

		[HttpGet]
		[Route("ObtenerICPM")]
		public async Task<dynamic> getICMPAsync(String fechaInicial, String fechaFinal)
		{
			return await serviceImp.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOAsync(fechaInicial, fechaFinal,false);
		}

		[HttpGet]
		[Route("ObtenerICPMTickets")]
		public async Task<dynamic> getICMPTicketsAsync(String fechaInicial, String fechaFinal)
		{
			return await serviceImp.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOAsync(fechaInicial, fechaFinal,true);
		}


		[HttpGet]
		[Route("ObtenerICPMPuertas")]
		public async Task<dynamic> getICMPPuertasAsync(String fechaInicial, String fechaFinal)
		{
			return await serviceImp.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(fechaInicial, fechaFinal, false,COMPONENTE_PUERTA);
		}

		[HttpGet]
		[Route("ObtenerICPMPuertasTickets")]
		public async Task<dynamic> getICMPPuertasTicketsAsync(String fechaInicial, String fechaFinal)
		{
			return await serviceImp.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(fechaInicial, fechaFinal, true,COMPONENTE_PUERTA);
		}
		[HttpGet]
		[Route("ObtenerICPMITS")]
		public async Task<dynamic> getICMITSAsync(String fechaInicial, String fechaFinal)
		{
			return await serviceImp.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(fechaInicial, fechaFinal, false, COMPONENTE_ITS);
		}

		[HttpGet]
		[Route("ObtenerICPMPuertasITSTickets")]
		public async Task<dynamic> getICMPITSTicketsAsync(String fechaInicial, String fechaFinal)
		{
			return await serviceImp.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(fechaInicial, fechaFinal, true, COMPONENTE_ITS);
		}
		[HttpGet]
		[Route("ObtenerICPMRFID")]
		public async Task<dynamic> getICMPRFIDAsync(String fechaInicial, String fechaFinal)
		{
			return await serviceImp.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(fechaInicial, fechaFinal, false, COMPONENTE_RFID);
		}

		[HttpGet]
		[Route("ObtenerICPMPuertasRFIDTickets")]
		public async Task<dynamic> getICMPRFIDTicketsAsync(String fechaInicial, String fechaFinal)
		{
			return await serviceImp.IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(fechaInicial, fechaFinal, true, COMPONENTE_RFID);
		}

	}


		
}
