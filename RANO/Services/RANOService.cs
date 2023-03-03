using RANO.Model;
using static RANO.Model.Tickets;
using System.Text.Json;

namespace RANO.Services
{
    public class RANOService
    {
        public async Task<List<Ticket>> ANIOSAsync(string fechaInicial, string fechaFinal)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var client = new HttpClient(handler);
            string url = "https://192.168.0.79:5022/ticket/GetByANIO";
            UriBuilder uriBuilder = new UriBuilder(url);
            uriBuilder.Query = $"initialDate={fechaInicial}&endDate={fechaFinal}";
            string urlCompleta = uriBuilder.ToString();
            var response = await client.GetAsync(urlCompleta);
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();

                var responseStream = await response.Content.ReadAsStreamAsync();
                List<Ticket> tickets = await JsonSerializer.DeserializeAsync<List<Ticket>>(responseStream);

                return tickets;
            }
            return null;
        }
    }
}
