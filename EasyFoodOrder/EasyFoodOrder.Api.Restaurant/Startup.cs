using System;
using System.Reflection;
using EasyFoodOrder.Api.Restaurant.Migrations;
using EasyFoodOrder.Common.DataAccess.Database;
using EasyFoodOrder.Services.Order;
using EasyFoodOrder.Services.Restaurant;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EasyFoodOrder.Api.Restaurant
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("EasyFoodOrderConnectionString");

            services.AddControllers();
            services.AddFluentMigratorCore()
                .ConfigureRunner(options =>
                {
                    options.AddSqlServer()
                        .WithGlobalConnectionString(connectionString)
                        .ScanIn(Assembly.GetExecutingAssembly())
                        .For.All();
                }).AddLogging(options =>
                {
                    options.AddFluentMigratorConsole();
                });

            services.AddCors(options => options.AddPolicy("CorsAllowAll",
                builder =>
                {
                    builder
                        .WithOrigins(Configuration["CORS_Origin"].Split(';', StringSplitOptions.RemoveEmptyEntries))
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }));

            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(connectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsAllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Database.EnsureDatabase(Configuration.GetConnectionString("EasyFoodOrderConnectionString"));

            using var scope = app.ApplicationServices.CreateScope();
            var migrationRunner = scope.ServiceProvider.GetService<IMigrationRunner>();
            if (migrationRunner != null)
            {
                migrationRunner.ListMigrations();
                migrationRunner.MigrateUp();
            }
        }
    }
}