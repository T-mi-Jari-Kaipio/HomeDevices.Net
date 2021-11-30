using BleReaderNet.Reader;
using BleReaderNet.Wrapper.DotNetBlueZ;
using HomeDevices.Net.Server.Web.Model;
using HomeDevices.Net.Server.Web.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using HomeDevices.Net.Server.Net.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Net.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string Status { get; set; }
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public List<ChartModel> LatestDatalist { get; set; }

        [BindProperty]
        public List<Device> Devices { get; set; }

        public IndexModel(ILogger<IndexModel> logger,
            IConfiguration configuration,
            ApplicationDbContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            ViewData["Error"] = null;

            //try
            //{
            //    var adapterName = _configuration["RuuviServer:AdapterName"].ToString();
            //    var scanDurationSeconds = 5;
            //    IBleReader reader = new BleReader(new DotNetBlueZService());
            //    await reader.ScanAsync(adapterName, scanDurationSeconds);
            //}
            //catch (Exception)
            //{
            //    ViewData["Error"] = "Problem opening Bluetooth. Is it enabled in this computer and is this application running in Linux?";
            //}

            //Devices = await _context.Devices.ToListAsync();

            @ViewData["LogCount"] = await _context.RuuviTags.CountAsync();
            @ViewData["LocationCount"] = await _context.Locations.CountAsync();
            @ViewData["DeviceCount"] = await _context.Devices.CountAsync();
            //LatestDatalist = new();

            //var ruuviTags = await _context.RuuviTags
            //    .OrderByDescending(o => o.CreatedOn)
            //    .AsNoTracking()
            //    .ToListAsync();

            //ruuviTags = ruuviTags.OrderBy(t => t.CreatedOn).Where(d => d.Temperature.HasValue).ToList();

            //foreach (var item in ruuviTags)
            //{
            //    LatestDatalist.Add(new ChartModel { Label = item.CreatedOn.ToString(), Data = item.Temperature.Value.ToString(CultureInfo.InvariantCulture) });
            //}

        }
    }
}
