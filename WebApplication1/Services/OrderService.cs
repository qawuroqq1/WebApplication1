using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
namespace WebApplication1.Services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetAllAsync(string? status)
        {
            var query = _context.Orders.AsQueryable();
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }
            return await query.ToListAsync();
        }    
        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders.FindAsync(id);
        }
        public async Task<int> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync(); 
            return order.Id;
        }
        public async Task<decimal> GetTotalSumAsync(string? status)
        {
            var query = _context.Orders.AsQueryable();
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }
            return await query.SumAsync(o => o.Price);
        }
    }
}