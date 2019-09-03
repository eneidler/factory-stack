namespace ProductionScheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIssueFlaggedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "IssueFlagged", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "IssueFlagged");
        }
    }
}
