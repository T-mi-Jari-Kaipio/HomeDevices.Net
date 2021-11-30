using HomeDevices.Net.Server.Web.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using HomeDevices.Net.Server.Net.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeDevices.Net.Server.Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuuviController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly LinkGenerator _linkGenerator;

        public RuuviController(ApplicationDbContext context,
            LinkGenerator linkGenerator)
        {
            _context = context;
            _linkGenerator = linkGenerator;
        }

        // GET: api/Ruuvi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuuviTag>>> GetRuuviTags()
        {
            List<RuuviTag> result = await _context.RuuviTags.ToListAsync();

            //var metadata = new
            //{
            //    result.TotalCount,
            //    result.PageSize,
            //    result.CurrentPage,
            //    result.TotalPages,
            //    result.HasNext,
            //    result.HasPrevious
            //};

            return result;
        }

        // GET: api/Ruuvi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LatestInfo>> GetRuuviTag(string id)
        {
            LatestInfo result = new();
            var ruuviTag = await _context.RuuviTags.FindAsync(id);

            if (ruuviTag == null)
            {
                return NotFound();
            }

            result.Value = ruuviTag;
            result.Links = new();
            result.Links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetRuuviTag), values: new { id }),
                    "self",
                    "GET"));

            return result;
        }

        [HttpGet("latest")]
        public async Task<ActionResult<LatestInfo>> GetLatestRuuviTag()
        {
            LatestInfo result = new();
            var ruuviTags = await _context.RuuviTags.OrderByDescending(o => o.CreatedOn).Take(2).AsNoTracking().ToListAsync();

            if (ruuviTags.Count == 0)
            {
                return NotFound();
            }

            result.Value = ruuviTags.First();
            result.Links = new();
            result.Links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetRuuviTag), values: new { ruuviTags.First().Id }),
                    "self",
                    "GET"));

            result.Links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetRuuviTag), values: new { ruuviTags.Last().Id }),
                    "next",
                    "GET"));

            return result;
        }

        // PUT: api/Ruuvi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutRuuviTag(string id, RuuviTag ruuviTag)
        //{
        //    if (id != ruuviTag.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(ruuviTag).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!RuuviTagExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Ruuvi
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<RuuviTag>> PostRuuviTag(RuuviTag ruuviTag)
        //{
        //    _context.RuuviTags.Add(ruuviTag);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (RuuviTagExists(ruuviTag.Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetRuuviTag", new { id = ruuviTag.Id }, ruuviTag);
        //}

        //// DELETE: api/Ruuvi/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteRuuviTag(string id)
        //{
        //    var ruuviTag = await _context.RuuviTags.FindAsync(id);
        //    if (ruuviTag == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.RuuviTags.Remove(ruuviTag);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool RuuviTagExists(string id)
        //{
        //    return _context.RuuviTags.Any(e => e.Id == id);
        //}
    }
}
