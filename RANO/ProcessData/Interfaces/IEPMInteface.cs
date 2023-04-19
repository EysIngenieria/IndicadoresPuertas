using RANO.Model;

namespace RANO.ProcessData.Interfaces
{
    public interface IEPMInterface
	{
		public List<Ticket> ObtenerTodosLosTicketsCerrados(List<Ticket> Ticket);
		public List<Ticket> ObtenerTodosLosPreventivosCerrados(List<Ticket> Ticket);
		public List<Ticket> ObtenerTodosLosCorrectivosCerrados(List<Ticket> Ticket);
		public List<Ticket> ObtenerTicketsNoACargoContratista(List<Ticket>? tickets);
		public List<Ticket> obtenerTicketsACargoContratista(List<Ticket>? Ticket);
		public IEPMEntity IndicadorDeEfectividadDelPlanDeMantenimiento(List<Ticket> ticket);
		public IEPMEntity IndicadorDeEfectividadDelPlanDeMantenimientoContratista(List<Ticket> ticket);
		public IEPMEntity IndicadorDeEfectividadDelPlanDeMantenimientoNoContratista(List<Ticket> ticket);
	}
}
