using RANO.Model;
using RANO.ProcessData.Interfaces;
using System.Net.Sockets;


namespace RANO.ProcessData
{
	public class IEPMProcessData : IEPMInterface
	{
		private readonly string ESTADO_CERRADO = "Cerrado";

		private readonly string TIPO_MANTENIMIENTO_CORRECTIVO = "Mantenimiento Correctivo";

		private readonly string TIPO_MANTENIMIENTO_PREVENTIVO = "Mantenimiento Preventivo";

		private readonly string CONTRATISTA = "A cargo del contratista";

		public List<Ticket> ObtenerTodosLosMantenimientos(List<Ticket> tickets)
		{
			var ticketAPEGroup = tickets.Where(tickets =>
							  !string.IsNullOrEmpty(tickets.estado_ticket) && tickets.estado_ticket.Equals(ESTADO_CERRADO, StringComparison.Ordinal) && !string.IsNullOrEmpty(tickets.tipo_mantenimiento)
							  && tickets.tipo_mantenimiento.Equals(TIPO_MANTENIMIENTO_CORRECTIVO, StringComparison.Ordinal) ||
							  !string.IsNullOrEmpty(tickets.tipo_mantenimiento) && tickets.tipo_mantenimiento.Equals(TIPO_MANTENIMIENTO_PREVENTIVO, StringComparison.Ordinal)
						 ).GroupBy(ticket => ticket);
			List<Ticket> ticketsTodosLosMantenimientos = new List<Ticket>();
			foreach (var group in ticketAPEGroup)
			{
				foreach (var ticket in group)
				{
					ticketsTodosLosMantenimientos.Add(ticket);
				}
			}

			return ticketsTodosLosMantenimientos;
		}
		public List<Ticket> ObtenerTodosLosTicketsCerrados(List<Ticket> Ticket)
		{
			var ticketAPEGroup = Ticket.Where(ticket =>
				 (ticket.fecha_cierre.Value - ticket.fecha_apertura.Value).TotalHours <= 6
				 && !string.IsNullOrEmpty(ticket.estado_ticket) && ticket.estado_ticket.Equals(ESTADO_CERRADO, StringComparison.Ordinal)
			 ).GroupBy(ticket => ticket);
			List<Ticket> ticketsTodosLosTicketsCerrados = new List<Ticket>();
			foreach (var group in ticketAPEGroup)
			{
				foreach (var ticket in group)
				{
					ticketsTodosLosTicketsCerrados.Add(ticket);
				}
			}

			return ticketsTodosLosTicketsCerrados;
		}
		public List<Ticket> ObtenerTodosLosPreventivosCerrados(List<Ticket> Ticket)
		{
			var ticketAPEGroup = Ticket.Where(ticket =>
				  !string.IsNullOrEmpty(ticket.estado_ticket) && ticket.estado_ticket.Equals(ESTADO_CERRADO, StringComparison.Ordinal) && !string.IsNullOrEmpty(ticket.tipo_mantenimiento) && ticket.tipo_mantenimiento.Equals(TIPO_MANTENIMIENTO_PREVENTIVO, StringComparison.Ordinal)
			 ).GroupBy(ticket => ticket);
			List<Ticket> ticketTodosLosPreventivosCerrados = new List<Ticket>();
			foreach (var group in ticketAPEGroup)
			{
				foreach (var ticket in group)
				{
					ticketTodosLosPreventivosCerrados.Add(ticket);
				}
			}

			return ticketTodosLosPreventivosCerrados;
		}
		public List<Ticket> ObtenerTodosLosCorrectivosCerrados(List<Ticket> tickets)
		{
			var ticketAPEGroup = tickets.Where(tickets =>
				  !string.IsNullOrEmpty(tickets.estado_ticket) && tickets.estado_ticket.Equals(ESTADO_CERRADO, StringComparison.Ordinal) && !string.IsNullOrEmpty(tickets.tipo_mantenimiento) && tickets.tipo_mantenimiento.Equals(TIPO_MANTENIMIENTO_CORRECTIVO, StringComparison.Ordinal)
			 ).GroupBy(ticket => ticket);
			List<Ticket> ticketsCorrectivosCerrados = new List<Ticket>();
			foreach (var group in ticketAPEGroup)
			{
				foreach (var ticket in group)
				{
					ticketsCorrectivosCerrados.Add(ticket);
				}
			}

			return ticketsCorrectivosCerrados;

		}
		public List<Ticket> ObtenerTicketsNoACargoContratista(List<Ticket>? tickets)
		{
			if (tickets == null)
			{

				return null;
			}var ticketGroups = tickets.Where(tickets => tickets != null && tickets.tipo_causa != null && !tickets.tipo_causa.Equals(CONTRATISTA, StringComparison.Ordinal) || tickets.tipo_causa == null)
				.GroupBy(x => x);

			List<Ticket> Ticketc = new List<Ticket>();
			foreach (var group in ticketGroups)
			{
				foreach (var ticket in group)
				{
					Ticketc.Add(ticket);
				}
			}

			return Ticketc;
		}
		public List<Ticket> obtenerTicketsACargoContratista(List<Ticket>? Ticket)
		{
			if (Ticket == null)
			{
				return null;
			}
			var ticketGroups = Ticket.Where(ticket => ticket != null && ticket.tipo_causa != null && ticket.tipo_causa.Equals(CONTRATISTA))
		.GroupBy(x => x);
			List<Ticket> Ticketc = new List<Ticket>();
			foreach (var group in ticketGroups)
			{
				foreach (var ticket in group)
				{
					Ticketc.Add(ticket);
				}
			}


			return Ticketc;
		}
		public IEPMEntity IndicadorDeEfectividadDelPlanDeMantenimiento(List<Ticket> ticket)
		{
			List<Ticket> TicketAME = ObtenerTodosLosMantenimientos(ticket);
			List<Ticket> TicketANP = ObtenerTodosLosCorrectivosCerrados(ticket);
			int AmountOFAME = TicketAME.Count;
			int AmountOFANP = TicketANP.Count;
			IEPMEntity entityGeneral = new IEPMEntity(AmountOFAME, AmountOFANP, TicketAME, TicketANP);

			return entityGeneral;

		}
		public IEPMEntity IndicadorDeEfectividadDelPlanDeMantenimientoContratista(List<Ticket> ticket)
		{
			List<Ticket> ticketCorrectivos = ObtenerTodosLosCorrectivosCerrados(ticket);
			List<Ticket> ticketPreventivos = ObtenerTodosLosPreventivosCerrados(ticket);
			List<Ticket> ticketCorrectivosACargoDe = obtenerTicketsACargoContratista(ticketCorrectivos);
			int AmountOFAMEC = ticketCorrectivosACargoDe.Count + ticketPreventivos.Count;
			int AmountOFANPC = ticketCorrectivosACargoDe.Count;
			IEPMEntity entityContratista = new IEPMEntity (AmountOFAMEC, AmountOFANPC, ticketCorrectivosACargoDe, ticketCorrectivos, ticketPreventivos);
			return entityContratista;

		}
		public IEPMEntity IndicadorDeEfectividadDelPlanDeMantenimientoNoContratista(List<Ticket> ticket)
		{
			List<Ticket> ticketCorrectivos = ObtenerTodosLosCorrectivosCerrados(ticket);
			List<Ticket> ticketPreventivos = ObtenerTodosLosPreventivosCerrados(ticket);
			List<Ticket> ticketCorrectivosNoACargoDe = ObtenerTicketsNoACargoContratista(ticketCorrectivos);
			int AmountOFAME = ticketCorrectivosNoACargoDe.Count + ticketPreventivos.Count;
			Console.WriteLine(AmountOFAME);

			int AmountOFANP = ticketCorrectivosNoACargoDe.Count;
			Console.WriteLine(AmountOFANP);
			IEPMEntity entityNC = new IEPMEntity(AmountOFAME, AmountOFANP, ticketCorrectivosNoACargoDe, ticketPreventivos, ticketCorrectivos);

			return entityNC;
		}

		
	}





}

