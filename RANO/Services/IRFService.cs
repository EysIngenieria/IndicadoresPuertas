using RANO.Constants;
using RANO.Model;
using RANO.ProcessData;
using System.Net;
using System.Text.Json;

namespace RANO.Services
{
	public class IRFService
	{

		private string urlANIO = ConstantsEntity.ANIO;

		private string path = ConstantsEntity.PATH;

		private string urlAio = ConstantsEntity.AIO;

		private string urlFechas = ConstantsEntity.URLFECHAS;

		private ImportJSONService service;
		public async Task<dynamic> ObtenerTodosLosTicketsDesdeUnaFecha(string fechaInicial, string fechaFinal)
		{
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
			var client = new HttpClient(handler);
			string url = urlFechas;
			UriBuilder uriBuilder = new UriBuilder(url);
			Console.WriteLine("Test");
			uriBuilder.Query = $"fecha_inicial_rango={fechaInicial}&fecha_final_rango={fechaFinal}&tipo_fecha=fecha_apertura";
			string urlCompleta = uriBuilder.ToString();
			Console.WriteLine(urlCompleta);
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
				response.EnsureSuccessStatusCode();

				var responseStream = await response.Content.ReadAsStreamAsync();
				List<Ticket> tickets = await JsonSerializer.DeserializeAsync<List<Ticket>>(responseStream);

				return tickets;
			}
			return null;
		}

		public async Task<List<Ticket>> ObtenerTodosLosAIOyANIOEnUnaFecha(string fechaInicial, string fechaFinal)
		{
			Console.WriteLine("Entro al metodo obtenerTodosLosAIOyANIOEnUnaFecha");
			var ticketsRANO = await ObtenerTodosLosRANOEnUnaFecha(fechaInicial, fechaFinal);
			var ticketsRAIO = await ObtenerTodosLosRAIOEnUnaFecha(fechaInicial, fechaFinal);
			var allTickets = ticketsRANO.Concat(ticketsRAIO).ToList();

			return allTickets;
		}
		public async Task<dynamic> ObtenerTicketsDesdeJSON()
		{
			service = new ImportJSONService();
			return service.getExportTicketFromFile(path);
		}
	}
}
