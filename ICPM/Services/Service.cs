using ICPM.Model;
using static ICPM.Model.Tickets;
using System.Text.Json;

namespace ICPM.Services
{
    public class Service
    {
        public async Task<List<Ticket>> PREVENTIVOAsync(string fechaInicial, string fechaFinal)
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            var client = new HttpClient(handler);
            string url = "https://192.168.0.79:5022/ticket/GetMantenimientoPreventivo";
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
