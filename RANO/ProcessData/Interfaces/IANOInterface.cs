using RANO.Model;
using static RANO.Model.Ticket;

namespace RANO.ProcessData.Interfaces
{
    public interface IANOInterface
    {
        public List<List<Ticket>> ANIO_POR_PUERTA(List<Ticket> Ticket);
        public List<List<Ticket>> ANIO_POR_PUERTA_NO_CONTRATISTA(List<Ticket> Ticket);
        public List<List<Ticket>> ANIO_POR_PUERTA_CONTRATISTA(List<Ticket> Ticket);
    }
}
