using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IndexWMSProject.Models
{
    public class B2BOrder
    {
        [Key]
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; } // "Pending", "Processing", "Shipped"
        
        public List<B2BOrderItem> Items { get; set; } = new List<B2BOrderItem>();
    }
}
