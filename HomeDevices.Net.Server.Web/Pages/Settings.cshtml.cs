using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace HomeDevices.Net.Server.Web.Pages
{
    public class SettingsModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public SettingsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string AdapterName { get; private set; }
        [BindProperty]
        public int PollingInterval { get; private set; }
        [BindProperty]
        public int ScanDurationSeconds { get; private set; }
        [BindProperty]
        public string Subdirectory { get; private set; }

        public void OnGet()
        {
            AdapterName = _configuration.GetValue<string>("DeviceServer:AdapterName");
            PollingInterval = _configuration.GetValue<int>("DeviceServer:PollingInterval");
            ScanDurationSeconds = _configuration.GetValue<int>("DeviceServer:ScanDurationSeconds");
            Subdirectory = _configuration.GetValue<string>("DeviceServer:Subdirectory");
        }
    }
}
