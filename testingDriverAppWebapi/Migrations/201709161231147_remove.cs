namespace testingDriverAppWebapi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Horses", "Hand");
            DropColumn("dbo.Horses", "Another");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Horses", "Another", c => c.String());
            AddColumn("dbo.Horses", "Hand", c => c.String());
        }
    }
}
