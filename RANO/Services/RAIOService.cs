using RANO.Model;
using static RANO.Model.Tickets;
using System.Text.Json;

namespace RAIO.Services
{
    public class RAIOService
    {
        public async Task<List<Ticket>> AIOSAsync(string fechaInicial, string fechaFinal)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var client = new HttpClient(handler);
            string url = "https://192.168.0.79:5022/ticket/GetByAIO";
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
