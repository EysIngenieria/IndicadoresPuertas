
using Microsoft.AspNetCore.Mvc;
using RAIO.Services;
using RANO.Model;
using RANO.ProcessData;
using RANO.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static RANO.Model.Ticket;

namespace RAIO.Controllers
{
    [ApiController]
    [Route("raio")]
    public class RAIOController: ControllerBase
    {
        RAIOService service = new RAIOService();
        RAIOProcessData processData = new RAIOProcessData();
        //--------------------------------- RAIO -----------------------------------------------------------

        [HttpGet]
        [Route("")]
        public async Task<dynamic> ObtenerRAIOGeneral(string fechaInicial, string fechaFinal)
        {
            List<Ticket> ticket = await service.ObtenerTodosLosRAIOEnUnaFecha(fechaInicial, fechaFinal);
			return new
			{
				Calculo = processData.ObtenerRAIOGeneral(ticket).CacularIndicadorRAIO(),
				Procedimiento = processData.ObtenerRAIOGeneral(ticket).ToString(),
				Ticket_TotaAbiertos = processData.ObtenerRAIOGeneral(ticket).getTAI(),
				Ticket_TotalCerrados = processData.ObtenerRAIOGeneral(ticket).getTCI(),

			};
        }






        //--------------------------------- RANO QUE ESTAN A CARGO DEL CONTRATISTA -----------------------------------------------------------


        [HttpGet]
        [Route("contratista")]
        public async Task<dynamic> ObtenerRAIOContratista(string fechaInicial, string fechaFinal)
        {
            List<Ticket> ticket = await service.ObtenerTodosLosRAIOEnUnaFecha(fechaInicial, fechaFinal);
			Console.WriteLine("Imprime: " + processData.ObtenerRAIONoContratista(ticket));
			return new
			{
				Calculo = processData.ObtenerRAIOContratista(ticket).CacularIndicadorRAIO(),
				Procedimiento = processData.ObtenerRAIOContratista(ticket).ToString(),
				Ticket_TotaAbiertos = processData.ObtenerRAIOContratista(ticket).getTAI(),
				Ticket_TotalCerrados = processData.ObtenerRAIOContratista(ticket).getTCI(),

			};
        }



        //--------------------------------- RANO QUE NO ESTAN A CARGO DEL CONTRATISTA -----------------------------------------------------------

        [HttpGet]
        [Route("no_contratista")]
        public async Task<dynamic> ObtenerRAIONoContratista(string fechaInicial, string fechaFinal)
        {
            List<Ticket> ticket = await service.ObtenerTodosLosRAIOEnUnaFecha(fechaInicial, fechaFinal);
            Console.WriteLine("Imprime: "+processData.ObtenerRAIONoContratista(ticket));
			return new
			{
				Calculo = processData.ObtenerRAIONoContratista(ticket).CacularIndicadorRAIO(),
				Procedimiento = processData.ObtenerRAIONoContratista(ticket).ToString(),
				Ticket_TotaAbiertos = processData.ObtenerRAIONoContratista(ticket).getTAI(),
				Ticket_TotalCerrados = processData.ObtenerRAIONoContratista(ticket).getTCINoContratista(),

			};
        }


    }
}
