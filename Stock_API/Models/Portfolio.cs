using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock_API.Models
{
    public class Portfolio
    {
        public string Ticket { get; set; }
        public decimal Quantity { get; set; }
    }
    public class PurchaseHistory
    {
        public string Ticket { get; set; }
        public decimal Quantity { get; set; }
        public DateTime Date { get; set; }
    }
    public class PriceHistory
    {
        public string Ticket { get; set; }
        [Column("Close Price")]
        public decimal ClosePrice { get; set; }
        public DateTime Date { get; set; }
    }
    public class Dividends
    {
        public string Ticket { get; set; }
        public decimal Amount { get; set; }
        [Column("Ex-dividend date")]
        public DateTime ExDividendDate { get; set; }
    }
}
