namespace ProductionScheduler.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixNullFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: true));
            AlterColumn("dbo.Users", "Password", c => c.String(nullable: true));
        }
    }
}
