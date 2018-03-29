namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyBookingAddBookingDateModel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bookings", "BookingDateTime");
            DropColumn("dbo.Bookings", "ReturnDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "ReturnDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Bookings", "BookingDateTime", c => c.DateTime(nullable: false));
        }
    }
}
