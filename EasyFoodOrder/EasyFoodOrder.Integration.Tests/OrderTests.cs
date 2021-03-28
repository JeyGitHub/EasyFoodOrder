using System;
using System.Collections.Generic;
using EasyFoodOrder.Common.DataAccess.Database;
using EasyFoodOrder.Common.DataAccess.Migrations;
using EasyFoodOrder.Common.DataAccess.Models.Order;
using EasyFoodOrder.Services.Order;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EasyFoodOrder.Integration.Tests
{
    public class OrderTests: IDisposable
    {
        private readonly OrderDbContext _dbContext;

        public OrderTests()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            
            string connectionString = config.GetConnectionString("EasyFoodOrderTestsConnectionString");

            EasyFoodOrder.Common.DataAccess.Migrations.
            Database.EnsureDatabase(connectionString);
            var serviceProvider = new ServiceCollection().AddFluentMigratorCore()
                .AddEntityFrameworkSqlServer()
                .ConfigureRunner(options =>
                {
                    options.AddSqlServer()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(typeof(Database).Assembly)
                        .For.All();
                }).AddLogging(options =>
                {
                    options.AddFluentMigratorConsole();
                })
                .BuildServiceProvider();
            var builder = new DbContextOptionsBuilder<OrderDbContext>();
            builder.UseSqlServer(connectionString);

            if (serviceProvider.GetService(typeof(IMigrationRunner)) is IMigrationRunner migrationRunner)
            {
                migrationRunner.ListMigrations();
                migrationRunner.MigrateUp();
            }

            _dbContext = new OrderDbContext(builder.Options);
            _dbContext.Database.Migrate();


        }

        [Fact]
        public void Create_Order_Test()
        {
            var orderItems1 = new List<OrderItemModel>
            {
                new OrderItemModel
                {
                    RestaurantId = 1,
                    MenuItemId = 1,
                    Price = 120
                },
                new OrderItemModel
                {
                    RestaurantId = 1,
                    MenuItemId = 2,
                    Price = 100
                }
            };

            var orderItems2 = new List<OrderItemModel>
            {
                new OrderItemModel
                {
                    RestaurantId = 2,
                    MenuItemId = 3,
                    Price = 150
                },
                new OrderItemModel
                {
                    RestaurantId = 2,
                    MenuItemId = 4,
                    Price = 160
                },
                new OrderItemModel
                {
                    RestaurantId = 3,
                    MenuItemId = 5,
                    Price = 200
                }
            };

            IOrderService orderService = new OrderService(_dbContext);
            int newOrderId1 = orderService.CreateOrder(orderItems1);
            int newOrderId2 = orderService.CreateOrder(orderItems2);

            Assert.Equal(1, newOrderId1);
            Assert.Equal(2, newOrderId2);
        }

        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Database.EnsureDeleted();
                _dbContext.Dispose();
            }
        }
    }
}