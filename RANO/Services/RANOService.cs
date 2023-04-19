
using RANO.Model;
using static RANO.Model.Ticket;
using System.Text.Json;
using RANO.ProcessData;
using System.IO;
using RANO.Constants;

namespace RANO.Services
{
    public class RANOService
    {

        private string urlANIO = ConstantsEntity.ANIO;

		private string path = ConstantsEntity.PATH;

		private ImportJSONService service;

		public async Task<List<Ticket>> ObtenerTodosLosRANOEnUnaFecha(string fechaInicial, string fechaFinal)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var client = new HttpClient(handler);
            string url = urlANIO;

			UriBuilder uriBuilder = new UriBuilder(url);
            uriBuilder.Query = $"fecha_inicial_rango={fechaInicial}&fecha_final_rango={fechaFinal}&tipo_fecha=fecha_apertura";
            string urlCompleta = uriBuilder.ToString();
            var response = await client.GetAsync(urlCompleta); 
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var responseStream = await response.Content.ReadAsStreamAsync();
                List<Ticket> tickets = await JsonSerializer.DeserializeAsync<List<Ticket>>(responseStream);

                return tickets;
            }
            return null;
        }

        public async Task<dynamic> ObtenerTicketsDesdeJSON()
        {
            service = new ImportJSONService();
            return service.getExportTicketFromFile(path);
        }
    }
}
