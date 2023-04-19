using RANO.Model;

namespace RANO.ProcessData.Interfaces
{
    public interface ICPMInterface
    {
		public List<Ticket> ObtenerTodosLosPreventivos(List<Ticket> Ticket);

		public List<Ticket> ObtenerTodosLosPreventivosComponente(List<Ticket> Ticket, string componente);

		public List<Ticket> ObtenerTodosLosPreventivosCerrados(List<Ticket> Ticket);
		public List<Ticket> ObtenerTodosLosPreventivosCerradosComponente(List<Ticket> Ticket, string componente);
		public ICPMEntity IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOAsync(List<Ticket> ticket);
		public ICPMEntity IndicadorDeCumplimientoEnEjecucionDelPlanDeMTTOComponenteAsync(List<Ticket> Ticket, string componente);
	}
}
