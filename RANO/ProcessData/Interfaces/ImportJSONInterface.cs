namespace RANO.ProcessData.Interfaces
{
    public interface ImportJSONInterface<T>
    {

        public List<T> getExportTicketFromFile(string filePath);
    }
}