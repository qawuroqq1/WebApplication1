using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

public class OrdersController : ControllerBase
{
 

    public OrdersController(OrderService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) => Ok(await _service.GetByIdAsync(id));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id) => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}