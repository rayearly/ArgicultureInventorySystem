namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookingStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "BookingStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bookings", "BookingStatus");
        }
    }
}
