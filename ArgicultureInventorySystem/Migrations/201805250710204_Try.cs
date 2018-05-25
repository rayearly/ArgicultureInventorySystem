namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Try : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.DepartmentFaculties", "DepartmentFacultyId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DepartmentFaculties", "DepartmentFacultyId", c => c.Int(nullable: false));
        }
    }
}
