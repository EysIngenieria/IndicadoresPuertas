using ICMP.Services;
using Microsoft.AspNetCore.Mvc;
using RANO.Model;
using RANO.ProcessData;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static RANO.Model.Ticket;

namespace IEPM.Controllers
{
    [ApiController]
    [Route("iepm")]
    public class IEPMController
    {

		private IEPMProcessData processData = new IEPMProcessData();
		private IEPMServiceImp service = new IEPMServiceImp();
		//--------------------------------- IEPM -----------------------------------------------------------
		[HttpGet]
		[Route("")]
		public async Task<dynamic> ObtenerIEPMGeneral(string fechaInicial, string fechaFinal)
		{ 
			List<Ticket> Ticket = await service.ObtenerTodosLosTicketsDesdeUnaFecha(fechaInicial, fechaFinal);
			return processData.IndicadorDeEfectividadDelPlanDeMantenimiento(Ticket).CalcularIndicadorIEPM();
		}

		[HttpGet]
		[Route("Contratista")]
		public async Task<dynamic> ObtenerIEPMContratista(string fechaInicial, string fechaFinal)
		{
			List<Ticket> Ticket = await service.ObtenerTodosLosTicketsDesdeUnaFecha(fechaInicial, fechaFinal);
			return processData.IndicadorDeEfectividadDelPlanDeMantenimientoContratista(Ticket).CalcularIndicadorIEPM();
		}

		[HttpGet]
		[Route("NoContratista")]
		public async Task<dynamic> ObtenerIEPMNoContratista(string fechaInicial, string fechaFinal)
		{
			List<Ticket> Ticket = await service.ObtenerTodosLosTicketsDesdeUnaFecha(fechaInicial, fechaFinal);
			return processData.IndicadorDeEfectividadDelPlanDeMantenimientoNoContratista(Ticket).CalcularIndicadorIEPM();
		}
	}
}
