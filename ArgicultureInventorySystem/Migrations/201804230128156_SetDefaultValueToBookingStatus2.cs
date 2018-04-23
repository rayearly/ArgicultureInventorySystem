namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetDefaultValueToBookingStatus2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "BookingStatus", c => c.String(nullable: false, defaultValue:"Not Approved"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bookings", "BookingStatus", c => c.String());
        }
    }
}
