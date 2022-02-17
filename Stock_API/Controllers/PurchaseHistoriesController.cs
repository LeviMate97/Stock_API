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
    public class PurchaseHistoriesController : ControllerBase
    {
        private readonly PortfoliosDbContext _context;

        public PurchaseHistoriesController(PortfoliosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseHistory>>> GetPurchaseHistory()
        {
            return await _context.PurchaseHistory.ToListAsync();
        }

        [HttpGet("{ticket}")]
        public async Task<ActionResult<IEnumerable<PurchaseHistory>>> GetPurchaseHistory(string ticket)
        {
            var purchaseHistory = await _context.PurchaseHistory
                .Where(p => p.Ticket == ticket)
                .ToListAsync();

            if (purchaseHistory == null)
            {
                return NotFound();
            }

            return purchaseHistory;
        }

        [HttpGet("GetByDate/{startDate}/endDate/ticket")]
        public async Task<ActionResult<IEnumerable<PurchaseHistory>>> GetPriceHistory(DateTime startDate, DateTime endDate, string ticket)
        {
            var purchaseHistory = await _context.PurchaseHistory
                .Where(p => (endDate == DateTime.MinValue) ? p.Date <= DateTime.Now : p.Date <= endDate)
                .Where(p => p.Date >= startDate)
                .Where(p => ticket == null || p.Ticket == ticket)
                .ToListAsync();

            if (purchaseHistory == null)
            {
                return NotFound();
            }

            return purchaseHistory;
        }

        [HttpPost("Purchase/{ticket}/{quantity}/{date}")]
        public async Task<ActionResult<PurchaseHistory>> PostPurchaseHistory([FromHeader]PurchaseHistory purchaseHistory)
        {
            _context.PurchaseHistory.Add(purchaseHistory);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PurchaseHistoryExists(purchaseHistory.Ticket))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetPurchaseHistory", new { ticket = purchaseHistory.Ticket }, purchaseHistory);
        }
        private bool PurchaseHistoryExists(string ticket)
        {
            return _context.PurchaseHistory.Any(p => p.Ticket == ticket);
        }
    }
}
