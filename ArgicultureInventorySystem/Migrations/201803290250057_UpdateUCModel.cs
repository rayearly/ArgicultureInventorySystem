namespace ArgicultureInventorySystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUCModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UniversityCommunities", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UniversityCommunities", "Name");
        }
    }
}
