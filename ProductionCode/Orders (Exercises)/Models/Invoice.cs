using System;

namespace ProductionCode.Orders.Models
{
    public class Invoice 
    {
        public int? InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? OrderId { get; set; }
    }
}