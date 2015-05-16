namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mh54 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BonSortieMagasins",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FournisseurId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        NumPiece = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        MagasinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entreprises", t => t.FournisseurId, cascadeDelete: true)
                .ForeignKey("dbo.Magasins", t => t.MagasinId, cascadeDelete: true)
                .Index(t => t.FournisseurId)
                .Index(t => t.MagasinId);
            
            CreateTable(
                "dbo.LigneSortieMagasins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicamentId = c.Int(nullable: false),
                        NumLot = c.String(),
                        Qnt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .Index(t => t.MedicamentId);
            
            CreateTable(
                "dbo.LigneSorties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicamentId = c.Int(nullable: false),
                        NumLot = c.String(),
                        Qnt = c.Int(nullable: false),
                        Montant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrixVente = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EntreeId = c.Long(nullable: false),
                        DateFabrication = c.DateTime(),
                        DatePeremption = c.DateTime(),
                        TauxBenifice = c.Decimal(precision: 18, scale: 2),
                        Sortie_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .ForeignKey("dbo.Sorties", t => t.Sortie_Id)
                .Index(t => t.MedicamentId)
                .Index(t => t.Sortie_Id);
            
            CreateTable(
                "dbo.Sorties",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FournisseurId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        NumPiece = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Tva = c.Decimal(precision: 18, scale: 2),
                        Ttc = c.Decimal(precision: 18, scale: 2),
                        Tht = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entreprises", t => t.FournisseurId, cascadeDelete: true)
                .Index(t => t.FournisseurId);
            
            AddColumn("dbo.LigneEntreeMagasins", "LigneSortieMagasin_Id", c => c.Int());
            CreateIndex("dbo.LigneEntreeMagasins", "LigneSortieMagasin_Id");
            AddForeignKey("dbo.LigneEntreeMagasins", "LigneSortieMagasin_Id", "dbo.LigneSortieMagasins", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LigneSorties", "Sortie_Id", "dbo.Sorties");
            DropForeignKey("dbo.Sorties", "FournisseurId", "dbo.Entreprises");
            DropForeignKey("dbo.LigneSorties", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.LigneSortieMagasins", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.LigneEntreeMagasins", "LigneSortieMagasin_Id", "dbo.LigneSortieMagasins");
            DropForeignKey("dbo.BonSortieMagasins", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.BonSortieMagasins", "FournisseurId", "dbo.Entreprises");
            DropIndex("dbo.Sorties", new[] { "FournisseurId" });
            DropIndex("dbo.LigneSorties", new[] { "Sortie_Id" });
            DropIndex("dbo.LigneSorties", new[] { "MedicamentId" });
            DropIndex("dbo.LigneSortieMagasins", new[] { "MedicamentId" });
            DropIndex("dbo.BonSortieMagasins", new[] { "MagasinId" });
            DropIndex("dbo.BonSortieMagasins", new[] { "FournisseurId" });
            DropIndex("dbo.LigneEntreeMagasins", new[] { "LigneSortieMagasin_Id" });
            DropColumn("dbo.LigneEntreeMagasins", "LigneSortieMagasin_Id");
            DropTable("dbo.Sorties");
            DropTable("dbo.LigneSorties");
            DropTable("dbo.LigneSortieMagasins");
            DropTable("dbo.BonSortieMagasins");
        }
    }
}
