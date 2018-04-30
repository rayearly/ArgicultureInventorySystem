namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookingIdForIdentification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "BookingId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "BookingId");
        }
    }
}
