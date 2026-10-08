using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IndexWMSProject.Models
{
    public class B2BOrderItem
    {
        [Key]
        public int Id { get; set; }
        
        public int B2BOrderId { get; set; }
        [ForeignKey("B2BOrderId")]
        public B2BOrder Order { get; set; }
        
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        
        public int Quantity { get; set; }
        public bool IsPicked { get; set; } // For RF picking simulation
    }
}
