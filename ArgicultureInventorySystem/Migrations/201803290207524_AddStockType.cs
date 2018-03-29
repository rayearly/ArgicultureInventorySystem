namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStockType : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO StockTypes (StockTypeAssigned) VALUES ('Tool')");
            Sql("INSERT INTO StockTypes (StockTypeAssigned) VALUES ('Pesticide')");
            Sql("INSERT INTO StockTypes (StockTypeAssigned) VALUES ('Fertilizer')");
        }
        
        public override void Down()
        {
        }
    }
}
