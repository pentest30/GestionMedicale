namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bannes", "UtilisateurId", "dbo.Utilisateurs");
            DropIndex("dbo.Bannes", new[] { "UtilisateurId" });
            CreateTable(
                "dbo.Conditionnements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Dcis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Formes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Entreprises",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Nrc = c.String(),
                        Nif = c.String(),
                        Nis = c.String(),
                        Capital = c.Decimal(precision: 18, scale: 2),
                        DateFondation = c.DateTime(),
                        Tel = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        SiteWeb = c.String(),
                        PropreitaireId = c.Guid(nullable: false),
                        CompteBancaire = c.String(),
                        Logo = c.String(),
                        Wilaya = c.String(),
                        Commune = c.String(),
                        Rue = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utilisateurs", t => t.PropreitaireId, cascadeDelete: true)
                .Index(t => t.PropreitaireId);
            
            CreateTable(
                "dbo.Laboratoires",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        Pays = c.String(),
                        Logo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Magasins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        EntrepriseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entreprises", t => t.EntrepriseId, cascadeDelete: true)
                .Index(t => t.EntrepriseId);
            
            CreateTable(
                "dbo.Medicaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomCommerciale = c.String(),
                        Code = c.String(),
                        NumEnregistrement = c.String(),
                        Dose = c.String(),
                        Remboursable = c.Boolean(nullable: false),
                        DciId = c.Int(nullable: false),
                        SpecialiteId = c.Int(nullable: false),
                        FormeId = c.Int(nullable: false),
                        ConditionnementId = c.Int(nullable: false),
                        LaboratoireId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conditionnements", t => t.ConditionnementId, cascadeDelete: true)
                .ForeignKey("dbo.Dcis", t => t.DciId, cascadeDelete: true)
                .ForeignKey("dbo.Formes", t => t.FormeId, cascadeDelete: true)
                .ForeignKey("dbo.Laboratoires", t => t.LaboratoireId, cascadeDelete: true)
                .ForeignKey("dbo.Specialites", t => t.SpecialiteId, cascadeDelete: true)
                .Index(t => t.DciId)
                .Index(t => t.SpecialiteId)
                .Index(t => t.FormeId)
                .Index(t => t.ConditionnementId)
                .Index(t => t.LaboratoireId);
            
            CreateTable(
                "dbo.Specialites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(nullable: false),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParamStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicamentId = c.Int(nullable: false),
                        EntrepriseId = c.Int(nullable: false),
                        QntMinimale = c.Int(nullable: false),
                        QntCritique = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entreprises", t => t.EntrepriseId, cascadeDelete: true)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .Index(t => t.MedicamentId)
                .Index(t => t.EntrepriseId);
            
            CreateTable(
                "dbo.Remboursements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        MedicamentId = c.Int(nullable: false),
                        TarifRefference = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .Index(t => t.MedicamentId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicamentId = c.Int(nullable: false),
                        MagasinId = c.Int(nullable: false),
                        Qnt = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Magasins", t => t.MagasinId, cascadeDelete: true)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .Index(t => t.MedicamentId)
                .Index(t => t.MagasinId);
            
            DropTable("dbo.Bannes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Bannes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UtilisateurId = c.Guid(nullable: false),
                        Debut = c.DateTime(),
                        Fin = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Stocks", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.Stocks", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.Remboursements", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.ParamStocks", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.ParamStocks", "EntrepriseId", "dbo.Entreprises");
            DropForeignKey("dbo.Medicaments", "SpecialiteId", "dbo.Specialites");
            DropForeignKey("dbo.Medicaments", "LaboratoireId", "dbo.Laboratoires");
            DropForeignKey("dbo.Medicaments", "FormeId", "dbo.Formes");
            DropForeignKey("dbo.Medicaments", "DciId", "dbo.Dcis");
            DropForeignKey("dbo.Medicaments", "ConditionnementId", "dbo.Conditionnements");
            DropForeignKey("dbo.Magasins", "EntrepriseId", "dbo.Entreprises");
            DropForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs");
            DropIndex("dbo.Stocks", new[] { "MagasinId" });
            DropIndex("dbo.Stocks", new[] { "MedicamentId" });
            DropIndex("dbo.Remboursements", new[] { "MedicamentId" });
            DropIndex("dbo.ParamStocks", new[] { "EntrepriseId" });
            DropIndex("dbo.ParamStocks", new[] { "MedicamentId" });
            DropIndex("dbo.Medicaments", new[] { "LaboratoireId" });
            DropIndex("dbo.Medicaments", new[] { "ConditionnementId" });
            DropIndex("dbo.Medicaments", new[] { "FormeId" });
            DropIndex("dbo.Medicaments", new[] { "SpecialiteId" });
            DropIndex("dbo.Medicaments", new[] { "DciId" });
            DropIndex("dbo.Magasins", new[] { "EntrepriseId" });
            DropIndex("dbo.Entreprises", new[] { "PropreitaireId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.Remboursements");
            DropTable("dbo.ParamStocks");
            DropTable("dbo.Specialites");
            DropTable("dbo.Medicaments");
            DropTable("dbo.Magasins");
            DropTable("dbo.Laboratoires");
            DropTable("dbo.Entreprises");
            DropTable("dbo.Formes");
            DropTable("dbo.Dcis");
            DropTable("dbo.Conditionnements");
            CreateIndex("dbo.Bannes", "UtilisateurId");
            AddForeignKey("dbo.Bannes", "UtilisateurId", "dbo.Utilisateurs", "Id", cascadeDelete: true);
        }
    }
}
