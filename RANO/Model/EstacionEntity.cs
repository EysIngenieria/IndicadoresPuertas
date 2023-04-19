using System.Net.Sockets;

namespace RANO.Model
{
    public class EstacionEntity
    {
        private const string EVP1 = "EVP1";
        private const string EVP12 = "EVP12";
        private const string EVP10 = "EVP10";
        private const string EVP11 = "EVP11";
        private const string EVP14 = "EVP14";
        private const string AUTODIAGNOSTICO = "AUTODIAGNOSTICO";
        private const string RESET = "RESET";
        private const string BOTON_MANUAL = "BOTON MANUAL";
        public int estacion { get; set; }
        public int cantidadPuertas { get; set; }
        public List<Ticket> tickets { get; set; }
        public List<Evento> eventos { get; set; }
        public List<Comando> comandos { get; set; }
        public EstacionEntity(int estacion, int cantidadPuertas, List<Ticket> tickets, List<Evento> eventos)
        {
            this.estacion = estacion;
            this.eventos = eventos;
            this.tickets = tickets;
            this.cantidadPuertas = cantidadPuertas;
        }
		public double tiempoTotalPorPuertaFueraDeServicio()
        {
            if (eventos != null && eventos.Count > 0)
            {
                if (eventos[0].idVagon.Equals("FIN-OP"))
                {
                    eventos.Remove(eventos[0]);
                }
                if (eventos[eventos.Count - 1].idVagon.Equals("INICIO-OP"))
                {
                    eventos.Remove(eventos[eventos.Count - 1]);
                }
            }
            double tiempoFueraDeServicio = 0.0;

            List<Ticket> ticketsTemporal = new List<Ticket>(tickets);

            for (int i = 0; i < eventos.Count - 1; i += 2)  
            {
                Evento eventoInicio = eventos[i];
                Evento eventoFin = eventos[i + 1];
                DateTime inicio = DateTime.Parse(eventoInicio.fechaHoraLecturaDato);
                DateTime fin = DateTime.Parse(eventoFin.fechaHoraLecturaDato);
                List<Ticket> ticketsTemporalDetenido = new List<Ticket>(ticketsTemporal);
                foreach (var ticket in ticketsTemporalDetenido)
                {
					DateTime fechaApertura = ticket.fecha_apertura ?? DateTime.MinValue;
					DateTime fechaCierre = ticket.fecha_cierre ?? DateTime.MinValue;
					if ((fechaApertura - inicio).TotalHours >= 0 && (fin - fechaCierre).TotalHours >= 0)
                    {
                        tiempoFueraDeServicio += (fechaCierre - fechaApertura).TotalHours;
                        ticketsTemporal.Remove(ticket);
                    }
                    else if ((fechaCierre - inicio).TotalHours >= 0 && (fin - fechaCierre).TotalHours >= 0)
                    {
                        tiempoFueraDeServicio += (fechaCierre - inicio).TotalHours;
                        ticketsTemporal.Remove(ticket);
                    }
                    else if ((fechaApertura - inicio).TotalHours >= 0 && (fechaCierre - fin).TotalHours >= 0 && (fin - fechaApertura).TotalHours >= 0)
                    {
                        tiempoFueraDeServicio += (fin - fechaApertura).TotalHours;
                    }
                    else if ((inicio - fechaCierre).TotalHours >= 0)
                    {
                        ticketsTemporal.Remove(ticket);
                    }
                }
            }
            return tiempoFueraDeServicio;
        }

        public double totalTiempoPuertas()
        {
            double total = 0.0;

            for (int i = 1; i < eventos.Count - 1; i += 2)
            {
                Evento eventoInicio = eventos[i];
                Evento eventoFin = eventos[i + 1];
                DateTime inicio = DateTime.Parse(eventoInicio.fechaHoraLecturaDato);
                DateTime fin = DateTime.Parse(eventoFin.fechaHoraLecturaDato);
                total = (fin - inicio).TotalHours * cantidadPuertas;

            }
            return total;

        }

        public double CEI()
        {
            double result = 0.0;
            if (eventos != null && eventos.Count > 0)
            {
                if (eventos[0].idVagon.Equals("FIN-OP"))
                {
                    eventos.Remove(eventos[0]);
                }
                if (eventos[eventos.Count - 1].idVagon.Equals("INICIO-OP"))
                {
                    eventos.Remove(eventos[eventos.Count - 1]);
                }
            }


            for (int i = 0; i < eventos.Count - 1; i += 2)
            {

                if (eventos[i].idVagon.Equals("INICIO-OP"))
                {
                    result++;
                }
            }
            
            return result;
        }

        public double CEF()
        {
            double result = 0.0;
            if (eventos != null && eventos.Count > 0)
            {
                if (eventos[0].idVagon.Equals("FIN-OP"))
                {
                    eventos.Remove(eventos[0]);
                }
                if (eventos[eventos.Count - 1].idVagon.Equals("INICIO-OP"))
                {
                    eventos.Remove(eventos[eventos.Count - 1]);
                }
            }

            for (int i = 0; i < eventos.Count - 1; i += 2)
            {
                if (eventos[i].idVagon.Equals("FIN-OP"))
                {
                    result++;
                }

            }
            
            return result;
        }

        public int TMR()
        {
            int result = 0;
            foreach (var evento in eventos)
            {
                if (evento.codigoEvento.Equals(EVP10) || evento.codigoEvento.Equals(EVP11) || evento.codigoEvento.Equals(EVP14))
                {
                    result++;
                }

            }

            return result;

        }
        public int TME()
        {
            int result = 0; 
            foreach (var comando in comandos)
            {
                if (comando.mensaje.Equals(AUTODIAGNOSTICO) || comando.mensaje.Equals(RESET) || comando.mensaje.Equals(BOTON_MANUAL))
                {
                    result++;
                }
            }
            return result;
        }

        public int EAP()
        {
            int result = 0;
            foreach (var evento in eventos)
            {
                if (evento.codigoEvento.Equals(EVP1) && evento.estadoAperturaCierrePuertas == true)
                {
                    result++;
                }
            }
            return result;
        }

        public int ECP()
        {
            int result = 0;
            foreach (var evento in eventos)
            {
                if (evento.codigoEvento.Equals(EVP1) && evento.estadoAperturaCierrePuertas == false)
                {
                    result++;
                }
            }
            return result;
        }
        public int EABE() {
            int result = 0;
            foreach (var evento in eventos)
            {
                if (evento.codigoEvento.Equals(EVP12) && evento.numeroEventoBusEstacion == 1)
                {
                    result++;
                }
            }
            return result;
        }

        public int ECBE()
        {
            int result = 0;
            foreach (var evento in eventos)
            {
                if (evento.codigoEvento.Equals(EVP12) && evento.numeroEventoBusEstacion == 2||evento.numeroEventoBusEstacion== 3)
                {
                    result++;
                }
            }
            return result;
        }
    }
}
