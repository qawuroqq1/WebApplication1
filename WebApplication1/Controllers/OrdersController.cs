using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // ПОЛУЧЕНИЕ с фильтрами
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders([FromQuery] string? status = null)
        {
            var query = _context.Orders.AsQueryable();
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }
            return await query.ToListAsync();
        }

        // СОЗДАНИЕ
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); // Сохраняем в базу
            return Ok(order);
        }

        // СУММА
        [HttpGet("total-price")]
        public async Task<ActionResult<object>> GetTotalPrice([FromQuery] string? status = null)
        {
            var query = _context.Orders.AsQueryable();
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }
            var total = await query.SumAsync(o => o.Price);
            return Ok(new { TotalPrice = total });
        }
    }
}