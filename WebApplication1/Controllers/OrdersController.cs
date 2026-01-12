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

        public OrdersController(OrderService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string? status)
            => Ok(await _service.GetAllAsync(status));

        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
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
        [HttpGet("total-price")]
        public async Task<ActionResult<decimal>> GetTotal([FromQuery] string? status)
        {
            var total = await _service.GetTotalSumAsync(status);
            return Ok(total);
        }
    }
}