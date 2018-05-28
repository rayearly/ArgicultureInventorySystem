namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRequiredInModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Stocks", "TypeId", "dbo.StockTypes");
            DropIndex("dbo.Stocks", new[] { "TypeId" });
            AlterColumn("dbo.DepartmentFaculties", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Stocks", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Stocks", "TypeId", c => c.Int(nullable: false));
            AlterColumn("dbo.StockMeasurements", "MeasurementType", c => c.String(nullable: false));
            AlterColumn("dbo.StockTypes", "StockTypeAssigned", c => c.String(nullable: false));
            CreateIndex("dbo.Stocks", "TypeId");
            AddForeignKey("dbo.Stocks", "TypeId", "dbo.StockTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "TypeId", "dbo.StockTypes");
            DropIndex("dbo.Stocks", new[] { "TypeId" });
            AlterColumn("dbo.StockTypes", "StockTypeAssigned", c => c.String());
            AlterColumn("dbo.StockMeasurements", "MeasurementType", c => c.String());
            AlterColumn("dbo.Stocks", "TypeId", c => c.Int());
            AlterColumn("dbo.Stocks", "Name", c => c.String());
            AlterColumn("dbo.DepartmentFaculties", "Name", c => c.String());
            CreateIndex("dbo.Stocks", "TypeId");
            AddForeignKey("dbo.Stocks", "TypeId", "dbo.StockTypes", "Id");
        }
    }
}
