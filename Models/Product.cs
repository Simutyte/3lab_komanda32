﻿using System.ComponentModel.DataAnnotations;

namespace _3lab_komanda32.Models
{
    public class Product
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public double Discount { get; set; }
        public DateTime? DiscountStart { get; set; }
        public DateTime? DiscountEnd { get; set; }
        public int Quantity { get; set; }
        public bool Refundable { get; set; }
        public bool DeliveryOption { get; set; }
    }
}
