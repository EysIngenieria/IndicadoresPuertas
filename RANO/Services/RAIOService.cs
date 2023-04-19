using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RANO.Constants;
using RANO.Model;
using RANO.ProcessData;
using System.IO;
using System.Text.Json;

namespace RAIO.Services
{
    public class RAIOService

    {
		private string urlAio = ConstantsEntity.AIO;

        private string path = ConstantsEntity.PATH;

		private ImportJSONService service;
		public async Task<List<Ticket>> ObtenerTodosLosRAIOEnUnaFecha(string fechaInicial, string fechaFinal)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var client = new HttpClient(handler);
            string url = urlAio;
            UriBuilder uriBuilder = new UriBuilder(url);
			uriBuilder.Query = $"fecha_inicial_rango={fechaInicial}&fecha_final_rango={fechaFinal}&tipo_fecha=fecha_apertura";
			string urlCompleta = uriBuilder.ToString();
            var response = await client.GetAsync(urlCompleta);

			if (response.IsSuccessStatusCode)
            {
				var responseStream = await response.Content.ReadAsStringAsync();

				JArray json = JArray.Parse(await response.Content.ReadAsStringAsync());
				Console.WriteLine("D " + responseStream);
				List<Ticket> tickets = JsonConvert.DeserializeObject<List<Ticket>>(responseStream); // Deserializa la cadena JSON en una lista de objetos Ticket
				Console.WriteLine("Total tickets: " + tickets.Count);


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
