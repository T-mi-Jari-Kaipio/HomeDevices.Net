using HomeDevices.Net.Server.Web.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Pages
{
    public class BasePageModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public BasePageModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGetHealthAsync()
        {
            try
            {
                var url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/health";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("User-Agent", "HomeDevices.Net.Server.Web");

                var client = _clientFactory.CreateClient();

                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var health = await JsonSerializer.DeserializeAsync<Health>(responseStream);
                    if (health != null)
                    {
                        ViewData["Status"] = health?.results?.ruuvi_hc?.data?.Status;
                    }
                }
            }
            catch (System.Exception)
            {
            }
        }
    }
}
