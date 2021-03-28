using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyFoodOrder.Common.DataAccess.Models.Order
{
    [Table("Orders")]
    public class OrderModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; }

        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}