using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyFoodOrder.Common.DataAccess.Models.Order
{
    [Table("OrderItems")]
    public class OrderItemModel
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }

        [Column("OrderId")]
        public int OrderId { get; set; }

        [Column("RestaurantId")]
        public int RestaurantId { get; set; }

        [Column("MenuItemId")]
        public int MenuItemId { get; set; }

        [Column("Price")]
        public decimal Price { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}