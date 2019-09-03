namespace ProductionScheduler.Migrations {
    using System.Data.Entity.Migrations;

    public partial class UpdateJobProperties : DbMigration {
        public override void Up() {
            AddColumn("dbo.Jobs", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Jobs", "JobNotes", c => c.String());
            AddColumn("dbo.Jobs", "IsComplete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Jobs", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jobs", "EndDate", c => c.DateTime(nullable: false));
        }

        public override void Down() {
            DropColumn("dbo.Jobs", "EndDate");
            DropColumn("dbo.Jobs", "StartDate");
            DropColumn("dbo.Jobs", "IsComplete");
            DropColumn("dbo.Jobs", "JobNotes");
            DropColumn("dbo.Jobs", "Quantity");
        }
    }
}
