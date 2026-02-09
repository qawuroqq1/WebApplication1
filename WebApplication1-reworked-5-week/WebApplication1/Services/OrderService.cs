// <copyright file="OrderService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebApplication1.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using WebApplication1.Models;

    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class OrderService : IOrderService
    {
        private readonly AppDbContext context;
#pragma warning disable SA1614 // Element parameter documentation should have text
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderService"/> class.
        /// </summary>
        /// <param name="context"></param>
        public OrderService(AppDbContext context)
#pragma warning restore SA1614 // Element parameter documentation should have text
        {
            this.context = context;
        }

        /// <inheritdoc/>
        public async Task<List<Order>> GetAllAsync(OrderStatus? status)
        {
            var query = this.context.Orders.AsQueryable();
            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            return await query.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Order?> GetByIdAsync(Guid id)
        {
            return await this.context.Orders.FirstOrDefaultAsync(o => o.Id == id).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<Guid> CreateAsync(Order order)
        {
            this.context.Orders.Add(order);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
#pragma warning disable CA1062 // Проверить аргументы или открытые методы
            return order.Id;
#pragma warning restore CA1062 // Проверить аргументы или открытые методы
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateAsync(Order updatedOrder)
        {
            var exists = await this.context.Orders.AnyAsync(o => o.Id == updatedOrder.Id).ConfigureAwait(false);
            if (!exists)
            {
                return false;
            }

            this.context.Entry(updatedOrder).State = EntityState.Modified;
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteAsync(Guid id)
        {
            var order = await this.context.Orders.FirstOrDefaultAsync(o => o.Id == id).ConfigureAwait(false);
            if (order == null)
            {
                return false;
            }

            this.context.Orders.Remove(order);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        /// <inheritdoc/>
        public async Task<decimal> GetTotalSumAsync(OrderStatus? status)
        {
            var query = this.context.Orders.AsQueryable();
            if (status.HasValue)
            {
                query = query.Where(o => o.Status == status.Value);
            }

            return await query.SumAsync(o => o.Price).ConfigureAwait(false);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Ожидание>")]
#pragma warning disable CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
        private string GetDebuggerDisplay() => ToString();
#pragma warning restore CS8603 // Возможно, возврат ссылки, допускающей значение NULL.
    }
}