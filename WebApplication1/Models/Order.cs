using System;

namespace WebApplication1.Models
{
    public enum OrderStatus { New, Processing, Completed, Cancelled }

    public class Order
    {
        public Guid Id { get; init; }
        public required string Name { get; init; } 
        public DateTime OrderDate { get; init; }
        public OrderStatus Status { get; init; }
        public decimal Price { get; init; }
    }
}