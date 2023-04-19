using RANO.ProcessData.Interfaces;

namespace RANO.Model{

	public class ICPMProcessData : ICPMInterface
	{
		private string ESTADO_CERRADO = "Cerrado";
		private string TIPO_MANTENIMIENTO_PREVENTIVO = "Mantenimiento Preventivo";


		public List<Ticket> ObtenerTodosLosPreventivos(List<Ticket> Ticket)
		{
			var ticketAPEGroup = Ticket.Where(ticket =>
				 !string.IsNullOrEmpty(ticket.tipo_mantenimiento) && ticket.tipo_mantenimiento.Equals(TIPO_MANTENIMIENTO_PREVENTIVO, StringComparison.Ordinal)
			 ).GroupBy(ticket => ticket);
			List<Ticket> Ticketc = new List<Ticket>();
			foreach (var group in ticketAPEGroup)
			{
				foreach (var ticket in group)
				{
					Ticketc.Add(ticket);
				}
			}

			return Ticketc;

		}
		public List<Ticket> ObtenerTodosLosPreventivosComponente(List<Ticket> Ticket,string componente)
		{
			var ticketAPEGroup = Ticket.Where(ticket =>
				 !string.IsNullOrEmpty(ticket.tipo_mantenimiento) && ticket.tipo_mantenimiento.Equals(TIPO_MANTENIMIENTO_PREVENTIVO, StringComparison.Ordinal) && !string.IsNullOrEmpty(ticket.tipo_componente) && ticket.tipo_componente.Equals(componente, StringComparison.Ordinal)
			 ).GroupBy(ticket => ticket);
			List<Ticket> Ticketc = new List<Ticket>();
			foreach (var group in ticketAPEGroup)
			{
				foreach (var ticket in group)
				{
					Ticketc.Add(ticket);
				}
			}

			return Ticketc;

		}
		public List<Ticket> ObtenerTodosLosPreventivosCerrados(List<Ticket> Ticket)
		{
			var ticketAPEGroup = Ticket.Where(ticket =>
				 !string.IsNullOrEmpty(ticket.tipo_mantenimiento) && ticket.tipo_mantenimiento.Equals(TIPO_MANTENIMIENTO_PREVENTIVO, StringComparison.Ordinal)&&
				  !string.IsNullOrEmpty(ticket.estado_ticket) && ticket.estado_ticket.Equals(ESTADO_CERRADO, StringComparison.Ordinal)

			 ).GroupBy(ticket => ticket);
			List<Ticket> Ticketc = new List<Ticket>();
			foreach (var group in ticketAPEGroup)
			{
				foreach (var ticket in group)
				{
					Ticketc.Add(ticket);
				}
			}

			return Ticketc;

		}
		public List<Ticket> ObtenerTodosLosPreventivosCerradosComponente(List<Ticket> Ticket, string componente)
		{
			var ticketAPEGroup = Ticket.Where(ticket =>
				 !string.IsNullOrEmpty(ticket.estado_ticket) && ticket.estado_ticket.Equals(ESTADO_CERRADO, StringComparison.Ordinal)  
				 && !string.IsNullOrEmpty(ticket.tipo_mantenimiento) && ticket.tipo_mantenimiento.Equals(TIPO_MANTENIMIENTO_PREVENTIVO, StringComparison.Ordinal) 
				 && !string.IsNullOrEmpty(ticket.tipo_componente) && ticket.tipo_componente.Equals(componente, StringComparison.Ordinal)
			 ).GroupBy(ticket => ticket);
			List<Ticket> Ticketc = new List<Ticket>();
			foreach (var group in ticketAPEGroup)
			{
				foreach (var ticket in group)
				{
					Ticketc.Add(ticket);
				}
			}

			return Ticketc;

		}

		public ICPMEntity IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOAsync(List<Ticket> ticket)
		{
			List<Ticket> TicketAPE = ObtenerTodosLosPreventivosCerrados(ticket);
			List<Ticket> TicketTAP = ObtenerTodosLosPreventivos(ticket);
			int AmountOFTAP = TicketTAP.Count;
			int AmountOFAPE = TicketAPE.Count;
			return new ICPMEntity(AmountOFTAP, AmountOFAPE, TicketAPE, TicketTAP);


		}
		public ICPMEntity IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(List<Ticket> Ticket, string componente)
		{
			List<Ticket> AllTickers = Ticket;
			List<Ticket> TicketAPE = ObtenerTodosLosPreventivosCerradosComponente(AllTickers, componente);
			List<Ticket> TicketTAP = ObtenerTodosLosPreventivosComponente(AllTickers, componente);
			int AmountOFTAP = TicketTAP.Count;
			int AmountOFAPE = TicketAPE.Count;
			return new ICPMEntity(AmountOFTAP,AmountOFAPE,TicketAPE,TicketTAP);
		}

		
	}
}
