namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyBookingAddBookingDateModelInIdentityModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingDates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingDateTime = c.DateTime(nullable: false),
                        ReturnDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BookingDates");
        }
    }
}
