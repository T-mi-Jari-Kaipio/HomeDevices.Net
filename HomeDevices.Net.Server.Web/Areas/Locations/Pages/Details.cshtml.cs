using HomeDevices.Net.Server.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeDevices.Net.Server.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Areas.Locations.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly HomeDevices.Net.Server.Net.Data.ApplicationDbContext _context;

        public DetailsModel(HomeDevices.Net.Server.Net.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Location Location { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Location = await _context.Locations.FirstOrDefaultAsync(m => m.Id == id);

            if (Location == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
