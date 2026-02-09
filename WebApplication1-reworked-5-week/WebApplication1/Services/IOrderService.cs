// <copyright file="IOrderService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebApplication1.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebApplication1.Models;

    /// <summary>
    /// Интерфейс сервиса заказов.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Создает заказ.
        /// </summary>
        /// <param name="order">Данные заказа.</param>
        /// <returns>Идентификатор.</returns>
        Task<Guid> CreateAsync(Order order);

        /// <summary>
        /// Удаляет заказ.
        /// </summary>
        /// <param name="id">Айди заказа.</param>
        /// <returns>Успешно или нет.</returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Получает все заказы.
        /// </summary>
        /// <param name="status">Фильтр по статусу.</param>
        /// <returns>Список заказов.</returns>
        Task<List<Order>> GetAllAsync(OrderStatus? status);

        /// <summary>
        /// Получает заказ по айди.
        /// </summary>
        /// <param name="id">Айди.</param>
        /// <returns>Заказ.</returns>
        Task<Order?> GetByIdAsync(Guid id);

        /// <summary>
        /// Считает сумму.
        /// </summary>
        /// <param name="status">Фильтр.</param>
        /// <returns>Сумма.</returns>
        Task<decimal> GetTotalSumAsync(OrderStatus? status);

        /// <summary>
        /// Обновляет заказ.
        /// </summary>
        /// <param name="updatedOrder">Данные.</param>
        /// <returns>Успешно или нет.</returns>
        Task<bool> UpdateAsync(Order updatedOrder);
    }
}