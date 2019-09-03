namespace ProductionScheduler.Migrations {
    using System.Data.Entity.Migrations;

    public partial class ProductFamilyCategory : DbMigration {
        public override void Up() {
            AddColumn("dbo.Parts", "ProductFamilyCategory", c => c.String());
            AddColumn("dbo.ProductFamilies", "ProductFamilyCategory", c => c.String());
            DropColumn("dbo.Parts", "ProductFamily");
        }

        public override void Down() {
            AddColumn("dbo.Parts", "ProductFamily", c => c.String());
            DropColumn("dbo.ProductFamilies", "ProductFamilyCategory");
            DropColumn("dbo.Parts", "ProductFamilyCategory");
        }
    }
}
