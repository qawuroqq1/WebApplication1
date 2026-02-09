// <copyright file="Order.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebApplication1.Models
{
    using System;

    /// <summary>
    /// Статус заказа.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>Новый.</summary>
        New,

        /// <summary>В процессе.</summary>
        InProgress,

        /// <summary>Завершен.</summary>
        Completed,
    }

    /// <summary>
    /// Модель заказа.
    /// </summary>
    public class Order
    {
        /// <summary>Gets or sets идентификатор.</summary>
        public Guid Id { get; set; }

        /// <summary>Gets or sets имя.</summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1206:Declaration keywords should follow order", Justification = "<Ожидание>")]
        public required string Name { get; set; }

        /// <summary>Gets or sets цену.</summary>
        public decimal Price { get; set; }

        /// <summary>Gets or sets статус.</summary>
        public OrderStatus Status { get; set; }
    }
}