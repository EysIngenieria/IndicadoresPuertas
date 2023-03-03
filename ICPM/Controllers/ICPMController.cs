
using Microsoft.AspNetCore.Mvc;
using ICPM.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static ICPM.Model.Tickets;

namespace RAIO.Controllers
{
    [ApiController]
    [Route("ICPM")]
    public class ICPMController
    {
        Service service = new Service();
        ProcessData processData = new ProcessData();
        //--------------------------------- RAIO -----------------------------------------------------------

        [HttpGet]
        [Route("")]
        public async Task<dynamic> getRanoAsync(string fechaInicial, string fechaFinal)
        {
            List<Ticket> tickets = service.AIOSAsync(fechaInicial, fechaFinal).Result;

            return processData.RAIO_GENERAL(tickets);
        }






        //--------------------------------- RANO QUE ESTAN A CARGO DEL CONTRATISTA -----------------------------------------------------------


        [HttpGet]
        [Route("contratista")]
        public async Task<dynamic> getRanoContratistaAsync(string fechaInicial, string fechaFinal)
        {
            List<Ticket> tickets = service.AIOSAsync(fechaInicial, fechaFinal).Result;

            return processData.RAIO_CONTRATISTA(tickets);
        }



        //--------------------------------- RANO QUE NO ESTAN A CARGO DEL CONTRATISTA -----------------------------------------------------------

        [HttpGet]
        [Route("no_contratista")]
        public async Task<dynamic> getRanoNoContratistaAsync(string fechaInicial, string fechaFinal)
        {
            List<Ticket> tickets = service.AIOSAsync(fechaInicial, fechaFinal).Result;

            return processData.RAIO_NO_CONTRATISTA(tickets);
        }


    }
}
