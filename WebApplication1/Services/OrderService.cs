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

        public async Task<List<Order>> GetAllAsync(OrderStatus? status)
        {
            var query = _context.Orders.AsQueryable();
            if (status.HasValue) query = query.Where(o => o.Status == status.Value);
            return await query.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Guid> CreateAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.Id;
        }

        public async Task<bool> UpdateAsync(Order updatedOrder)
        {
            var exists = await _context.Orders.AnyAsync(o => o.Id == updatedOrder.Id);
            if (!exists) return false;
            _context.Entry(updatedOrder).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null) return false;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<decimal> GetTotalSumAsync(OrderStatus? status)
        {
            var query = _context.Orders.AsQueryable();
            if (status.HasValue) query = query.Where(o => o.Status == status.Value);
            return await query.SumAsync(o => o.Price);
        }
    }
}