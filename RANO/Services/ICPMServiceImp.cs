using RANO.Model;
using System.Net;
using System.Text.Json;
using static RANO.Model.Tickets;

namespace ICMP.Services
{
	
    public class ICPMServiceImp
    {
		private string ESTADO_CERRADO = "Cerrado";
		static string COMPONENTE_PUERTA = "Puerta";
		static string COMPONENTE_ITS = "ComponenteITS";
		static string COMPONENTE_RFID = "Equipos RFID";
		private string urlMantenimientoPreventivo = "https://192.168.0.79:5022/ticket/GetMantenimientoPreventivo";
		private string urlFechas = "https://192.168.0.79:5022/ticket/GetByDate";
		public async Task<dynamic> GetAll(string fechaInicial, string fechaFinal)
        {
			var handler = new HttpClientHandler();
			handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
			var client = new HttpClient(handler);
			string url = urlFechas;
			UriBuilder uriBuilder = new UriBuilder(url);
			uriBuilder.Query = $"initialDate={fechaInicial}&endDate={fechaFinal}";
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

		private List<Ticket> GetAllClosed(List<Ticket> tickets)
        {
			var ticketAPEGroup = tickets.Where(ticket =>
				 !string.IsNullOrEmpty(ticket.fecha_apertura) && !string.IsNullOrEmpty(ticket.fecha_cierre)
				 && (DateTime.Parse(ticket.fecha_cierre) - DateTime.Parse(ticket.fecha_apertura)).Hours <= 6
				 && !string.IsNullOrEmpty(ticket.estado_ticket) && ticket.estado_ticket.Equals(ESTADO_CERRADO, StringComparison.Ordinal)
			 ).GroupBy(ticket => ticket);
			List<Ticket> ticketsc = new List<Ticket>();
			foreach (var group in ticketAPEGroup)
			{
				foreach (var ticket in group)
				{
					ticketsc.Add(ticket);
				}
			}

			return ticketsc;
			;
		}
		private List<Ticket> GetTicketWithConditionClosed(List<Ticket> tickets,string condition)
		{
			
			var ticketAPEGroup = tickets.Where(ticket =>
				!string.IsNullOrEmpty(ticket.fecha_apertura) && !string.IsNullOrEmpty(ticket.fecha_cierre)
				&& (DateTime.Parse(ticket.fecha_cierre) - DateTime.Parse(ticket.fecha_apertura)).Hours <= 6
				&& !string.IsNullOrEmpty(ticket.estado_ticket) && ticket.estado_ticket.Equals(ESTADO_CERRADO, StringComparison.Ordinal)
				&& !string.IsNullOrEmpty(ticket.componente) && ticket.componente.StartsWith(condition)
			).GroupBy(ticket => ticket);

			List<Ticket> ticketsc = new List<Ticket>();
			foreach (var group in ticketAPEGroup)
			{
				foreach (var ticket in group)
				{
					Console.WriteLine(ticket);
					ticketsc.Add(ticket);
				}
			}

			return ticketsc;
		}
		private List<Ticket> GetTicketWithConditionNotClosed(List<Ticket> tickets,  string condition)
		{
			

			var ticketAPEGroup = tickets.Where(ticket =>
				!string.IsNullOrEmpty(ticket.fecha_apertura) && !string.IsNullOrEmpty(ticket.fecha_cierre)
				&& (DateTime.Parse(ticket.fecha_cierre) - DateTime.Parse(ticket.fecha_apertura)).Hours <= 6
				&& !string.IsNullOrEmpty(ticket.componente) && ticket.componente.StartsWith(condition)
			).GroupBy(ticket => ticket);

			List<Ticket> ticketsc = new List<Ticket>();
			foreach (var group in ticketAPEGroup)
			{
				foreach (var ticket in group)
				{
					Console.WriteLine(ticket);
					ticketsc.Add(ticket);
				}
			}

			return ticketsc;
		}
		private  List<Ticket> GetTicketWithOutCondition(List<Ticket> tickets)
		{
			return GetAllClosed(tickets);
		}

		public async Task<object> IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOAsync(string fechaInicial, string fechaFinal, bool showTickets)
		{
			List<Ticket> AllTickers = await GetAll(fechaInicial, fechaFinal);
			List<Ticket> ticketsAPE = GetAllClosed(AllTickers);
			List<Ticket> ticketsTAP = GetTicketWithOutCondition(AllTickers);
			double icmpToPercentage = 0;
			if (ticketsTAP.Count > 0)
			{
				double IndicadorDeCumplimientoEnEjecuciondelPlandeMtto = ticketsAPE.Count / ticketsTAP.Count;
				icmpToPercentage = IndicadorDeCumplimientoEnEjecuciondelPlandeMtto * 100;

			}
			else
			{
				icmpToPercentage = 100;
			}
			int AmountOFTAP = ticketsTAP.Count;
			int AmountOFAPE = ticketsAPE.Count;

			if (!showTickets)
			{
				return new
				{
					ICMP = icmpToPercentage,
					Cantidad_TAP = ticketsTAP.Count,
					Cantidad_APE = ticketsAPE.Count,
				};
			}
			else
			{
				return new
				{
					ICMP = icmpToPercentage,
					Cantidad_TAP = ticketsTAP.Count,
					Cantidad_APE = ticketsAPE.Count,
					Tickets_APE = ticketsAPE,
					TicketsTAP = ticketsTAP,
				};
			}
			
		}
		public async Task<object> IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(string fechaInicial, string fechaFinal,bool showTickets,string componente)
		{
			List<Ticket> AllTickers = await GetAll(fechaInicial, fechaFinal);
			List<Ticket> ticketsAPE = GetTicketWithConditionClosed( AllTickers, componente);
			List<Ticket> ticketsTAP = GetTicketWithConditionNotClosed(AllTickers,componente);
			double icmpToPercentage = 0;
			if (ticketsTAP.Count > 0) {
				double IndicadorDeCumplimientoEnEjecuciondelPlandeMtto = ticketsAPE.Count / ticketsTAP.Count;
				icmpToPercentage = IndicadorDeCumplimientoEnEjecuciondelPlandeMtto * 100;

			}
			else
			{
			icmpToPercentage = 100;
			}
			int AmountOFTAP = ticketsTAP.Count;
			int AmountOFAPE = ticketsAPE.Count;

			if (!showTickets)
			{

				return new
				{
					ICMP = icmpToPercentage,
					Cantidad_TAP = AmountOFTAP,
					Cantidad_APE = AmountOFAPE,
				};
			}
			else
			{
				return new
				{
					ICMP = icmpToPercentage,
					Cantidad_TAP = AmountOFTAP,
					Cantidad_APE = AmountOFAPE,
					Tickets_APE = ticketsAPE,
					TicketsTAP = ticketsTAP,
				};
				
			}
		}

	}
}
