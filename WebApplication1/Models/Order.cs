using System;

namespace WebApplication1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Новый"; 
        public decimal Price { get; set; }           
    }
}