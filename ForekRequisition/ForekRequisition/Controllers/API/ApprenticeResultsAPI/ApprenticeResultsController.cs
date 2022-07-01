#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForekRequisition.Data;
using ForekRequisition.Models;

namespace ForekRequisition.Controllers.API.ApprenticeResultsAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprenticeResultsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApprenticeResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApprenticeResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApprenticeResult>>> GetApprenticeResult()
        {
            return await _context.AppResults.ToListAsync();
        }

        // GET: api/ApprenticeResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApprenticeResult>> GetApprenticeResult(Guid id)
        {
            var apprenticeResult = await _context.AppResults.FindAsync(id);

            if (apprenticeResult == null)
            {
                return NotFound();
            }

            return apprenticeResult;
        }

        // PUT: api/ApprenticeResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApprenticeResult(Guid id, ApprenticeResult apprenticeResult)
        {
            if (id != apprenticeResult.Id)
            {
                return BadRequest();
            }

            _context.Entry(apprenticeResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApprenticeResultExists(id))
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

        // POST: api/ApprenticeResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApprenticeResult>> PostApprenticeResult(ApprenticeResult apprenticeResult)
        {
            _context.AppResults.Add(apprenticeResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApprenticeResult", new { id = apprenticeResult.Id }, apprenticeResult);
        }

        // DELETE: api/ApprenticeResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApprenticeResult(Guid id)
        {
            var apprenticeResult = await _context.AppResults.FindAsync(id);

            if (apprenticeResult == null)
            {
                return NotFound();
            }

            _context.AppResults.Remove(apprenticeResult);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApprenticeResultExists(Guid id)
        {
            return _context.AppResults.Any(e => e.Id == id);
        }
    }
}
