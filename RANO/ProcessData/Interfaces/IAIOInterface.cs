using RANO.Model;

namespace RANO.ProcessData.Interfaces
{
    public interface IAIOInterface
    {
        public List<List<Ticket>> AIO_POR_PUERTA(List<Ticket> Ticket);
        public List<List<Ticket>> AIO_POR_PUERTA_NO_CONTRATISTA(List<Ticket> Ticket);
        public List<List<Ticket>> AIO_POR_PUERTA_CONTRATISTA(List<Ticket> Ticket);
    }
}
