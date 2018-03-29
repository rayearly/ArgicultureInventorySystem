namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateStockModel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Stocks", name: "Measurement_Id", newName: "MeasurementId");
            RenameColumn(table: "dbo.Stocks", name: "Type_Id", newName: "TypeId");
            RenameIndex(table: "dbo.Stocks", name: "IX_Type_Id", newName: "IX_TypeId");
            RenameIndex(table: "dbo.Stocks", name: "IX_Measurement_Id", newName: "IX_MeasurementId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Stocks", name: "IX_MeasurementId", newName: "IX_Measurement_Id");
            RenameIndex(table: "dbo.Stocks", name: "IX_TypeId", newName: "IX_Type_Id");
            RenameColumn(table: "dbo.Stocks", name: "TypeId", newName: "Type_Id");
            RenameColumn(table: "dbo.Stocks", name: "MeasurementId", newName: "Measurement_Id");
        }
    }
}
