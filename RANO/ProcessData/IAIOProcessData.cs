using RANO.Model;
using RANO.ProcessData.Interfaces;
using System.Text.Json.Nodes;

namespace RANO.ProcessData
{
    public class IAIOProcessData : IAIOInterface
    {
        private readonly string NIVEL_FALLA = "AIO";
        private readonly double TOTAL_PUERTAS = 146.0;
        private readonly string CONTRATISTA = "A cargo del contratista";
        private readonly string COMPONENTE = "Puerta";
        public List<List<Ticket>> AIO_POR_PUERTA(List<Ticket> Ticket)
        {
			List<List<Ticket>> gruposPuertasAIO = new List<List<Ticket>>();
			List<Ticket> auxiliar = new List<Ticket>();
			var ticketANIOPuertaGroup = Ticket.Where(ticket =>
                 !string.IsNullOrEmpty(ticket.nivel_falla) && ticket.nivel_falla.Equals(NIVEL_FALLA, StringComparison.Ordinal)
                 && ticket.id_puerta != null && !string.IsNullOrEmpty(ticket.id_puerta)
                 && ticket.tipo_componente != null && ticket.tipo_componente.Equals(COMPONENTE)
             ).GroupBy(ticket => ticket.id_puerta);
            foreach (var group in ticketANIOPuertaGroup)
            {
                
                foreach (var ticket in group)
                {
                    auxiliar.Add(ticket);
                }
                gruposPuertasAIO.Add(auxiliar);
            }
            return gruposPuertasAIO;
        }

        public List<List<Ticket>> AIO_POR_PUERTA_CONTRATISTA(List<Ticket> Ticket)
        {
            var ticketANIOPuertaGroup = Ticket.Where(ticket =>
                 !string.IsNullOrEmpty(ticket.nivel_falla) && ticket.nivel_falla.Equals(NIVEL_FALLA, StringComparison.Ordinal)
                 && ticket.id_puerta != null && !string.IsNullOrEmpty(ticket.id_puerta)
                 && ticket.tipo_causa != null
                 && ticket.tipo_causa.Equals(CONTRATISTA)
                 && ticket.tipo_componente != null && ticket.tipo_componente.Equals(COMPONENTE)
             ).GroupBy(ticket => ticket.id_puerta);
            List<List<Ticket>> gruposPuertasAIOPorPuerta = new List<List<Ticket>>();

            foreach (var group in ticketANIOPuertaGroup)
            {
                List<Ticket> auxiliar = new List<Ticket>();
                foreach (var ticket in group)
                {
                    auxiliar.Add(ticket);
                }
				gruposPuertasAIOPorPuerta.Add(auxiliar);
            }
            return gruposPuertasAIOPorPuerta;
        }

        public List<List<Ticket>> AIO_POR_PUERTA_NO_CONTRATISTA(List<Ticket> Ticket)
        {
			List<List<Ticket>> gruposPuertas = new List<List<Ticket>>();
			List<Ticket> auxiliar = new List<Ticket>();
			var ticketANIOPuertaGroup = Ticket.Where(ticket =>
                 !string.IsNullOrEmpty(ticket.nivel_falla) && ticket.nivel_falla.Equals(NIVEL_FALLA, StringComparison.Ordinal)
                 && ticket.id_puerta != null && !string.IsNullOrEmpty(ticket.id_puerta)
                 && (ticket.tipo_causa == null || !ticket.tipo_causa.Equals(CONTRATISTA))
                 && ticket.tipo_componente != null && ticket.tipo_componente.Equals(COMPONENTE)
             ).GroupBy(ticket => ticket.id_puerta);


            foreach (var group in ticketANIOPuertaGroup)
            {
                
                foreach (var ticket in group)
                {
                    auxiliar.Add(ticket);
                }
                gruposPuertas.Add(auxiliar);
            }
            return gruposPuertas;
        }

        public IAIOEntity ObtenerIAIOGeneral(List<Ticket> Ticket)
        {
            List<List<Ticket>> gruposPuertas = AIO_POR_PUERTA(Ticket);
            JsonArray puertas = new JsonArray();
            double sumatoria = 0.0;
            double TOTAL_PUERTAS_AIO = gruposPuertas.Count;
            foreach (var grupo in gruposPuertas)
            {
                JsonObject aux = new JsonObject();
                aux.Add("nombre", grupo[0].id_puerta);
                double calculo = 0;
                if (grupo.Count == 0)
                {
                    calculo = 100;
                }
                else if (grupo.Count == 1)
                {
                    calculo = 90;
                }
                else if (grupo.Count == 2)
                {
                    calculo = 40;
                }
                sumatoria += calculo;
                aux.Add("CantidadTicketAIO", grupo.Count);
                aux.Add("PAIO", calculo);
                puertas.Add(aux);
            }
            IAIOEntity entityGeneralIAIO = new IAIOEntity(TOTAL_PUERTAS, TOTAL_PUERTAS_AIO, sumatoria, puertas);
            

            return entityGeneralIAIO;

		}
        public IAIOEntity ObtenerIAIONoContratista(List<Ticket> Ticket)
        {
            List<List<Ticket>> gruposPuertas = AIO_POR_PUERTA_NO_CONTRATISTA(Ticket);
            JsonArray puertas = new JsonArray();
            double sumatoria = 0.0;
            double TOTAL_PUERTAS_AIO = gruposPuertas.Count;
            foreach (var grupo in gruposPuertas)
            {
                JsonObject aux = new JsonObject();
                aux.Add("nombre", grupo[0].id_puerta);
                double calculo = 0;
                if (grupo.Count == 0)
                {
                    calculo = 100;
                }
                else if (grupo.Count == 1)
                {
                    calculo = 90;
                }
                else if (grupo.Count == 2)
                {
                    calculo = 40;
                }
                sumatoria += calculo;
                aux.Add("CantidadTicketAIO", grupo.Count);
                aux.Add("PAIO", calculo);
                puertas.Add(aux);
            }
            IAIOEntity entityNoContratista = new IAIOEntity(TOTAL_PUERTAS, TOTAL_PUERTAS_AIO, sumatoria, puertas);

            return entityNoContratista;
        }

        public IAIOEntity ObtenerIAIOContratista(List<Ticket> Ticket)
        {
            List<List<Ticket>> gruposPuertas = AIO_POR_PUERTA_CONTRATISTA(Ticket);
            JsonArray puertas = new JsonArray();
            double sumatoria = 0.0;
            double TOTAL_PUERTAS_AIO = gruposPuertas.Count;
            foreach (var grupo in gruposPuertas)
            {
                JsonObject aux = new JsonObject();
                aux.Add("nombre", grupo[0].id_puerta);
                double calculo = 0;
                if (grupo.Count == 0)
                {
                    calculo = 100;
                }
                else if (grupo.Count == 1)
                {
                    calculo = 90;
                }
                else if (grupo.Count == 2)
                {
                    calculo = 40;
                }
                sumatoria += calculo;
                aux.Add("CantidadTicketAIO", grupo.Count);
                aux.Add("PAIO", calculo);
                puertas.Add(aux);
            }
			IAIOEntity entityContratista = new IAIOEntity(TOTAL_PUERTAS, TOTAL_PUERTAS_AIO, sumatoria, puertas);

            return entityContratista;

		}
	}
}
