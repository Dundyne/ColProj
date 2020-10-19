using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CollectivesController : ControllerBase
    {
        private readonly DataContext _context;

        public CollectivesController(DataContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("collectivecount")]
        public int GetCollectiveCount()
        {
            return _context.Collectives.Count();
        }

        // GET: /Collectives
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collective>>> GetCollectives()
        {
            return await _context.Collectives.ToListAsync();
        }

        // GET: /Collectives/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Collective>> GetCollective(int id)
        {
            var collective = await _context.Collectives.FindAsync(id);

            if (collective == null)
            {
                return NotFound();
            }

            return collective;
        }

        // PUT: /Collectives/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollective(int id, [FromBody]Collective collective)
        {
            if (id != collective.Id)
            {
                return BadRequest();
            }

            _context.Entry(collective).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectiveExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: /Collectives
        [HttpPost]
        public async Task<ActionResult<Collective>> PostCollective(Collective collective)
        {
            _context.Collectives.Add(collective);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCollective", new { id = collective.Id }, collective);
        }

        // DELETE: /Collectives/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Collective>> DeleteCollective(int id)
        {
            var collective = await _context.Collectives.FindAsync(id);

            if (collective == null)
            {
                return NotFound();
            }

            _context.Collectives.Remove(collective);
            await _context.SaveChangesAsync();

            return collective;
        }

        private bool CollectiveExists(int id)
        {
            return _context.Collectives.Any(e => e.Id == id);
        }
    }
}
