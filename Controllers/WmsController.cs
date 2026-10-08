using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IndexWMSProject.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IndexWMSProject.Controllers
{
    public class WmsController : Controller
    {
        private readonly WmsDbContext _context;

        public WmsController(WmsDbContext context)
        {
            _context = context;
        }

        // B2B Dashboard / Orders List
        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
            return View(orders);
        }

        // RF Terminal view for a specific order
        public async Task<IActionResult> RfTerminal(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // API Endpoint for RF Scanner simulating barcode reading
        [HttpPost]
        public async Task<IActionResult> ScanBarcode(int orderId, string barcode)
        {
            var order = await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return Json(new { success = false, message = "Sipariş bulunamadı." });

            var item = order.Items.FirstOrDefault(i => i.Product.Barcode == barcode);

            if (item == null)
                return Json(new { success = false, message = "Okutulan ürün bu siparişte yok." });

            if (item.IsPicked)
                return Json(new { success = false, message = "Bu ürün zaten okutulmuş." });

            // Simulate picking
            item.IsPicked = true;
            
            // Decrease stock
            item.Product.StockQuantity -= item.Quantity;

            // Check if all items are picked to update order status
            if (order.Items.All(i => i.IsPicked))
            {
                order.Status = "Shipped"; // Hazırlandı / Sevk Edildi
            }
            else
            {
                order.Status = "Processing"; // Toplanıyor
            }

            await _context.SaveChangesAsync();

            return Json(new { 
                success = true, 
                message = "Ürün başarıyla okutuldu ve stoktan düşüldü.",
                productName = item.Product.Name,
                orderStatus = order.Status
            });
        }
    }
}
