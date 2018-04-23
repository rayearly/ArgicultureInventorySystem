namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetDefaultValueToBookingStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bookings", "BookingStatus", c=> c.String(defaultValue: "Not Approved"));
        }
        
        public override void Down()
        {
        }
    }
}
