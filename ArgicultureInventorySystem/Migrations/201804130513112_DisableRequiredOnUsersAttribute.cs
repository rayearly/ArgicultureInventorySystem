namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisableRequiredOnUsersAttribute : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "IdNumber", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String());
            AlterColumn("dbo.AspNetUsers", "PhoneNo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneNo", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "IdNumber", c => c.String(nullable: false));
        }
    }
}
