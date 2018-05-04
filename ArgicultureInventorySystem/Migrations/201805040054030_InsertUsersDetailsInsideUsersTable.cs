namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InsertUsersDetailsInsideUsersTable : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE [dbo].[AspNetUsers] SET [Name] = 'Ray Adderley', [IdNumber] = '331041', [PhoneNo] = '0178624891' WHERE [Email] = 'guest@guest.com'");
            Sql("UPDATE [dbo].[AspNetUsers] SET [Name] = 'Brad Admin', [IdNumber] = '331041', [PhoneNo] = '0178624891' WHERE [Email] = 'admin@admin.com'");
            Sql("UPDATE [dbo].[AspNetUsers] SET [Name] = 'Mock User', [IdNumber] = '331041', [PhoneNo] = '0178624891' WHERE [Email] = 'raysocialemail@gmail.com'");
        }
        
        public override void Down()
        {
        }
    }
}
