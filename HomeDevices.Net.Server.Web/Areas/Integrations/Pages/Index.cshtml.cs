using HomeDevices.Net.Server.Web.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeDevices.Net.Server.Net.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Areas.Integrations.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        //public IList<Location> Locations { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            //Locations = await _context.Locations
            //    .OrderByDescending(o => o.Name)
            //    .AsNoTracking()
            //    .ToListAsync();
        }
    }
}
