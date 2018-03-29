namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMeasurementTypes : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO StockMeasurements (MeasurementType) VALUES ('Kilograms')");
            Sql("INSERT INTO StockMeasurements (MeasurementType) VALUES ('Bags')");
            Sql("INSERT INTO StockMeasurements (MeasurementType) VALUES ('Bottles')");
            Sql("INSERT INTO StockMeasurements (MeasurementType) VALUES ('Barrels')");
            Sql("INSERT INTO StockMeasurements (MeasurementType) VALUES ('Pack')");
            Sql("INSERT INTO StockMeasurements (MeasurementType) VALUES ('Litre')");
            Sql("INSERT INTO StockMeasurements (MeasurementType) VALUES ('Box')");
            Sql("INSERT INTO StockMeasurements (MeasurementType) VALUES ('Barrels / Litre')");
        }
        
        public override void Down()
        {
        }
    }
}
