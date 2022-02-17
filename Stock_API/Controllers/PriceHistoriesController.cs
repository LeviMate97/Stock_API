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
    public class PriceHistoriesController : ControllerBase
    {
        private readonly PortfoliosDbContext _context;

        public PriceHistoriesController(PortfoliosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriceHistory>>> GetPriceHistory()
        {
            return await _context.PriceHistory.ToListAsync();
        }

        [HttpGet("{ticket}")]
        public async Task<ActionResult<IEnumerable<PriceHistory>>> GetPriceHistory(string ticket)
        {
            var priceHistory = await _context.PriceHistory
                            .Where(p => p.Ticket == ticket)
                            .ToListAsync();

            if (priceHistory == null)
            {
                return NotFound();
            }

            return priceHistory;
        }

        [HttpGet("GetByDate/{startDate}/endDate/ticket")]
        public async Task<ActionResult<IEnumerable<PriceHistory>>> GetPriceHistory(DateTime startDate, DateTime endDate, string ticket)
        {
            var priceHistory = await _context.PriceHistory
                .Where(p => (endDate == DateTime.MinValue) ? p.Date <= DateTime.Now : p.Date <= endDate)
                .Where(p => p.Date >= startDate)
                .Where(p => ticket == null || p.Ticket == ticket)
                .ToListAsync();

            if (priceHistory == null)
            {
                return NotFound();
            }

            return priceHistory;
        }
    }
}
