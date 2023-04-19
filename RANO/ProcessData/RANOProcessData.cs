using RANO.Model;
using RANO.Model.RANO;

namespace RANO.Services
{

	
	public class RANOProcessData
	{
		public const int HORAS_MAXIMAS_A_TIEMPO = 24;

		public List<Ticket> ANIO_CERRADO_A_TIEMPO(List<Ticket> Ticket)
		{
			var ticketGroups = Ticket.Where(ticket => ticket.fecha_apertura != null && ticket.fecha_cierre != null && !ticket.fecha_apertura.Equals("null")
			&& !ticket.fecha_cierre.Equals("null") &&
			(ticket.fecha_cierre.Value - ticket.fecha_apertura.Value).TotalHours <= HORAS_MAXIMAS_A_TIEMPO
			&& !ticket.estado_ticket.Equals("null") &&
			ticket.estado_ticket.Equals("Cerrado"))
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

		private List<Ticket> ANIO_ABIERTO_SIN_RETRASO(List<Ticket> Ticket)
		{
			var ticketGroups = Ticket.Where(ticket => ticket.fecha_apertura != null && ticket.fecha_cierre != null && !ticket.fecha_apertura.Equals("null") &&
			(ticket.fecha_cierre.Value - ticket.fecha_apertura.Value).TotalHours <= HORAS_MAXIMAS_A_TIEMPO
			&& !ticket.estado_ticket.Equals("null") && ticket.estado_ticket.Equals("Abierta"))
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

		private List<Ticket> ANIO_A_CARGO_CONTRATISTA(List<Ticket> Ticket)
		{


			var ticketGroups = Ticket.Where(ticket => ticket.diagnostico_causa != null && ticket.diagnostico_causa.Equals("A cargo del contratista"))
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
		private List<Ticket> ANIO_NO_A_CARGO_CONTRATISTA(List<Ticket> Ticket)
		{


			var ticketGroups = Ticket.Where(ticket => ticket.diagnostico_causa != null && !ticket.diagnostico_causa.Equals("A cargo del contratista") || ticket.diagnostico_causa == null)
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

		public RANOEntity ObtenerRANOGeneral(List<Ticket> Ticket)
		{
			List<Ticket> totalTicketCerradosATiempo = ANIO_CERRADO_A_TIEMPO(Ticket);
			List<Ticket> totalTicketAbiertosATiempo = ANIO_ABIERTO_SIN_RETRASO(Ticket);
			double TCN = Convert.ToDouble(totalTicketCerradosATiempo.Count);
			double TAN = Convert.ToDouble(Ticket.Count - totalTicketAbiertosATiempo.Count);
			return new RANOEntity(TCN, TAN, totalTicketAbiertosATiempo, totalTicketCerradosATiempo);
		}

		public RANOEntity ObtenerRANOContratista(List<Ticket> Ticket)
		{
			List<Ticket> TicketContratista = ANIO_A_CARGO_CONTRATISTA(Ticket);
			List<Ticket> totalTicketCerradosATiempo = ANIO_CERRADO_A_TIEMPO(TicketContratista);
			List<Ticket> totalTicketAbiertosATiempo = ANIO_ABIERTO_SIN_RETRASO(TicketContratista);
			double TCN = Convert.ToDouble(totalTicketCerradosATiempo.Count);
			double TAN = Convert.ToDouble(TicketContratista.Count - totalTicketAbiertosATiempo.Count);

			return new RANOEntity(TCN, TAN, totalTicketAbiertosATiempo, totalTicketCerradosATiempo);


		}

		public RANOEntity ObtenerRANONoContratista(List<Ticket> Ticket)
		{
			List<Ticket> TicketNoContratista = new List<Ticket>();
			List<Ticket> totalTicketCerradosATiempo = new List<Ticket>();
			List<Ticket> totalTicketAbiertosATiempo = new List<Ticket>();
			if (Ticket.Count > 0)
			{
				TicketNoContratista = ANIO_NO_A_CARGO_CONTRATISTA(Ticket);
				if (TicketNoContratista.Count > 0)
				{
					totalTicketCerradosATiempo = ANIO_CERRADO_A_TIEMPO(TicketNoContratista);
					totalTicketAbiertosATiempo = ANIO_ABIERTO_SIN_RETRASO(TicketNoContratista);
				}
			}
			double TCN = Convert.ToDouble(totalTicketAbiertosATiempo.Count + totalTicketCerradosATiempo.Count);
			double TAN = Convert.ToDouble(TicketNoContratista.Count);

			return new RANOEntity(TCN, TAN, totalTicketAbiertosATiempo, totalTicketCerradosATiempo);


		}
	}
}
