using static RANO.Model.Tickets;

namespace RANO.Services
{

    public class RANOProcessData
    {
        public const int HORAS_MAXIMAS_A_TIEMPO = 24;
        
        public List<Ticket> ANIO_CERRADO_A_TIEMPO(List<Ticket> tickets)
        {
            var ticketGroups = tickets.Where(ticket => ticket.fecha_apertura != null && ticket.fecha_cierre!=null &&!ticket.fecha_apertura.Equals("null") 
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

        private List<Ticket> ANIO_ABIERTO_SIN_RETRASO(List<Ticket> tickets)
        {


            var ticketGroups = tickets.Where(ticket => ticket.fecha_apertura != null && !ticket.fecha_apertura.Equals("null") &&
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

        private List<Ticket> ANIO_A_CARGO_CONTRATISTA(List<Ticket> tickets)
        {


            var ticketGroups = tickets.Where(  ticket =>  ticket.diagnostico_causa!=null && ticket.diagnostico_causa.Equals("A cargo del contratista"))
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
        private List<Ticket> ANIO_NO_A_CARGO_CONTRATISTA(List<Ticket> tickets)
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

        public dynamic RANO_GENERAL(List<Ticket> tickets) 
        {
            List<Ticket> totalTicketsCerradosATiempo = ANIO_CERRADO_A_TIEMPO(tickets);
            List<Ticket> totalTicketsAbiertosATiempo = ANIO_ABIERTO_SIN_RETRASO(tickets);
            double result = 0;
            double TCN = Convert.ToDouble( totalTicketsCerradosATiempo.Count);
            double TAN = Convert.ToDouble(tickets.Count - totalTicketsAbiertosATiempo.Count);
            if (TAN != 0)
            {
                result = (TCN / TAN) * 100;
            }
            else
            {
                result = 100;
            }
           


            return new
            {
                RANO_GENERAL = result,
                cantidad_ANIO = tickets.Count,
                CANTIDAD_ANIOS_CERRADOS_A_TIEMPO = totalTicketsCerradosATiempo.Count,
                CANTIDAD_ANIOS_ABIERTOS_A_TIEMPO = totalTicketsAbiertosATiempo.Count,
                ANIOS_CERRADOS_A_TIEMPO = totalTicketsCerradosATiempo,
                ANIOS_ABIERTOS_A_TIEMPO = totalTicketsAbiertosATiempo,
                ANIOS_TOTALES = tickets

            }; 


        }

        public dynamic RANO_CONTRATISTA(List<Ticket> tickets)
        {
            List<Ticket> ticketsContratista = ANIO_A_CARGO_CONTRATISTA(tickets);
            List<Ticket> totalTicketsCerradosATiempo = ANIO_CERRADO_A_TIEMPO(ticketsContratista);
            List<Ticket> totalTicketsAbiertosATiempo = ANIO_ABIERTO_SIN_RETRASO(ticketsContratista);
            double result = 0;
            double TCN = Convert.ToDouble( totalTicketsCerradosATiempo.Count);
            double TAN = Convert.ToDouble(ticketsContratista.Count - totalTicketsAbiertosATiempo.Count);
            if (TAN != 0)
            {
                result = (TCN / TAN) * 100;
            }
            else
            {
                result = 100;
            }

            return new
            {
                RANO_CONTRATISTA = result,
                cantidad_ANIO = ticketsContratista.Count,
                CANTIDAD_ANIOS_CERRADOS_A_TIEMPO = totalTicketsCerradosATiempo.Count,
                CANTIDAD_ANIOS_ABIERTOS_A_TIEMPO = totalTicketsAbiertosATiempo.Count,
                ANIOS_CERRADOS_A_TIEMPO = totalTicketsCerradosATiempo,
                ANIOS_ABIERTOS_A_TIEMPO = totalTicketsAbiertosATiempo,
                ANIOS_TOTALES = ticketsContratista

            };


        }

        public dynamic RANO_NO_CONTRATISTA(List<Ticket> tickets)
        {
            List<Ticket> ticketsNoContratista = new List<Ticket>();
            List<Ticket> totalTicketsCerradosATiempo = new List<Ticket>();
            List<Ticket> totalTicketsAbiertosATiempo = new List<Ticket>();
            if (tickets.Count > 0)
            {
                ticketsNoContratista = ANIO_NO_A_CARGO_CONTRATISTA(tickets);
                if (ticketsNoContratista.Count > 0)
                {
                    totalTicketsCerradosATiempo = ANIO_CERRADO_A_TIEMPO(ticketsNoContratista);
                    totalTicketsAbiertosATiempo = ANIO_ABIERTO_SIN_RETRASO(ticketsNoContratista);
                }
            }
            double result = 0;
            double TCN = Convert.ToDouble(totalTicketsAbiertosATiempo.Count + totalTicketsCerradosATiempo.Count);
            double TAN = Convert.ToDouble(ticketsNoContratista.Count);
            if (TAN != 0)
            {
                result = (TCN / TAN) * 100;
            }
            else
            {
                result = 100;
            }



            return new
            {
                RANO_NO_CONTRATISTA = result,
                cantidad_ANIO = ticketsNoContratista.Count,
                CANTIDAD_ANIOS_CERRADOS_A_TIEMPO = totalTicketsCerradosATiempo.Count,
                CANTIDAD_ANIOS_ABIERTOS_A_TIEMPO = totalTicketsAbiertosATiempo.Count,
                ANIOS_CERRADOS_A_TIEMPO = totalTicketsCerradosATiempo,
                ANIOS_ABIERTOS_A_TIEMPO = totalTicketsAbiertosATiempo,
                ANIOS_TOTALES = ticketsNoContratista

            };


        }
    }
}
