namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSomeStockSample : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Stocks (Name, OriginalQuantity, CurrentQuantity, Note, MeasurementId, TypeId) VALUES ('ALRABOL @ BRT SPRY (WHITE OIL 4L)' , '75', '71' , '', '6' , '2')");
            Sql("INSERT INTO Stocks (Name, OriginalQuantity, CurrentQuantity, Note, MeasurementId, TypeId) VALUES ('AVMEC @ AGRIMECTIN ABAMECTIN 1L' , '11', '6' , '', '6' , '2')");
            Sql("INSERT INTO Stocks (Name, OriginalQuantity, CurrentQuantity, Note, MeasurementId, TypeId) VALUES ('BENAKIL-FENOBUCARB' , '1', '1' , '', '6' , '2')");
            Sql("INSERT INTO Stocks (Name, OriginalQuantity, CurrentQuantity, Note, MeasurementId, TypeId) VALUES ('BAJA CIRP 50KG / BEG' , '17', '16' , '', '2' , '3')");
            Sql("INSERT INTO Stocks (Name, OriginalQuantity, CurrentQuantity, Note, MeasurementId, TypeId) VALUES ('BAJA MOP 50KG / BEG' , '5', '5' , '', '2' , '3')");
            Sql("INSERT INTO Stocks (Name, OriginalQuantity, CurrentQuantity, Note, MeasurementId, TypeId) VALUES ('BAJA NPK BLUE 50KG / BEG' , '7', '0' , '', '2' , '3')");
            Sql("INSERT INTO Stocks (Name, OriginalQuantity, CurrentQuantity, Note, MeasurementId, TypeId) VALUES ('CANGKUL' , '450', '308' , '', null, '1')");
            Sql("INSERT INTO Stocks (Name, OriginalQuantity, CurrentQuantity, Note, MeasurementId, TypeId) VALUES ('CHOP BESI' , '75', '64' , '', null, '1')");
            Sql("INSERT INTO Stocks (Name, OriginalQuantity, CurrentQuantity, Note, MeasurementId, TypeId) VALUES ('SABIT' , '23', '20' , '', null, '1')");
        }

        public override void Down()
        {
        }
    }
}
