using RANO.Model;
using RANO.Model.RANO;
using static RANO.Model.Ticket;

namespace RANO.ProcessData
{

    public class RAIOProcessData
    {
        public const int HORAS_MAXIMAS_A_TIEMPO = 6;

        private List<Ticket> AIO_CERRADO_A_TIEMPO(List<Ticket> Ticket)
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

        private List<Ticket> AIO_ABIERTO_SIN_RETRASO(List<Ticket> Ticket)
        {var ticketGroups = Ticket.Where(ticket => ticket.fecha_apertura.HasValue && ticket.fecha_cierre.HasValue &&
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

        private List<Ticket> AIO_A_CARGO_CONTRATISTA(List<Ticket> Ticket)
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
        private List<Ticket> AIO_NO_A_CARGO_CONTRATISTA(List<Ticket> Ticket)
        {


            var ticketGroups = Ticket.Where(ticket => ticket.diagnostico_causa != null && !ticket.diagnostico_causa.Equals("A cargo del contratista"))
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

        public RAIOEntity ObtenerRAIOGeneral(List<Ticket> Ticket)
        {
            List<Ticket> totalTicketCerradosATiempo = AIO_CERRADO_A_TIEMPO(Ticket);
            List<Ticket> totalTicketAbiertosATiempo = AIO_ABIERTO_SIN_RETRASO(Ticket);
            double result = 0;
            double TCI = Convert.ToDouble(totalTicketAbiertosATiempo.Count + totalTicketCerradosATiempo.Count);
            double TAI = Convert.ToDouble(Ticket.Count);
            return new RAIOEntity (TCI, TAI, totalTicketCerradosATiempo, totalTicketAbiertosATiempo, Ticket);


        }

        public RAIOEntity ObtenerRAIOContratista(List<Ticket> Ticket)
        {
            List<Ticket> TicketContratista = AIO_A_CARGO_CONTRATISTA(Ticket);
            List<Ticket> totalTicketCerradosATiempo = AIO_CERRADO_A_TIEMPO(TicketContratista);
            List<Ticket> totalTicketAbiertosATiempo = AIO_ABIERTO_SIN_RETRASO(TicketContratista);
            double TCI = Convert.ToDouble(totalTicketAbiertosATiempo.Count + totalTicketCerradosATiempo.Count);
            double TAI = Convert.ToDouble(TicketContratista.Count);

			return new RAIOEntity(TCI, TAI, totalTicketAbiertosATiempo, totalTicketCerradosATiempo, TicketContratista);


		}

        public RAIOEntity ObtenerRAIONoContratista(List<Ticket> Ticket)
        {
            List<Ticket> TicketNoContratista = new List<Ticket>();
            List<Ticket> totalTicketCerradosATiempo = new List<Ticket>();
            List<Ticket> totalTicketAbiertosATiempo = new List<Ticket>();
            if (Ticket.Count > 0)
            {
                TicketNoContratista = AIO_NO_A_CARGO_CONTRATISTA(Ticket);
                if (TicketNoContratista.Count > 0)
                {
                    totalTicketCerradosATiempo = AIO_CERRADO_A_TIEMPO(TicketNoContratista);
                    totalTicketAbiertosATiempo = AIO_ABIERTO_SIN_RETRASO(TicketNoContratista);
                }
            }
            double TCI = Convert.ToDouble(totalTicketAbiertosATiempo.Count + totalTicketCerradosATiempo.Count);
            double TAI = Convert.ToDouble(TicketNoContratista.Count);
            return new RAIOEntity(TCI,TAI, totalTicketAbiertosATiempo, totalTicketCerradosATiempo, TicketNoContratista);

            


        }
    }
}
