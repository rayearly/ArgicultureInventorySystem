namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Idunno : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "FacultyDepartmentId", "dbo.FacultyDepartments");
            DropIndex("dbo.AspNetUsers", new[] { "FacultyDepartmentId" });
            DropColumn("dbo.AspNetUsers", "FacultyDepartmentId");
            DropTable("dbo.FacultyDepartments");
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
