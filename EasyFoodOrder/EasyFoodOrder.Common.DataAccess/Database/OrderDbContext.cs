using EasyFoodOrder.Common.DataAccess.Models.Order;
using Microsoft.EntityFrameworkCore;

namespace EasyFoodOrder.Common.DataAccess.Database
{
    public class OrderDbContext: DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options): base(options) { }

        public DbSet<OrderModel> Orders { get; set; }

        public DbSet<OrderItemModel> OrderItems { get; set; }
    }
}