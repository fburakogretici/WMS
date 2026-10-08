using System.ComponentModel.DataAnnotations;

namespace IndexWMSProject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public int StockQuantity { get; set; }
        public string Location { get; set; } // e.g. A1-05
    }
}
