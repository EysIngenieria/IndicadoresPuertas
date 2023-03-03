using Microsoft.AspNetCore.Mvc;
using RANO.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static RANO.Model.Tickets;

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
        public async Task<dynamic> getRanoAsync(string fechaInicial, string fechaFinal)
        {
            List<Ticket> tickets= service.ANIOSAsync(fechaInicial, fechaFinal).Result;

            return processData.RANO_GENERAL(tickets);
        }


        

        

        //--------------------------------- RANO QUE ESTAN A CARGO DEL CONTRATISTA -----------------------------------------------------------


        [HttpGet]
        [Route("contratista")]
        public async Task<dynamic> getRanoContratistaAsync(string fechaInicial, string fechaFinal)
        {
            List<Ticket> tickets = service.ANIOSAsync(fechaInicial, fechaFinal).Result;

            return processData.RANO_CONTRATISTA(tickets);
        }

        

        //--------------------------------- RANO QUE NO ESTAN A CARGO DEL CONTRATISTA -----------------------------------------------------------

        [HttpGet]
        [Route("no_contratista")]
        public async Task<dynamic> getRanoNoContratistaAsync(string fechaInicial, string fechaFinal)
        {
            List<Ticket> tickets = service.ANIOSAsync(fechaInicial, fechaFinal).Result;

            return processData.RANO_NO_CONTRATISTA(tickets);
        }

        
    }
}
