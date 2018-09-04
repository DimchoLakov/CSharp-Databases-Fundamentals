using FastFood.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace FastFood.Models
{
    public class Order
    {
        public Order()
        {
            this.OrderItems = new List<OrderItem>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Customer { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        [DefaultValue(0)]
        public OrderType Type { get; set; }

        [NotMapped]
        [Required]
        public decimal TotalPrice => this.OrderItems.Select(io => io.Item.Price).DefaultIfEmpty(0).Sum();

        [Required]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
