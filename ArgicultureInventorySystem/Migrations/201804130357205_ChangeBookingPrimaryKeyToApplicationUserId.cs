namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBookingPrimaryKeyToApplicationUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "UniversityCommunityId", "dbo.UniversityCommunities");
            DropForeignKey("dbo.Bookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Bookings", new[] { "UniversityCommunityId" });
            DropIndex("dbo.Bookings", new[] { "ApplicationUser_Id" });
            RenameColumn(table: "dbo.Bookings", name: "UniversityCommunityId", newName: "UniversityCommunity_Id");
            RenameColumn(table: "dbo.Bookings", name: "ApplicationUser_Id", newName: "UserId");
            DropPrimaryKey("dbo.Bookings");
            AlterColumn("dbo.Bookings", "UniversityCommunity_Id", c => c.Int());
            AlterColumn("dbo.Bookings", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Bookings", new[] { "UserId", "StockId", "BookingDateId" });
            CreateIndex("dbo.Bookings", "UserId");
            CreateIndex("dbo.Bookings", "UniversityCommunity_Id");
            AddForeignKey("dbo.Bookings", "UniversityCommunity_Id", "dbo.UniversityCommunities", "Id");
            AddForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Bookings", "UniversityCommunity_Id", "dbo.UniversityCommunities");
            DropIndex("dbo.Bookings", new[] { "UniversityCommunity_Id" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropPrimaryKey("dbo.Bookings");
            AlterColumn("dbo.Bookings", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Bookings", "UniversityCommunity_Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Bookings", new[] { "UniversityCommunityId", "StockId", "BookingDateId" });
            RenameColumn(table: "dbo.Bookings", name: "UserId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Bookings", name: "UniversityCommunity_Id", newName: "UniversityCommunityId");
            CreateIndex("dbo.Bookings", "ApplicationUser_Id");
            CreateIndex("dbo.Bookings", "UniversityCommunityId");
            AddForeignKey("dbo.Bookings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Bookings", "UniversityCommunityId", "dbo.UniversityCommunities", "Id", cascadeDelete: true);
        }
    }
}
