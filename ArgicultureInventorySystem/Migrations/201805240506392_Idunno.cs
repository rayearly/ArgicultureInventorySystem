namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Idunno : DbMigration
    {
        public override void Up()
        {
            
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FacultyDepartments",
                c => new
                    {
                        FacultyDepartmentId = c.Int(nullable: false, identity: true),
                        FacultyDepartmentName = c.String(),
                    })
                .PrimaryKey(t => t.FacultyDepartmentId);
            
            AddColumn("dbo.AspNetUsers", "FacultyDepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "FacultyDepartmentId");
            AddForeignKey("dbo.AspNetUsers", "FacultyDepartmentId", "dbo.FacultyDepartments", "FacultyDepartmentId", cascadeDelete: true);
        }
    }
}
