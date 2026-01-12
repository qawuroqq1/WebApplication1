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

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] OrderStatus? status) => Ok(await _service.GetAllAsync(status));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await _service.GetByIdAsync(id);
            return order == null ? NotFound() : Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var id = await _service.CreateAsync(order);
            return CreatedAtAction(nameof(GetById), new { id = id }, new { Id = id });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) => await _service.DeleteAsync(id) ? NoContent() : NotFound();

        [HttpGet("total-price")]
        public async Task<ActionResult<decimal>> GetTotalPrice([FromQuery] OrderStatus? status) => Ok(await _service.GetTotalSumAsync(status));
    }
}