namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOtherAttributesToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "IdNumber", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "PhoneNo", c => c.String(nullable: false));
            CreateIndex("dbo.Bookings", "ApplicationUser_Id");
            AddForeignKey("dbo.Bookings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Bookings", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "PhoneNo");
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.AspNetUsers", "IdNumber");
            DropColumn("dbo.Bookings", "ApplicationUser_Id");
        }
    }
}
