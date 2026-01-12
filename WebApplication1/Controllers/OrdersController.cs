using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService _service; 

        public OrdersController(OrderService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _service.GetByIdAsync(id);
            return order == null ? NotFound() : Ok(order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Order order)
        {
            if (id != order.Id) return BadRequest();
            return await _service.UpdateAsync(order) ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await _service.DeleteAsync(id) ? NoContent() : NotFound();
        }
    }
}