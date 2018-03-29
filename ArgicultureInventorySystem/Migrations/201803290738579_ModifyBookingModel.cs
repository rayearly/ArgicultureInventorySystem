namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyBookingModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "BookingDateId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bookings", "BookingDateId");
            AddForeignKey("dbo.Bookings", "BookingDateId", "dbo.BookingDates", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "BookingDateId", "dbo.BookingDates");
            DropIndex("dbo.Bookings", new[] { "BookingDateId" });
            DropColumn("dbo.Bookings", "BookingDateId");
        }
    }
}
