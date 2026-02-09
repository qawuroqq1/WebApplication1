// <copyright file="OrderConfiguration..cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace WebApplication1.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using WebApplication1.Models;

    /// <summary>
    /// Конфигурация для сущности заказа.
    /// </summary>
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderConfiguration"/> class.
        /// Инициализирует новый экземпляр класса <see cref="OrderConfiguration"/>.
        /// </summary>
        public OrderConfiguration()
        {
        }

        /// <inheritdoc/>
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            ArgumentNullException.ThrowIfNull(builder);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Status)
                .HasConversion<string>();
        }
    }
}