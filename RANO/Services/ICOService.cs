using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RANO.Model;
using RANO.ProcessData;
using System.IO;
using System.Net;
using System.Text.Json;

namespace RANO.Services
{
	public class ICOService
	{
		private ImportJSONService service;

		string path = "";

		public List<Ticket> ObtenerTicketsDesdeJSON()
		{
			service = new ImportJSONService();
			return service.getExportTicketFromFile(path);
		}
		public async Task<dynamic> ObtenerTodosLosEventosDeEstacionDesdeUnaFecha(string fechaInicial, string fechaFinal)
		{
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
			var client = new HttpClient(handler);
			string url = "https://192.168.0.79:5022/ticket/GetStationReportByDateAllStationFechas";
			UriBuilder uriBuilder = new UriBuilder(url);
			uriBuilder.Query = $"FechaInicio={fechaInicial}&fechaFinal={fechaFinal}";
			string urlCompleta = uriBuilder.ToString();
			var response = await client.GetAsync(urlCompleta);
			try
			{
				
				if (response.IsSuccessStatusCode)
				{
					var responseStream = await response.Content.ReadAsStringAsync();
					JArray json = JArray.Parse(await response.Content.ReadAsStringAsync());
					Console.Write(json.ToString());
					List<EstacionEntity> estaciones = JsonConvert.DeserializeObject<List<EstacionEntity>>(json.ToString());
					Console.WriteLine("Total Estaciones: " + estaciones.Count);
					foreach (EstacionEntity estacion in estaciones)
					{
						Console.WriteLine("Estacion: "+ estacion);
						Console.WriteLine("Estacion tickets: " + estacion.tickets);
						Console.WriteLine("Estacion eventos: " + estacion.eventos);
						//resultadoTotal += estacion.totalTiempoPuertas();
					}


					return estaciones;
				}
				/*if (response.IsSuccessStatusCode)
				{
					var responseStream = await response.Content.ReadAsStreamAsync();
					JArray json = JArray.Parse(await response.Content.ReadAsStringAsync());
					var estaciones = await System.Text.Json.JsonSerializer.DeserializeAsync<List<EstacionEntity>>(responseStream);
					string estacionesJson = System.Text.Json.JsonSerializer.Serialize(estaciones);
					if (estaciones != null)
					{
						return estaciones;
					}
					else
					{
						return new HttpResponseMessage(HttpStatusCode.NotFound);
					}
				}*/
				else
				{
					string errorMessage = await response.Content.ReadAsStringAsync();
					Console.WriteLine($"Error al hacer la solicitud HTTP: {response.StatusCode} - {errorMessage}");
					return new HttpResponseMessage(HttpStatusCode.NotFound);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error al hacer la solicitud HTTP: {ex.Message}");
				return new HttpResponseMessage(HttpStatusCode.NotFound);



			}
		}
	}
}
