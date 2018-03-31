namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBookingAddPK : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Bookings");
            AddPrimaryKey("dbo.Bookings", new[] { "UniversityCommunityId", "StockId", "BookingDateId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Bookings");
            AddPrimaryKey("dbo.Bookings", new[] { "UniversityCommunityId", "StockId" });
        }
    }
}
