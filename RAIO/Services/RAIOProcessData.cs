using static RAIO.Model.Tickets;

namespace RAIO.Services
{

    public class RAIOProcessData
    {
        public const int HORAS_MAXIMAS_A_TIEMPO = 6;

        public List<Ticket> AIO_CERRADO_A_TIEMPO(List<Ticket> tickets)
        {
            var ticketGroups = tickets.Where(ticket => ticket.fecha_apertura !=null && ticket.fecha_cierre!=null && !ticket.fecha_apertura.Equals("null")
            && !ticket.fecha_cierre.Equals("null") &&
            (DateTime.Parse(ticket.fecha_cierre) - DateTime.Parse(ticket.fecha_apertura)).TotalHours <= HORAS_MAXIMAS_A_TIEMPO
            && !ticket.estado_ticket.Equals("null") &&
            ticket.estado_ticket.Equals("Cerrado"))
                .GroupBy(x => x);
            List<Ticket> ticketsc = new List<Ticket>();
            foreach (var group in ticketGroups)
            {
                foreach (var ticket in group)
                {

                    ticketsc.Add(ticket);

                }
            }


            return ticketsc;
        }

        private List<Ticket> AIO_ABIERTO_SIN_RETRASO(List<Ticket> tickets)
        {


            var ticketGroups = tickets.Where(ticket => ticket.fecha_apertura != null &&  !ticket.fecha_apertura.Equals("null") &&
            (DateTime.Now - DateTime.Parse(ticket.fecha_apertura)).TotalHours <= HORAS_MAXIMAS_A_TIEMPO
            && !ticket.estado_ticket.Equals("null") && ticket.estado_ticket.Equals("Abierta"))
                .GroupBy(x => x);
            List<Ticket> ticketsc = new List<Ticket>();
            foreach (var group in ticketGroups)
            {
                foreach (var ticket in group)
                {
                    ticketsc.Add(ticket);
                }
            }


            return ticketsc;


        }

        private List<Ticket> AIO_A_CARGO_CONTRATISTA(List<Ticket> tickets)
        {


            var ticketGroups = tickets.Where(ticket => ticket.diagnostico_causa != null && ticket.diagnostico_causa.Equals("A cargo del contratista"))
                .GroupBy(x => x);
            List<Ticket> ticketsc = new List<Ticket>();
            foreach (var group in ticketGroups)
            {
                foreach (var ticket in group)
                {
                    ticketsc.Add(ticket);
                }
            }


            return ticketsc;


        }
        private List<Ticket> AIO_NO_A_CARGO_CONTRATISTA(List<Ticket> tickets)
        {


            var ticketGroups = tickets.Where(ticket => ticket.diagnostico_causa != null && !ticket.diagnostico_causa.Equals("A cargo del contratista"))
                .GroupBy(x => x);
            List<Ticket> ticketsc = new List<Ticket>();
            foreach (var group in ticketGroups)
            {
                foreach (var ticket in group)
                {
                    ticketsc.Add(ticket);
                }
            }


            return ticketsc;


        }

        public dynamic RAIO_GENERAL(List<Ticket> tickets)
        {
            List<Ticket> totalTicketsCerradosATiempo = AIO_CERRADO_A_TIEMPO(tickets);
            List<Ticket> totalTicketsAbiertosATiempo = AIO_ABIERTO_SIN_RETRASO(tickets);
            double result = 0;
            double TCI = Convert.ToDouble(totalTicketsAbiertosATiempo.Count + totalTicketsCerradosATiempo.Count);
            double TAI = Convert.ToDouble(tickets.Count);
            if (TAI != 0) { 
                result = (TCI / TAI)*100;
            }
            else
            {
                result = 100;
            }
            


            return new
            {
                RAIO_GENERAL = result,
                cantidad_AIO = tickets.Count,
                CANTIDAD_AIOS_CERRADOS_A_TIEMPO = totalTicketsCerradosATiempo.Count,
                CANTIDAD_AIOS_ABIERTOS_A_TIEMPO = totalTicketsAbiertosATiempo.Count,
                AIOS_CERRADOS_A_TIEMPO = totalTicketsCerradosATiempo,
                AIOS_ABIERTOS_A_TIEMPO = totalTicketsAbiertosATiempo,
                AIOS_TOTALES = tickets

            };


        }

        public dynamic RAIO_CONTRATISTA(List<Ticket> tickets)
        {
            List<Ticket> ticketsContratista = AIO_A_CARGO_CONTRATISTA(tickets);
            List<Ticket> totalTicketsCerradosATiempo = AIO_CERRADO_A_TIEMPO(ticketsContratista);
            List<Ticket> totalTicketsAbiertosATiempo = AIO_ABIERTO_SIN_RETRASO(ticketsContratista);
            double result = 0;
            double TCI = Convert.ToDouble(totalTicketsAbiertosATiempo.Count + totalTicketsCerradosATiempo.Count);
            double TAI = Convert.ToDouble(ticketsContratista.Count);
            if (TAI != 0)
            {
                result = (TCI / TAI) * 100;
            }
            else
            {
                result = 100;
            }

            return new
            {
                RAIO_CONTRATISTA = result,
                cantidad_AIO = ticketsContratista.Count,
                CANTIDAD_AIOS_CERRADOS_A_TIEMPO = totalTicketsCerradosATiempo.Count,
                CANTIDAD_AIOS_ABIERTOS_A_TIEMPO = totalTicketsAbiertosATiempo.Count,
                AIOS_CERRADOS_A_TIEMPO = totalTicketsCerradosATiempo,
                AIOS_ABIERTOS_A_TIEMPO = totalTicketsAbiertosATiempo,
                AIOS_TOTALES = ticketsContratista

            };


        }

        public dynamic RAIO_NO_CONTRATISTA(List<Ticket> tickets)
        {
            List<Ticket> ticketsNoContratista = new List<Ticket>();
            List<Ticket> totalTicketsCerradosATiempo = new List<Ticket>();
            List<Ticket> totalTicketsAbiertosATiempo = new List<Ticket>();
            if (tickets.Count > 0)
            {
                ticketsNoContratista = AIO_NO_A_CARGO_CONTRATISTA(tickets);
                if (ticketsNoContratista.Count > 0)
                {
                    totalTicketsCerradosATiempo = AIO_CERRADO_A_TIEMPO(ticketsNoContratista);
                    totalTicketsAbiertosATiempo = AIO_ABIERTO_SIN_RETRASO(ticketsNoContratista);
                }
            }
            double result = 0;
            double TCI = Convert.ToDouble(totalTicketsAbiertosATiempo.Count + totalTicketsCerradosATiempo.Count);
            double TAI = Convert.ToDouble(ticketsNoContratista.Count);
            if (TAI != 0)
            {
                result = (TCI / TAI) * 100;
            }
            else
            {
                result = 100;
            }



            return new
            {
                RAIO_NO_CONTRATISTA = result,
                cantidad_AIO = ticketsNoContratista.Count,
                CANTIDAD_AIOS_CERRADOS_A_TIEMPO = totalTicketsCerradosATiempo.Count,
                CANTIDAD_AIOS_ABIERTOS_A_TIEMPO = totalTicketsAbiertosATiempo.Count,
                AIOS_CERRADOS_A_TIEMPO = totalTicketsCerradosATiempo,
                AIOS_ABIERTOS_A_TIEMPO = totalTicketsAbiertosATiempo,
                AIOS_TOTALES = ticketsNoContratista

            };


        }
    }
}
