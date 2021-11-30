using HomeDevices.Net.Server.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeDevices.Net.Server.Net.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Areas.Data.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<RuuviTag> RuuviTags { get; set; }
        public int TotalRows { get; set; }
        public DateTime LogStart { get; set; }
        public DateTime LogEnd { get; set; }
        [BindProperty]
        public DateTime SearchStart { get; set; }
        [BindProperty]
        public DateTime SearchEnd { get; set; }

        [BindProperty]
        public Device Device { get; set; }

        [BindProperty]
        public List<SelectListItem> Devices { get; set; }

        [BindProperty]
        public List<SelectListItem> Locations { get; set; }

        [BindProperty]
        public Location Location { get; set; }

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            //await GenerateTestData();
            await GetDefaultData();
            RuuviTags = await _context.RuuviTags
                .Where(a => a.CreatedOn >= SearchStart && a.CreatedOn <= SearchEnd)
                .OrderByDescending(o => o.CreatedOn)
                .AsNoTracking()
                .ToListAsync();
        }

        private async Task GetDefaultData()
        {
            TotalRows = await _context.RuuviTags.CountAsync();
            LogStart = await _context.RuuviTags.MinAsync(t => t.CreatedOn);
            LogEnd = await _context.RuuviTags.MaxAsync(t => t.CreatedOn);

            Locations = await _context.Locations.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Name
                                  }).ToListAsync();

            Devices = await _context.Devices.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id.ToString(),
                                      Text = a.Name
                                  }).ToListAsync();

            if (SearchStart == DateTime.MinValue)
                SearchStart = DateTime.Now.AddDays(-1);
            if (SearchEnd == DateTime.MinValue)
                SearchEnd = DateTime.Now;
        }

        public async Task<IActionResult> OnPostDeleteAllAsync()
        {
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE RuuviTags");
            RuuviTags = new List<RuuviTag>();
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteByDateAsync(DateTime start, DateTime end)
        {
            var items = await _context.RuuviTags.Where(r => r.CreatedOn >= start && r.CreatedOn <= end).ToListAsync();
            if (items.Any())
            {
                _context.RuuviTags.RemoveRange(items);
                await _context.SaveChangesAsync();
            }

            await GetDefaultData();

            return Page();
        }

        private async Task GenerateTestData()
        {
            for (int i = 0; i < 100; i++)
            {
                RuuviTag tag = new()
                {
                    AccelerationX = i,
                    AccelerationY = i,
                    AccelerationZ = i,
                    AirPressure = i,
                    BatteryVoltage = i,
                    CreatedOn = DateTime.Now,
                    DataFormat = 1,
                    Deleted = false,
                    Humidity = i,
                    Id = Guid.NewGuid().ToString(),
                    MacAddress = Guid.NewGuid().ToString(),
                    MeasurementSequenceNumber = i,
                    MovementCounter = i,
                    Temperature = i,
                    TxPower = i
                };

                _context.RuuviTags.Add(tag);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IActionResult> OnPostSearchByTermsAsync(
            string locationId,
            string deviceId)
        {
            await GetDefaultData();

            if (!string.IsNullOrWhiteSpace(deviceId))
            {
                Device = await _context.Devices.FindAsync(deviceId);
                if (Device != null)
                {
                    RuuviTags = await _context.RuuviTags
                        .Where(r => r.CreatedOn >= SearchStart && r.CreatedOn <= SearchEnd && r.MacAddress == Device.MacAddress)
                        .OrderByDescending(o => o.CreatedOn)
                        .AsNoTracking()
                        .ToListAsync();
                }
            }
            else if (!string.IsNullOrWhiteSpace(locationId))
            {
                Location = await _context.Locations.FindAsync(locationId);
                if (Location != null)
                {
                    List<string> macs = Location.Devices.Select(d => d.MacAddress).ToList();
                    if (macs.Any())
                    {
                        RuuviTags = await _context.RuuviTags
                            .Where(r => r.CreatedOn >= SearchStart && r.CreatedOn <= SearchEnd && macs.Contains(r.MacAddress))
                            .OrderByDescending(o => o.CreatedOn)
                            .AsNoTracking()
                            .ToListAsync();
                    }
                }
            }
            else
            {
                RuuviTags = await _context.RuuviTags
                    .Where(r => r.CreatedOn >= SearchStart && r.CreatedOn <= SearchEnd)
                    .OrderByDescending(o => o.CreatedOn)
                    .AsNoTracking()
                    .ToListAsync();
            }

            return Page();
        }

    }
}
