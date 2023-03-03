
using Microsoft.AspNetCore.Mvc;
using RAIO.Services;
using RANO.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static RANO.Model.Tickets;

namespace RAIO.Controllers
{
    [ApiController]
    [Route("raio")]
    public class Controller
    {
        RAIOService service = new RAIOService();
        RAIOProcessData processData = new RAIOProcessData();
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
