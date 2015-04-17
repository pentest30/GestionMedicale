namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs");
            DropForeignKey("dbo.Magasins", "EntrepriseId", "dbo.Entreprises");
            DropForeignKey("dbo.Medicaments", "ConditionnementId", "dbo.Conditionnements");
            DropForeignKey("dbo.Medicaments", "DciId", "dbo.Dcis");
            DropForeignKey("dbo.Medicaments", "FormeId", "dbo.Formes");
            DropForeignKey("dbo.Medicaments", "LaboratoireId", "dbo.Laboratoires");
            DropForeignKey("dbo.Medicaments", "SpecialiteId", "dbo.Specialites");
            DropForeignKey("dbo.ParamStocks", "EntrepriseId", "dbo.Entreprises");
            DropForeignKey("dbo.ParamStocks", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.Remboursements", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.Stocks", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.Stocks", "MedicamentId", "dbo.Medicaments");
            DropIndex("dbo.Entreprises", new[] { "PropreitaireId" });
            DropIndex("dbo.Magasins", new[] { "EntrepriseId" });
            DropIndex("dbo.Medicaments", new[] { "DciId" });
            DropIndex("dbo.Medicaments", new[] { "SpecialiteId" });
            DropIndex("dbo.Medicaments", new[] { "FormeId" });
            DropIndex("dbo.Medicaments", new[] { "ConditionnementId" });
            DropIndex("dbo.Medicaments", new[] { "LaboratoireId" });
            DropIndex("dbo.ParamStocks", new[] { "MedicamentId" });
            DropIndex("dbo.ParamStocks", new[] { "EntrepriseId" });
            DropIndex("dbo.Remboursements", new[] { "MedicamentId" });
            DropIndex("dbo.Stocks", new[] { "MedicamentId" });
            DropIndex("dbo.Stocks", new[] { "MagasinId" });
            CreateTable(
                "dbo.Bannes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UtilisateurId = c.Guid(nullable: false),
                        Debut = c.DateTime(),
                        Fin = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utilisateurs", t => t.UtilisateurId, cascadeDelete: true)
                .Index(t => t.UtilisateurId);
            
            DropTable("dbo.Conditionnements");
            DropTable("dbo.Dcis");
            DropTable("dbo.Formes");
            DropTable("dbo.Entreprises");
            DropTable("dbo.Laboratoires");
            DropTable("dbo.Magasins");
            DropTable("dbo.Medicaments");
            DropTable("dbo.Specialites");
            DropTable("dbo.ParamStocks");
            DropTable("dbo.Remboursements");
            DropTable("dbo.Stocks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicamentId = c.Int(nullable: false),
                        MagasinId = c.Int(nullable: false),
                        Qnt = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Remboursements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        MedicamentId = c.Int(nullable: false),
                        TarifRefference = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParamStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicamentId = c.Int(nullable: false),
                        EntrepriseId = c.Int(nullable: false),
                        QntMinimale = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QntCritique = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Magasins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        EntrepriseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Dcis",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Conditionnements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Bannes", "UtilisateurId", "dbo.Utilisateurs");
            DropIndex("dbo.Bannes", new[] { "UtilisateurId" });
            DropTable("dbo.Bannes");
            CreateIndex("dbo.Stocks", "MagasinId");
            CreateIndex("dbo.Stocks", "MedicamentId");
            CreateIndex("dbo.Remboursements", "MedicamentId");
            CreateIndex("dbo.ParamStocks", "EntrepriseId");
            CreateIndex("dbo.ParamStocks", "MedicamentId");
            CreateIndex("dbo.Medicaments", "LaboratoireId");
            CreateIndex("dbo.Medicaments", "ConditionnementId");
            CreateIndex("dbo.Medicaments", "FormeId");
            CreateIndex("dbo.Medicaments", "SpecialiteId");
            CreateIndex("dbo.Medicaments", "DciId");
            CreateIndex("dbo.Magasins", "EntrepriseId");
            CreateIndex("dbo.Entreprises", "PropreitaireId");
            AddForeignKey("dbo.Stocks", "MedicamentId", "dbo.Medicaments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Stocks", "MagasinId", "dbo.Magasins", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Remboursements", "MedicamentId", "dbo.Medicaments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ParamStocks", "MedicamentId", "dbo.Medicaments", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ParamStocks", "EntrepriseId", "dbo.Entreprises", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Medicaments", "SpecialiteId", "dbo.Specialites", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Medicaments", "LaboratoireId", "dbo.Laboratoires", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Medicaments", "FormeId", "dbo.Formes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Medicaments", "DciId", "dbo.Dcis", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Medicaments", "ConditionnementId", "dbo.Conditionnements", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Magasins", "EntrepriseId", "dbo.Entreprises", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs", "Id", cascadeDelete: true);
        }
    }
}
