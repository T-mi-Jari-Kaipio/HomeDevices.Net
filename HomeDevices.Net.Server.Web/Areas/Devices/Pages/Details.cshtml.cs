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
    public class DetailsModel : PageModel
    {
        private readonly HomeDevices.Net.Server.Net.Data.ApplicationDbContext _context;

        public DetailsModel(HomeDevices.Net.Server.Net.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Device Device { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Device = await _context.Devices.Include(l => l.Location).FirstOrDefaultAsync(m => m.Id == id);

            if (Device == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
