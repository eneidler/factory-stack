namespace ProductionScheduler.Migrations {
    using System.Data.Entity.Migrations;

    public partial class InitialModel : DbMigration {
        public override void Up() {
            CreateTable(
                "dbo.Jobs",
                c => new {
                    Id = c.Int(nullable: false, identity: true),
                    Mold_Id = c.Int(),
                    Part_Id = c.Int(),
                    Press_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Molds", t => t.Mold_Id)
                .ForeignKey("dbo.Parts", t => t.Part_Id)
                .ForeignKey("dbo.Presses", t => t.Press_Id)
                .Index(t => t.Mold_Id)
                .Index(t => t.Part_Id)
                .Index(t => t.Press_Id);

            CreateTable(
                "dbo.Molds",
                c => new {
                    Id = c.Int(nullable: false, identity: true),
                    MoldNumber = c.String(),
                    NumberOfCavities = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Parts",
                c => new {
                    Id = c.Int(nullable: false, identity: true),
                    PartNumber = c.String(),
                    ProductFamily = c.String(),
                    ProductDescription = c.String(),
                    CureTimeInMinutes = c.Int(nullable: false),
                    ProductFamily_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductFamilies", t => t.ProductFamily_Id)
                .Index(t => t.ProductFamily_Id);

            CreateTable(
                "dbo.Presses",
                c => new {
                    Id = c.Int(nullable: false, identity: true),
                    PressNumber = c.String(),
                    PressCapacity = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ProductFamilies",
                c => new {
                    Id = c.Int(nullable: false, identity: true),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.PartMolds",
                c => new {
                    Part_Id = c.Int(nullable: false),
                    Mold_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Part_Id, t.Mold_Id })
                .ForeignKey("dbo.Parts", t => t.Part_Id, cascadeDelete: true)
                .ForeignKey("dbo.Molds", t => t.Mold_Id, cascadeDelete: true)
                .Index(t => t.Part_Id)
                .Index(t => t.Mold_Id);

            CreateTable(
                "dbo.PressMolds",
                c => new {
                    Press_Id = c.Int(nullable: false),
                    Mold_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.Press_Id, t.Mold_Id })
                .ForeignKey("dbo.Presses", t => t.Press_Id, cascadeDelete: true)
                .ForeignKey("dbo.Molds", t => t.Mold_Id, cascadeDelete: true)
                .Index(t => t.Press_Id)
                .Index(t => t.Mold_Id);

        }

        public override void Down() {
            DropForeignKey("dbo.Parts", "ProductFamily_Id", "dbo.ProductFamilies");
            DropForeignKey("dbo.Jobs", "Press_Id", "dbo.Presses");
            DropForeignKey("dbo.Jobs", "Part_Id", "dbo.Parts");
            DropForeignKey("dbo.Jobs", "Mold_Id", "dbo.Molds");
            DropForeignKey("dbo.PressMolds", "Mold_Id", "dbo.Molds");
            DropForeignKey("dbo.PressMolds", "Press_Id", "dbo.Presses");
            DropForeignKey("dbo.PartMolds", "Mold_Id", "dbo.Molds");
            DropForeignKey("dbo.PartMolds", "Part_Id", "dbo.Parts");
            DropIndex("dbo.PressMolds", new[] { "Mold_Id" });
            DropIndex("dbo.PressMolds", new[] { "Press_Id" });
            DropIndex("dbo.PartMolds", new[] { "Mold_Id" });
            DropIndex("dbo.PartMolds", new[] { "Part_Id" });
            DropIndex("dbo.Parts", new[] { "ProductFamily_Id" });
            DropIndex("dbo.Jobs", new[] { "Press_Id" });
            DropIndex("dbo.Jobs", new[] { "Part_Id" });
            DropIndex("dbo.Jobs", new[] { "Mold_Id" });
            DropTable("dbo.PressMolds");
            DropTable("dbo.PartMolds");
            DropTable("dbo.ProductFamilies");
            DropTable("dbo.Presses");
            DropTable("dbo.Parts");
            DropTable("dbo.Molds");
            DropTable("dbo.Jobs");
        }
    }
}
