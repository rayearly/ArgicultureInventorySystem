namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDeptFacForeignKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DepartmentFacultyName", c => c.String());
            AddColumn("dbo.AspNetUsers", "DepartmentFacultyId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "DepartmentFacultyId");
            DropColumn("dbo.AspNetUsers", "DepartmentFacultyName");
        }
    }
}
