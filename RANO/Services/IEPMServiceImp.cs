using RANO.Constants;
using RANO.Model;
using RANO.ProcessData;
using System.Net;
using System.Reflection.Metadata;
using System.Text.Json;
using static RANO.Model.Ticket;

namespace ICMP.Services
{
	
    public class IEPMServiceImp
    {
		private string urlFechas = ConstantsEntity.URLFECHAS;

		private string path = ConstantsEntity.PATH;

		private ImportJSONService service;
		public async Task<dynamic> ObtenerTodosLosTicketsDesdeUnaFecha(string fechaInicial, string fechaFinal)
		{
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
			var client = new HttpClient(handler);
			string url = urlFechas;
			UriBuilder uriBuilder = new UriBuilder(url);
			uriBuilder.Query = $"fecha_inicial_rango={fechaInicial}&fecha_final_rango={fechaFinal}&tipo_fecha=fecha_apertura";
			string urlCompleta = uriBuilder.ToString();
			var response = await client.GetAsync(urlCompleta);
			if (response.IsSuccessStatusCode)
			{
				var responseStream = await response.Content.ReadAsStreamAsync();

				var tickets = await JsonSerializer.DeserializeAsync<List<Ticket>>(responseStream);
				if (tickets != null)
				{
					return tickets;
				}
				else
				{
					return new HttpResponseMessage(HttpStatusCode.NotFound);
				}
			}
			else
			{
				return new HttpResponseMessage(HttpStatusCode.NotFound);
			}
		}
		public async Task<dynamic> ObtenerTicketsDesdeJSON()
		{
			service = new ImportJSONService();
			return service.getExportTicketFromFile(path);
		}

	}
}
