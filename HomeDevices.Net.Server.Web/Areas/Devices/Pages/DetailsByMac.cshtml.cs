using HomeDevices.Net.Server.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeDevices.Net.Server.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Areas.Devices.Pages
{
    public class DetailsByMacModel : PageModel
    {
        private readonly HomeDevices.Net.Server.Net.Data.ApplicationDbContext _context;

        public DetailsByMacModel(HomeDevices.Net.Server.Net.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Device Device { get; set; }

        public async Task<IActionResult> OnGetAsync(string mac)
        {
            if (string.IsNullOrWhiteSpace(mac))
            {
                return NotFound();
            }

            Device = await _context.Devices
                .Where(m => m.MacAddress.ToUpper() == mac.ToUpper())
                .Include(l => l.Location)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (Device == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
