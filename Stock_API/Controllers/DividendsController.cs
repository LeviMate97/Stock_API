using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stock_API.Models;


namespace Stock_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DividendsController : ControllerBase
    {
        private readonly PortfoliosDbContext _context;

        public DividendsController(PortfoliosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dividends>>> GetDividends()
        {
            return await _context.Dividends.ToListAsync();
        }

        [HttpGet("{ticket}")]
        public async Task<ActionResult<IEnumerable<Dividends>>> GetDividend(string ticket)
        {
            var dividends = await _context.Dividends
                            .Where(d => d.Ticket == ticket)                
                            .ToListAsync();

            if (dividends == null)
            {
                return NotFound();
            }

            return dividends;
        }

        [HttpGet("GetByDate/{startDate}/endDate/ticket")]
        public async Task<ActionResult<IEnumerable<Dividends>>> GetDividends(DateTime startDate, DateTime endDate, string ticket)
        {
            var dividends = await _context.Dividends
                    .Where(d => (endDate == DateTime.MinValue) ? d.ExDividendDate <= DateTime.Now : d.ExDividendDate <= endDate)
                    .Where(d => d.ExDividendDate >= startDate)
                    .Where(d => ticket == null || d.Ticket == ticket)
                    .ToListAsync();

            if (dividends == null)
            {
                return NotFound();
            }

            return dividends;
        }
    }
}
