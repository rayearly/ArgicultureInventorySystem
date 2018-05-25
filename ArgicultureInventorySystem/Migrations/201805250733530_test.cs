namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "DepartmentFacultyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DepartmentFacultyId", c => c.Int(nullable: false));
        }
    }
}
