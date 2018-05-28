namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Testntah : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "DFId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "DFId");
            AddForeignKey("dbo.AspNetUsers", "DFId", "dbo.DepartmentFaculties", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "DFId", "dbo.DepartmentFaculties");
            DropIndex("dbo.AspNetUsers", new[] { "DFId" });
            DropColumn("dbo.AspNetUsers", "DFId");
        }
    }
}
