namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBookingStatusToStringForInProcessDefault : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "BookingStatus", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "BookingStatus", c => c.Boolean(nullable: false));
        }
    }
}
