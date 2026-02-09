// <copyright file="OrdersController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebApplication1.Controllers
{
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;
    using WebApplication1.Models;

    /// <summary>
    /// Контроллер для управления заказами.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IPublishEndpoint publishEndpoint;
        private readonly List<Order> orders = new List<Order>();

        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersController"/> class.
        /// </summary>
        /// <param name="publishEndpoint">Конечная точка для публикации сообщений.</param>
        public OrdersController(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
        }

        /// <summary>
        /// Получает список всех заказов.
        /// </summary>
        /// <returns>Список заказов.</returns>
        [HttpGet]
        public Task<IActionResult> GetAll() => Task.FromResult<IActionResult>(this.Ok(this.orders));

        /// <summary>
        /// Создает новый заказ.
        /// </summary>
        /// <param name="order">Данные заказа.</param>
        /// <returns>Результат операции.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            order.Id = Guid.NewGuid();
            order.Status = OrderStatus.New;
            this.orders.Add(order);

            await this.publishEndpoint.Publish(order).ConfigureAwait(false);

            return this.Ok(order);
        }

        /// <summary>
        /// Обновляет существующий заказ.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        /// <param name="order">Новые данные.</param>
        /// <returns>Результат обновления.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Order order)
        {
            ArgumentNullException.ThrowIfNull(order);

            var existingOrder = this.orders.Find(o => o.Id == id);
            if (existingOrder == null)
            {
                return this.NotFound();
            }

            existingOrder.Name = order.Name;
            existingOrder.Price = order.Price;
            existingOrder.Status = order.Status;

            await this.publishEndpoint.Publish(existingOrder).ConfigureAwait(false);

            return this.NoContent();
        }
    }
}