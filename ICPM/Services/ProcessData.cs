using static ICPM.Model.Tickets;

namespace ICPM.Services
{

    public class ProcessData
    {
        public const int HORAS_MAXIMAS_A_TIEMPO = 6;

        public List<Ticket> PREVENTIVO_CERRADO(List<Ticket> tickets)
        {
            var ticketGroups = tickets.Where(ticket => 
            ticket.estado_ticket!=null &&
            !ticket.estado_ticket.Equals("null") &&
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



        private List<Ticket> PREVENTIVO_PUERTAS(List<Ticket> tickets)
        {
            var ticketGroups = tickets.Where(ticket => ticket.componente != null
            && !ticket.componente.Equals("null")
            && ticket.componente.Equals("Puerta"))
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
        private List<Ticket> PREVENTIVO_COMPONENTE_ITS(List<Ticket> tickets)
        {


            var ticketGroups = tickets.Where(ticket => ticket.componente != null
            && !ticket.componente.Equals("null")
            && ticket.componente.Equals("Componente ITS"))
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
        private List<Ticket> PREVENTIVO_RFID(List<Ticket> tickets)
        {


            var ticketGroups = tickets.Where(ticket => ticket.componente != null
            && !ticket.componente.Equals("null")
            && ticket.componente.Equals("Equipo RFID"))
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

        public dynamic ICPM_GENERAL(List<Ticket> tickets)
        {
            List<Ticket> totalTicketsCerrados = PREVENTIVO_CERRADO(tickets);
            double result = 0;
            double TCI = Convert.ToDouble(totalTicketsCerrados.Count);
            double TAI = Convert.ToDouble(tickets.Count);
            try
            {
                result = TCI / TAI;
            }
            catch (Exception e) { }


            return new
            {
                ICPM_GENERAL = result,
                CANTIDAD_PREVENTIVOS = tickets.Count,
                CANTIDAD_PREVENTIVOS_CERRADOS = totalTicketsCerrados.Count,
                PREVENTIVO_CERRADO = totalTicketsCerrados,
                PREVENTIVO = tickets

            };


        }

        public dynamic ICPM_PUERTAS(List<Ticket> tickets)
        {
            List<Ticket> totalTicketsPuertas = PREVENTIVO_PUERTAS(tickets);
            List<Ticket> totalTicketsCerrados = PREVENTIVO_CERRADO(totalTicketsPuertas);
            double result = 0;
            double TCI = Convert.ToDouble(totalTicketsCerrados.Count);
            double TAI = Convert.ToDouble(totalTicketsPuertas.Count);
            try
            {
                result = TCI / TAI;
            }
            catch (Exception e) { }


            return new
            {
                ICPM_PUERTAS = result,
                CANTIDAD_PREVENTIVOS = totalTicketsPuertas.Count,
                CANTIDAD_PREVENTIVOS_CERRADOS = totalTicketsCerrados.Count,
                PREVENTIVO_CERRADO = totalTicketsCerrados,
                PREVENTIVO = totalTicketsPuertas

            };


        }

        public dynamic ICPM_COMPONENTE_ITS(List<Ticket> tickets)
        {
            List<Ticket> totalTicketsComponenteITS = PREVENTIVO_COMPONENTE_ITS(tickets);
            List<Ticket> totalTicketsCerrados = PREVENTIVO_CERRADO(totalTicketsComponenteITS);
            double result = 0;
            double TCI = Convert.ToDouble(totalTicketsCerrados.Count);
            double TAI = Convert.ToDouble(totalTicketsComponenteITS.Count);
            try
            {
                result = TCI / TAI;
            }
            catch (Exception e) { }


            return new
            {
                ICPM_COMPONENTE_ITS = result,
                CANTIDAD_PREVENTIVOS = totalTicketsComponenteITS.Count,
                CANTIDAD_PREVENTIVOS_CERRADOS = totalTicketsCerrados.Count,
                PREVENTIVO_CERRADO = totalTicketsCerrados,
                PREVENTIVO = totalTicketsComponenteITS

            };


        }
    }
}
