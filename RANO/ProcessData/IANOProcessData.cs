using RANO.Model;
using RANO.ProcessData.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace RANO.ProcessData
{
    public class IANOProcessData : IANOInterface
    {
        private const string NIVEL_FALLA = "ANIO";
        private const double TOTAL_PUERTAS = 146.0;
        private const string CONTRATISTA = "A cargo del contratista";
        private const string COMPONENTE = "Puerta";
        public List<List<Ticket>> ANIO_POR_PUERTA(List<Ticket> Ticket)
        {
            var ticketANIOPuertaGroup = Ticket.Where(ticket =>
                 !string.IsNullOrEmpty(ticket.nivel_falla) && ticket.nivel_falla.Equals(NIVEL_FALLA, StringComparison.Ordinal)
                 && ticket.id_puerta !=null && !string.IsNullOrEmpty(ticket.id_puerta)
                 && ticket.tipo_componente != null && ticket.tipo_componente.Equals(COMPONENTE)
             ).GroupBy(ticket => ticket.id_puerta);
            List<List<Ticket>> gruposPuertas = new List<List<Ticket>>();
			List<Ticket> auxiliar = new List<Ticket>();
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

        public List<List<Ticket>> ANIO_POR_PUERTA_CONTRATISTA(List<Ticket> Ticket)
        {
            var ticketANIOPuertaGroup = Ticket.Where(ticket =>
                 !string.IsNullOrEmpty(ticket.nivel_falla) && ticket.nivel_falla.Equals(NIVEL_FALLA, StringComparison.Ordinal)
                 && ticket.id_puerta != null && !string.IsNullOrEmpty(ticket.id_puerta)
                 && ticket.tipo_causa != null 
                 && ticket.tipo_causa.Equals(CONTRATISTA)
                 && ticket.tipo_componente != null && ticket.tipo_componente.Equals(COMPONENTE)
             ).GroupBy(ticket => ticket.id_puerta);
            List<List<Ticket>> gruposPuertasContratista = new List<List<Ticket>>();
			List<Ticket> auxiliar = new List<Ticket>();
			foreach (var group in ticketANIOPuertaGroup)
            {
                
                foreach (var ticket in group)
                {
                    auxiliar.Add(ticket);
                }
                gruposPuertasContratista.Add(auxiliar);
            }
            return gruposPuertasContratista;
        }

        public List<List<Ticket>> ANIO_POR_PUERTA_NO_CONTRATISTA(List<Ticket> Ticket)
        {
            var ticketANIOPuertaGroup = Ticket.Where(ticket =>
                 !string.IsNullOrEmpty(ticket.nivel_falla) && ticket.nivel_falla.Equals(NIVEL_FALLA, StringComparison.Ordinal)
                 && ticket.id_puerta != null && !string.IsNullOrEmpty(ticket.id_puerta)
                 && (ticket.tipo_causa == null || !ticket.tipo_causa.Equals("A cargo del contratista"))
                 && ticket.tipo_componente != null && ticket.tipo_componente.Equals(COMPONENTE)
             ).GroupBy(ticket => ticket.id_puerta);
            List<List<Ticket>> gruposPuertas = new List<List<Ticket>>();

            foreach (var group in ticketANIOPuertaGroup)
            {
                List<Ticket> auxiliar = new List<Ticket>();
                foreach (var ticket in group)
                {
                    auxiliar.Add(ticket);
                }
                gruposPuertas.Add(auxiliar);
            }
            return gruposPuertas;
        }



        public IANOEntity OBtenerIndicadorIANO(List<Ticket> Ticket) {
            List<List<Ticket>> gruposPuertas = ANIO_POR_PUERTA(Ticket);
            JsonArray puertas = new JsonArray();
            double sumatoria = 0.0;
            double TOTAL_PUERTAS_ANIO = gruposPuertas.Count;
            foreach (var grupo in gruposPuertas)
            {
                JsonObject aux = new JsonObject();
                aux.Add("nombre", grupo[0].id_puerta);
                double calculo = Convert.ToDouble( 1 - (grupo.Count/30.0));
                sumatoria += calculo;
                aux.Add("CantidadTicketANIO", grupo.Count);
                aux.Add("PANO", calculo);
                puertas.Add(aux);
            }
            IANOEntity entitidadIndicadorGeneral = new IANOEntity(TOTAL_PUERTAS, TOTAL_PUERTAS_ANIO, sumatoria, puertas);

            return entitidadIndicadorGeneral;

		}
        public IANOEntity ObtenerIndicadorIANONoContratista(List<Ticket> Ticket)
        {
            List<List<Ticket>> gruposPuertas = ANIO_POR_PUERTA_NO_CONTRATISTA(Ticket);
            JsonArray puertas = new JsonArray();
            double sumatoria = 0.0;
            double TOTAL_PUERTAS_ANIO = gruposPuertas.Count;
            foreach (var grupo in gruposPuertas)
            {
                JsonObject aux = new JsonObject();
                aux.Add("nombre", grupo[0].id_puerta);
                double calculo = Convert.ToDouble(1 - (grupo.Count / 30.0));
                sumatoria += calculo;
                aux.Add("CantidadTicketANIO", grupo.Count);
                aux.Add("PANO", calculo);
                puertas.Add(aux);
            }
            IANOEntity entidadIndicadorGeneralNoContratista = new IANOEntity(TOTAL_PUERTAS, TOTAL_PUERTAS_ANIO, sumatoria, puertas);
            return entidadIndicadorGeneralNoContratista;

		}

        public IANOEntity ObtenerIndicadorIANOContratista(List<Ticket> Ticket)
        {
            List<List<Ticket>> gruposPuertas = ANIO_POR_PUERTA_CONTRATISTA(Ticket);
            JsonArray puertas = new JsonArray();
            double sumatoria = 0.0;
            double TOTAL_PUERTAS_ANIO = gruposPuertas.Count;
            foreach (var grupo in gruposPuertas)
            {
                JsonObject aux = new JsonObject();
                aux.Add("nombre", grupo[0].id_puerta);
                double calculo = Convert.ToDouble(1 - (grupo.Count / 30.0));
                sumatoria += calculo;
                aux.Add("CantidadTicketANIO", grupo.Count);
                aux.Add("PANO", calculo);
                puertas.Add(aux);
            }
			IANOEntity entidaddGeneralContratista = new IANOEntity(TOTAL_PUERTAS, TOTAL_PUERTAS_ANIO, sumatoria, puertas);

            return entidaddGeneralContratista;
		}
    }
}
