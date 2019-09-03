namespace ProductionScheduler.Migrations {
    using System.Data.Entity.Migrations;

    public partial class AddJobNumberColumn : DbMigration {
        public override void Up() {
            AddColumn("dbo.Jobs", "JobNumber", c => c.Int(nullable: false));
        }

        public override void Down() {
            DropColumn("dbo.Jobs", "JobNumber");
        }
    }
}
