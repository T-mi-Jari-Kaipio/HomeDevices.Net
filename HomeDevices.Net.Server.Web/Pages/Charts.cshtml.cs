using HomeDevices.Net.Server.Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeDevices.Net.Server.Net.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Pages
{
    public class ChartsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<RuuviTag> RuuviTags { get; set; }
        public int TotalRows { get; set; }
        public DateTime LogStart { get; set; }
        public DateTime LogEnd { get; set; }
        [BindProperty]
        public List<ChartModel> Temperatures { get; set; }
        [BindProperty]
        public List<ChartModel> Humidies { get; set; }
        [BindProperty]
        public List<ChartModel> AirPressures { get; set; }
        [BindProperty]
        [DisplayFormat(DataFormatString = "F", ApplyFormatInEditMode = true)]
        public double? LowTemp { get; set; }
        [BindProperty]
        [DisplayFormat(DataFormatString = "F", ApplyFormatInEditMode = true)]
        public double? HighTemp { get; set; }
        [BindProperty]
        [DisplayFormat(DataFormatString = "F", ApplyFormatInEditMode = true)]
        public double? AverageTemp { get; set; }
        [BindProperty]
        [DisplayFormat(DataFormatString = "F", ApplyFormatInEditMode = true)]
        public double? LowHumidy { get; set; }
        [BindProperty]
        [DisplayFormat(DataFormatString = "F", ApplyFormatInEditMode = true)]
        public double? HighHumidy { get; set; }
        [BindProperty]
        [DisplayFormat(DataFormatString = "F", ApplyFormatInEditMode = true)]
        public double? AverageHumidy { get; set; }
        [BindProperty]
        public List<ChartModel> TempInfo { get; set; }
        [BindProperty]
        public List<ChartModel> HumInfo { get; set; }
        [BindProperty]
        public RuuviTag LatestTag { get; set; }
        [BindProperty]
        public List<SelectListItem> Devices { get; set; }
        [BindProperty]
        public Device SelectedDevice { get; set; }
        [BindProperty]
        public bool InfoFound { get; set; }

        public ChartsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task OnGetAsync(string id)
        {
            Devices = await _context.Devices.Select(a =>
                                  new SelectListItem
                                  {
                                      Value = a.Id,
                                      Text = a.Name
                                  }).ToListAsync();

            if (string.IsNullOrEmpty(id))
            {
                SelectedDevice = await _context.Devices.FirstOrDefaultAsync();
            }
            else
            {
                SelectedDevice = await _context.Devices.FirstOrDefaultAsync(x => x.Id == id);
            }

            TotalRows = await _context.RuuviTags.CountAsync();
            LogStart = await _context.RuuviTags.MinAsync(t => t.CreatedOn);
            LogEnd = await _context.RuuviTags.MaxAsync(t => t.CreatedOn);

            if (SelectedDevice != null)
            {

                RuuviTags = await _context.RuuviTags
                    .Where(t => t.MacAddress.Trim().ToUpper() == SelectedDevice.MacAddress.Trim().ToUpper())
                    .OrderByDescending(o => o.CreatedOn)
                    .AsNoTracking()
                    .ToListAsync();

                if (RuuviTags.Count > 0)
                {
                    InfoFound = true;
                    LatestTag = RuuviTags.OrderByDescending(o => o.CreatedOn).FirstOrDefault();

                    Temperatures = RuuviTags
                        .OrderByDescending(o => o.CreatedOn)
                        .Select(t => new ChartModel { Data = t.Temperature.Value.ToString(CultureInfo.InvariantCulture), Label = t.CreatedOn.ToString() }).ToList();

                    var lowestTemp = RuuviTags.OrderBy(o => o.Temperature).First();
                    var highestTemp = RuuviTags.OrderByDescending(o => o.Temperature).First();

                    LowTemp = lowestTemp.Temperature.Value;
                    HighTemp = highestTemp.Temperature.Value;
                    AverageTemp = RuuviTags.Average(o => o.Temperature);

                    TempInfo = new();

                    ChartModel chartModel = new()
                    {
                        Data = lowestTemp.Temperature.Value.ToString(CultureInfo.InvariantCulture),
                        Label = lowestTemp.CreatedOn.ToString()
                    };

                    TempInfo.Add(chartModel);

                    chartModel = new()
                    {
                        Data = highestTemp.Temperature.Value.ToString(CultureInfo.InvariantCulture),
                        Label = highestTemp.CreatedOn.ToString()
                    };

                    TempInfo.Add(chartModel);

                    chartModel = new()
                    {
                        Data = AverageTemp.Value.ToString(CultureInfo.InvariantCulture),
                        Label = string.Format("Average from period {0} - {1}", LogStart, LogEnd)
                    };

                    TempInfo.Add(chartModel);

                    Humidies = RuuviTags
                        .OrderByDescending(o => o.CreatedOn)
                        .Where(t => t.MacAddress.Trim().ToUpper() == SelectedDevice.MacAddress.Trim().ToUpper())
                        .Select(t => new ChartModel { Data = t.Humidity.Value.ToString(CultureInfo.InvariantCulture), Label = t.CreatedOn.ToString() }).ToList();

                    var lowestHum = RuuviTags.OrderBy(o => o.Humidity).First();
                    var highestHum = RuuviTags.OrderByDescending(o => o.Humidity).First();

                    AverageHumidy = RuuviTags.Average(t => t.Humidity);
                    LowHumidy = lowestHum.Humidity.Value;
                    HighHumidy = highestHum.Humidity.Value;

                    HumInfo = new();

                    chartModel = new()
                    {
                        Data = lowestHum.Humidity.Value.ToString(CultureInfo.InvariantCulture),
                        Label = lowestHum.CreatedOn.ToString()
                    };

                    HumInfo.Add(chartModel);

                    chartModel = new()
                    {
                        Data = highestHum.Humidity.Value.ToString(CultureInfo.InvariantCulture),
                        Label = highestHum.CreatedOn.ToString()
                    };

                    HumInfo.Add(chartModel);

                    chartModel = new()
                    {
                        Data = AverageHumidy.Value.ToString(CultureInfo.InvariantCulture),
                        Label = string.Format("Average from period {0} - {1}", LogStart, LogEnd)
                    };

                    HumInfo.Add(chartModel);
                }
            }
            else
                return;

        }

        public ActionResult OnPost(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./Charts", new { id = id });
        }
    }
}
