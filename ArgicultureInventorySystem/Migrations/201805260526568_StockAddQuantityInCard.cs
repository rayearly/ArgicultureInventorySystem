namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StockAddQuantityInCard : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "QuantityInCard", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stocks", "QuantityInCard");
        }
    }
}
