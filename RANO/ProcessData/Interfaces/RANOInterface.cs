using RANO.Model;
using static RANO.Model.Ticket;

namespace RANO.ProcessData.Interfaces
{
    public interface RANOInterface
    {
		public List<Ticket> ANIO_CERRADO_A_TIEMPO(List<Ticket> Ticket);

		public List<Ticket> ANIO_ABIERTO_SIN_RETRASO(List<Ticket> Ticket);

		public List<Ticket> ANIO_A_CARGO_CONTRATISTA(List<Ticket> Ticket);

		public List<Ticket> ANIO_NO_A_CARGO_CONTRATISTA(List<Ticket> Ticket);


	}
}
