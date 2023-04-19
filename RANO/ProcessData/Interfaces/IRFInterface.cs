using RANO.Model;

namespace RANO.ProcessData.Interfaces
{
    public interface IRFInterface
    {
        public List<ReporteFallasPorPuerta> ContarFallasPorPuerta(List<Ticket> tickets);
        public List<ReporteFallasPorPuerta> ContarFallasPorPuertaNoContratista(List<Ticket> tickets);

        public List<ReporteFallasPorPuerta> ContarFallasPorPuertaContratista(List<Ticket> tickets);

	}

}
