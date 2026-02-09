// <copyright file="IOrderConfiguration.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models;

namespace WebApplication1.Configurations
{
#pragma warning disable SA1600 // Elements should be documented
    public interface IOrderConfiguration
#pragma warning restore SA1600 // Elements should be documented
    {
#pragma warning disable SA1600 // Elements should be documented
        void Configure(EntityTypeBuilder<Order> builder);
#pragma warning restore SA1600 // Elements should be documented
    }
}