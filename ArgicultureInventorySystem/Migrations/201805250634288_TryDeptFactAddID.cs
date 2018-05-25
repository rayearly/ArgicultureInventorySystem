namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TryDeptFactAddID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DepartmentFaculties", "DepartmentFacultyId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DepartmentFaculties", "DepartmentFacultyId");
        }
    }
}
