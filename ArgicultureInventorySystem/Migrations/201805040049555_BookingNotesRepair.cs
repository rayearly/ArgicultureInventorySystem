namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookingNotesRepair : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "BookingNotes", c => c.String());
        }
        
        public override void Down()
        {
        }
    }
}
