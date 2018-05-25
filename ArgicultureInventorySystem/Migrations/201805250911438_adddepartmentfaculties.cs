namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddepartmentfaculties : DbMigration
    {
        public override void Up()
        {
            Sql("DBCC CHECKIDENT ('DepartmentFaculties', RESEED, 1)");
            Sql(@"INSERT INTO [dbo].[DepartmentFaculties] ([Name]) VALUES (N'FACULTY OF APPLIED SCIENCE')
            INSERT INTO[dbo].[DepartmentFaculties] ([Name]) VALUES('FACULTY OF ACCOUNTANCY')
            INSERT INTO[dbo].[DepartmentFaculties] ([Name]) VALUES('FACULTY OF BUSINESS MANAGEMENT')
            INSERT INTO[dbo].[DepartmentFaculties] ([Name]) VALUES('FACULTY OF ARCHITECTURE, PLANNING AND SURVEYING')
            INSERT INTO[dbo].[DepartmentFaculties] ([Name]) VALUES('FACULTY OF SPORTS SCIENCE AND RECREATION')
            INSERT INTO[dbo].[DepartmentFaculties] ([Name]) VALUES('FACULTY OF COMPUTER AND MATHEMATICAL SCIENCES ')
            INSERT INTO[dbo].[DepartmentFaculties] ([Name]) VALUES('FACULTY OF PLANTATION AND ARGOTECHNOLOGY')");
        }
        
        public override void Down()
        {
        }
    }
}
