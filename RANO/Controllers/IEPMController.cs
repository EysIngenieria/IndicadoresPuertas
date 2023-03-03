using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using static RANO.Model.Tickets;

namespace IEPM.Controllers
{
    [ApiController]
    [Route("iepm")]
    public class IEPMController
    {
        //--------------------------------- IEPM -----------------------------------------------------------

        [HttpGet]
        [Route("")]
        public async Task<dynamic> getIEPMAsync(string fechaInicial, string fechaFinal)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var client = new HttpClient(handler);
            string url = "https://192.168.0.79:5022/ticket/GetMantenimientoPreventivo";
            UriBuilder uriBuilder = new UriBuilder(url);
            uriBuilder.Query = $"initialDate={fechaInicial}&endDate={fechaFinal}";
            string urlCompleta = uriBuilder.ToString();
            var responsePreventivo = await client.GetAsync(urlCompleta);


            var client1 = new HttpClient(handler);
            string url1 = "https://192.168.0.79:5022/ticket/GetMantenimientoPreventivo";
            UriBuilder uriBuilder1 = new UriBuilder(url1);
            uriBuilder.Query = $"initialDate={fechaInicial}&endDate={fechaFinal}";
            string urlCompleta1 = uriBuilder.ToString();
            var responseCorrectivo = await client.GetAsync(urlCompleta1);







            if (responsePreventivo.IsSuccessStatusCode && responseCorrectivo.IsSuccessStatusCode)
            {
                responsePreventivo.EnsureSuccessStatusCode();

                var responseStreamPreventivo = await responsePreventivo.Content.ReadAsStreamAsync();
                var ticketsPreventivos = await JsonSerializer.DeserializeAsync<List<Ticket>>(responseStreamPreventivo);

                var responseStreamCorrectivos = await responsePreventivo.Content.ReadAsStreamAsync();
                var ticketsCorrectivos = await JsonSerializer.DeserializeAsync<List<Ticket>>(responseStreamCorrectivos);


                List<Ticket> ticketsCerrados = preventivosCerrados(ticketsPreventivos);
                
                double iepm = 0;
                try
                {
                    iepm = Convert.ToDouble(ticketsCerrados.Count - ticketsCorrectivos.Count) / Convert.ToDouble(ticketsCerrados.Count);
                }
                catch (Exception ex)
                {

                }
                return new
                {
                    IEPM = iepm,
                    cantidad_Preventivos_Cerrados = ticketsCerrados.Count,
                    cantidad_Correctivos = ticketsCorrectivos.Count,
                    preventivos_cerrados = ticketsCerrados,
                    correctivos = ticketsCorrectivos
                    

                };

            }


            return new
            {
                hola = "mundo"
            };
        }


        private dynamic preventivosCerrados(List<Ticket>? tickets)
        {


            var ticketGroups = tickets.Where(ticket =>  !ticket.estado_ticket.Equals("null") && ticket.estado_ticket.Equals("Cerrado"))
                .GroupBy(x => x);
            List<Ticket> ticketsc = new List<Ticket>();
            foreach (var group in ticketGroups)
            {
                foreach (var ticket in group)
                {
                    ticketsc.Add(ticket);
                }
            }


            return ticketsc;


        }

        

        //--------------------------------- IEPM QUE ESTAN A CARGO DEL CONTRATISTA -----------------------------------------------------------


        [HttpGet]
        [Route("contratista")]
        public async Task<dynamic> getRanoContratistaAsync(string fechaInicial, string fechaFinal)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var client = new HttpClient(handler);
            string url = "https://192.168.0.79:5022/ticket/GetMantenimientoPreventivo";
            UriBuilder uriBuilder = new UriBuilder(url);
            uriBuilder.Query = $"initialDate={fechaInicial}&endDate={fechaFinal}";
            string urlCompleta = uriBuilder.ToString();
            var responsePreventivo = await client.GetAsync(urlCompleta);


            var client1 = new HttpClient(handler);
            string url1 = "https://192.168.0.79:5022/ticket/GetMantenimientoPreventivo";
            UriBuilder uriBuilder1 = new UriBuilder(url1);
            uriBuilder.Query = $"initialDate={fechaInicial}&endDate={fechaFinal}";
            string urlCompleta1 = uriBuilder.ToString();
            var responseCorrectivo = await client.GetAsync(urlCompleta1);







