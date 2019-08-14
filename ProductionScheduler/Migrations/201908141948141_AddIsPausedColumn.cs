namespace ProductionScheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsPausedColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "IsPaused", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jobs", "IsPaused");
        }
    }
}
