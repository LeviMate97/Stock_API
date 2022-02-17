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
    public class PortfoliosController : ControllerBase
    {
        private readonly PortfoliosDbContext _context;

        public PortfoliosController(PortfoliosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Portfolio>>> GetPortfolio()
        {
            return await _context.Portfolio.ToListAsync();
        }

        [HttpGet("{ticket}")]
        public async Task<ActionResult<Portfolio>> GetPortfolio(string ticket)
        {
            var portfolio = await _context.Portfolio
                .Where(p => p.Ticket == ticket)
                .FirstOrDefaultAsync();

            if (portfolio == null)
            {
                return NotFound();
            }

            return portfolio;
        }
    }
}