            if (responsePreventivo.IsSuccessStatusCode && responseCorrectivo.IsSuccessStatusCode)
            {
                responsePreventivo.EnsureSuccessStatusCode();

                var responseStreamPreventivo = await responsePreventivo.Content.ReadAsStreamAsync();
                var ticketsPreventivos = await JsonSerializer.DeserializeAsync<List<Ticket>>(responseStreamPreventivo);
                List<Ticket> ticketsPreventivosContratista = aCargoContratista(ticketsPreventivos);

                var responseStreamCorrectivos = await responsePreventivo.Content.ReadAsStreamAsync();
                var ticketsCorrectivos = await JsonSerializer.DeserializeAsync<List<Ticket>>(responseStreamCorrectivos);
                List<Ticket> ticketsCerrados = preventivosCerrados(aCargoContratista(ticketsPreventivos));

                List<Ticket> ticketsCorrectivosContratista = aCargoContratista(ticketsCorrectivos);

                double iepm = 0;
                try
                {
                    iepm = Convert.ToDouble(ticketsCerrados.Count - ticketsCorrectivosContratista.Count) / Convert.ToDouble(ticketsCerrados.Count);
                }
                catch (Exception ex)
                {

                }
                return new
                {
                    IEPM = iepm,
                    cantidad_Preventivos_Cerrados = ticketsCerrados.Count,
                    cantidad_Correctivos = ticketsCorrectivosContratista.Count,
                    preventivos_cerrados = ticketsCerrados,
                    correctivos = ticketsCorrectivosContratista


                };

            }


            return new
            {
                hola = "mundo"
            };
        }

        private dynamic aCargoContratista(List<Ticket>? tickets)
        {


            var ticketGroups = tickets.Where(ticket => ticket.diagnostico_causa.Equals("A cargo del contratista"))
                .GroupBy(x => x);
            List<Ticket> ticketsc = new List<Ticket>();
            foreach (var group in ticketGroups)
            {
                foreach (var ticket in group)
                {
                    ticketsc.Add(ticket);
                }
            }


            return ticketsc;


        }

        //--------------------------------- IEPM QUE NO ESTAN A CARGO DEL CONTRATISTA -----------------------------------------------------------

        [HttpGet]
        [Route("no_contratista")]
        public async Task<dynamic> getRanoNoContratistaAsync(string fechaInicial, string fechaFinal)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var client = new HttpClient(handler);
            string url = "https://192.168.0.79:5022/ticket/GetMantenimientoPreventivo";
            UriBuilder uriBuilder = new UriBuilder(url);
            uriBuilder.Query = $"initialDate={fechaInicial}&endDate={fechaFinal}";
            string urlCompleta = uriBuilder.ToString();
            var responsePreventivo = await client.GetAsync(urlCompleta);


            var client1 = new HttpClient(handler);
            string url1 = "https://192.168.0.79:5022/ticket/GetMantenimientoPreventivo";
            UriBuilder uriBuilder1 = new UriBuilder(url1);
            uriBuilder.Query = $"initialDate={fechaInicial}&endDate={fechaFinal}";
            string urlCompleta1 = uriBuilder.ToString();
            var responseCorrectivo = await client.GetAsync(urlCompleta1);







            if (responsePreventivo.IsSuccessStatusCode && responseCorrectivo.IsSuccessStatusCode)
            {
                responsePreventivo.EnsureSuccessStatusCode();

                var responseStreamPreventivo = await responsePreventivo.Content.ReadAsStreamAsync();
                var ticketsPreventivos = await JsonSerializer.DeserializeAsync<List<Ticket>>(responseStreamPreventivo);
                List<Ticket> ticketsPreventivosNoContratista = noACargoContratista(ticketsPreventivos);

                var responseStreamCorrectivos = await responsePreventivo.Content.ReadAsStreamAsync();
                var ticketsCorrectivos = await JsonSerializer.DeserializeAsync<List<Ticket>>(responseStreamCorrectivos);
                List<Ticket> ticketsCerrados = preventivosCerrados(noACargoContratista(ticketsPreventivos));

                List<Ticket> ticketsCorrectivosNoContratista = noACargoContratista(ticketsCorrectivos);

                double iepm = 0;
                try
                {
                    iepm = Convert.ToDouble(ticketsCerrados.Count - ticketsCorrectivosNoContratista.Count) / Convert.ToDouble(ticketsCerrados.Count);
                }
                catch (Exception ex)
                {

                }
                return new
                {
                    IEPM = iepm,
                    cantidad_Preventivos_Cerrados = ticketsCerrados.Count,
                    cantidad_Correctivos = ticketsCorrectivosNoContratista.Count,
                    preventivos_cerrados = ticketsCerrados,
                    correctivos = ticketsCorrectivosNoContratista


                };

            }


            return new
            {
                hola = "mundo"
            };
        }

        private dynamic noACargoContratista(List<Ticket>? tickets)
        {


            var ticketGroups = tickets.Where(ticket => !ticket.diagnostico_causa.Equals("A cargo del contratista"))
                .GroupBy(x => x);
            List<Ticket> ticketsc = new List<Ticket>();
            foreach (var group in ticketGroups)
            {
                foreach (var ticket in group)
                {
                    ticketsc.Add(ticket);
                }
            }


            return ticketsc;


        }
    }
}
