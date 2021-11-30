using HomeDevices.Net.Server.Web.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using HomeDevices.Net.Server.Net.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Areas.Devices.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<Device> Devices { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Devices = await _context.Devices
                .OrderByDescending(o => o.CreatedOn)
                .AsNoTracking()
                .Include(l => l.Location)
                .ToListAsync();
        }
    }
}
