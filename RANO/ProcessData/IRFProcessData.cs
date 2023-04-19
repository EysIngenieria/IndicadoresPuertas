using RANO.Model;
using RANO.ProcessData.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Nodes;

namespace RANO.ProcessData
{
    public class IRFOProcessData : IRFInterface
    {
        private const string ESTADO = "Cerrado";
        private const double TOTAL_PUERTAS = 146.0;
        private const string COMPONENTE = "Puerta";

       
		private List<Ticket> ObtenerTodosLosTicketsAIOoANIO(List<Ticket> tickets)
		{
			var ticketAPEGroup = tickets.Where(ticket =>
				 !string.IsNullOrEmpty(ticket.id_componente) && 
				  !string.IsNullOrEmpty(ticket.estado_ticket)

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
		public List<ReporteFallasPorPuerta> ContarFallasPorPuertaNoContratista(List<Ticket> tickets)
		{
			// Separar los tickets por puerta
			var ticketsPorPuerta = tickets
				.Where(ticket => ticket.estado_ticket != null && ticket.estado_ticket.Equals(ESTADO)
				&& ticket.tipo_componente != null && ticket.tipo_componente.Equals(COMPONENTE) && ticket.codigo_falla != null &&
					ticket.tipo_componente.Equals(COMPONENTE) && ticket.estado_ticket != null && ticket.estado_ticket.Equals(ESTADO)
					&& ticket.nivel_falla != null && ticket.tipo_causa != null  && !ticket.tipo_causa.Equals("A cargo del contratista")
				)
				.GroupBy(ticket => ticket.id_puerta);

			// Contar las fallas repetidas por cada una de las fallas en cada puerta cerrada
			var reportesFallasPorPuerta = new List<ReporteFallasPorPuerta>();
			foreach (var grupo in ticketsPorPuerta)
			{
				var fallasPorPuertaEnGrupo = grupo
					.Where(ticket => ticket.codigo_falla != null
					)
					.GroupBy(ticket => ticket.codigo_falla)
					.Select(grupoFallas => new FallaPorPuerta
					{
						CodigoFalla = grupoFallas.Key,
						Cantidad = grupoFallas.Count()
					})
					.ToList();

				var reporteFallasPorPuerta = new ReporteFallasPorPuerta
				{
					Puerta = grupo.Key,
					Fallas = fallasPorPuertaEnGrupo
				};

				reportesFallasPorPuerta.Add(reporteFallasPorPuerta);
			}

			return reportesFallasPorPuerta;
		}

		public List<ReporteFallasPorPuerta> ContarFallasPorPuertaContratista(List<Ticket> tickets)
        {
			// Separar los tickets por puerta
			var ticketsPorPuerta = tickets
				.Where(ticket => ticket.estado_ticket != null && ticket.estado_ticket.Equals(ESTADO)
				&& ticket.tipo_componente != null && ticket.tipo_componente.Equals(COMPONENTE) && ticket.codigo_falla != null &&
					ticket.tipo_componente.Equals(COMPONENTE) && ticket.estado_ticket != null && ticket.estado_ticket.Equals(ESTADO)
					&& ticket.nivel_falla != null &&  ticket.tipo_causa != null && ticket.tipo_causa.Equals("A cargo del contratista")
				)
				.GroupBy(ticket => ticket.id_puerta);

			// Contar las fallas repetidas por cada una de las fallas en cada puerta cerrada
			var reportesFallasPorPuerta = new List<ReporteFallasPorPuerta>();
			foreach (var grupo in ticketsPorPuerta)
			{
				var fallasPorPuertaEnGrupo = grupo
					.Where(ticket => ticket.codigo_falla != null
					)
					.GroupBy(ticket => ticket.codigo_falla)
					.Select(grupoFallas => new FallaPorPuerta
					{
						CodigoFalla = grupoFallas.Key,
						Cantidad = grupoFallas.Count()
					})
					.ToList();

				var reporteFallasPorPuerta = new ReporteFallasPorPuerta
				{
					Puerta = grupo.Key,
					Fallas = fallasPorPuertaEnGrupo
				};

				reportesFallasPorPuerta.Add(reporteFallasPorPuerta);
			}

			return reportesFallasPorPuerta;
		}
        public List<ReporteFallasPorPuerta> ContarFallasPorPuerta(List<Ticket> tickets)
        {
            // Separar los tickets por puerta
            var ticketsPorPuerta = tickets
                .Where(ticket => ticket.estado_ticket != null && ticket.estado_ticket.Equals(ESTADO)
                && ticket.tipo_componente != null && ticket.tipo_componente.Equals(COMPONENTE)&& ticket.codigo_falla!=null &&
                    ticket.tipo_componente.Equals(COMPONENTE) && ticket.estado_ticket != null && ticket.estado_ticket.Equals(ESTADO) 
                    && ticket.nivel_falla!=null
                )
                .GroupBy(ticket => ticket.id_puerta);

            // Contar las fallas repetidas por cada una de las fallas en cada puerta cerrada
            var reportesFallasPorPuerta = new List<ReporteFallasPorPuerta>();
            foreach (var grupo in ticketsPorPuerta)
            {
                var fallasPorPuertaEnGrupo = grupo
                    .Where(ticket => ticket.codigo_falla != null 
                    )
                    .GroupBy(ticket => ticket.codigo_falla)
                    .Select(grupoFallas => new FallaPorPuerta
                    {
                        CodigoFalla = grupoFallas.Key,
                        Cantidad = grupoFallas.Count()
                    })
                    .ToList();

                var reporteFallasPorPuerta = new ReporteFallasPorPuerta
                {
                    Puerta = grupo.Key,
                    Fallas = fallasPorPuertaEnGrupo
                };

                reportesFallasPorPuerta.Add(reporteFallasPorPuerta);
            }

            return reportesFallasPorPuerta;
        }

        public IRFEntity calculo_IRF_general(List<Ticket> tickets) { 
            IRFEntity irf = new IRFEntity(ContarFallasPorPuerta( tickets),TOTAL_PUERTAS,tickets);
            return irf;
        }
		public IRFEntity calculo_IRF_Contratista(List<Ticket> tickets)
		{
			IRFEntity irf = new IRFEntity(ContarFallasPorPuertaContratista(tickets), TOTAL_PUERTAS, tickets);
			return irf;
		}
		public IRFEntity calculo_IRF_NoContratista(List<Ticket> tickets)
		{
			IRFEntity irf = new IRFEntity(ContarFallasPorPuertaNoContratista(tickets), TOTAL_PUERTAS, tickets);
			return irf;
		}









	}


}
