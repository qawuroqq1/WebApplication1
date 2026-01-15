namespace WebApplication1.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using WebApplication1.Models;
    using WebApplication1.Services;

    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderService service;

        public OrdersController(OrderService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] OrderStatus? status)
        {
            return this.Ok(await this.service.GetAllAsync(status));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var order = await this.service.GetByIdAsync(id);
            if (order == null)
            {
                return this.NotFound();
            }

            return this.Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var id = await this.service.CreateAsync(order);
            return this.CreatedAtAction(nameof(this.GetById), new { id = id }, new { Id = id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Order order)
        {
            if (id != order.Id)
            {
                return this.BadRequest();
            }

            var result = await this.service.UpdateAsync(order);
            return result ? this.NoContent() : this.NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return await this.service.DeleteAsync(id) ? this.NoContent() : this.NotFound();
        }

        [HttpGet("total-price")]
        public async Task<IActionResult> GetTotalPrice([FromQuery] OrderStatus? status)
        {
            return this.Ok(await this.service.GetTotalSumAsync(status));
        }
    }
}