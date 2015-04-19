namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BonEntrees",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FournisseurId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        NumPiece = c.String(),
                        Date = c.DateTime(),
                        Tva = c.Decimal(precision: 18, scale: 2),
                        Ttc = c.Decimal(precision: 18, scale: 2),
                        Tht = c.Decimal(precision: 18, scale: 2),
                        MagasinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Magasins", t => t.MagasinId, cascadeDelete: true)
                .Index(t => t.MagasinId);
            
            CreateTable(
                "dbo.LigneEntrees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicamentId = c.Int(nullable: false),
                        NumLot = c.String(),
                        Qnt = c.Int(nullable: false),
                        PrixUnitaire = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateFabrication = c.DateTime(),
                        DatePeremption = c.DateTime(),
                        TauxBenifice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .Index(t => t.MedicamentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LigneEntrees", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.BonEntrees", "MagasinId", "dbo.Magasins");
            DropIndex("dbo.LigneEntrees", new[] { "MedicamentId" });
            DropIndex("dbo.BonEntrees", new[] { "MagasinId" });
            DropTable("dbo.LigneEntrees");
            DropTable("dbo.BonEntrees");
        }
    }
}
