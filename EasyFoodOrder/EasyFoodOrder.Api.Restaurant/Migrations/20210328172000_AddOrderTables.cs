using FluentMigrator;

namespace EasyFoodOrder.Api.Restaurant.Migrations
{
    [Migration(20210328172000)]
    public class AddOrderTables: ForwardOnlyMigration
    {
        public override void Up()
        {
            this.Create.Table("Orders")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable();

            this.Create.Table("OrderItems")
                .WithColumn("Id").AsInt64().Identity().PrimaryKey()
                .WithColumn("OrderId").AsInt32().ForeignKey("Orders", "Id")
                .WithColumn("RestaurantId").AsInt32().NotNullable() // here should be FK to Restaurants table
                .WithColumn("MenuItemId").AsInt32().NotNullable() // here should be FK to MenuItems table
                .WithColumn("Price").AsDecimal(18, 2).NotNullable()
                .WithColumn("CreatedAt").AsDateTime().NotNullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable();
        }
    }
}