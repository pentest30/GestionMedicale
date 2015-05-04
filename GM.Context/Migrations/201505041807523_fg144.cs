namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fg144 : DbMigration
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
                        NumPiece = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Tva = c.Decimal(precision: 18, scale: 2),
                        Ttc = c.Decimal(precision: 18, scale: 2),
                        Tht = c.Decimal(precision: 18, scale: 2),
                        MagasinId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entreprises", t => t.FournisseurId, cascadeDelete: true)
                .ForeignKey("dbo.Magasins", t => t.MagasinId, cascadeDelete: true)
                .Index(t => t.FournisseurId)
                .Index(t => t.MagasinId);
            
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
                        LogoUrl = c.String(),
                        Wilaya = c.String(),
                        Commune = c.String(),
                        Rue = c.String(),
                        CodePostale = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utilisateurs", t => t.PropreitaireId, cascadeDelete: true)
                .Index(t => t.PropreitaireId);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nom = c.String(),
                        Prenom = c.String(),
                        DateNaissance = c.DateTime(nullable: false),
                        Addresse = c.String(),
                        Wilaya = c.String(),
                        Email = c.String(),
                        Tel = c.String(),
                        PasswordHash = c.String(),
                        Pseudo = c.String(),
                        LienPhotoPersonnelle = c.String(),
                        LienPhotoDocument = c.String(),
                        Validation = c.Boolean(nullable: false),
                        DateInscription = c.DateTime(),
                        Sexe = c.String(),
                        EnrepriseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UtilisateurRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UtilisateurId = c.Guid(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.Utilisateurs", t => t.UtilisateurId, cascadeDelete: true)
                .Index(t => t.UtilisateurId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                        Public = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Magasins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        EntrepriseId = c.Int(nullable: false),
                        PointVenteId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entreprises", t => t.EntrepriseId, cascadeDelete: false)
                .ForeignKey("dbo.PointVentes", t => t.PointVenteId)
                .Index(t => t.EntrepriseId)
                .Index(t => t.PointVenteId);
            
            CreateTable(
                "dbo.PointVentes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntrepriseId = c.Int(nullable: false),
                        Nom = c.String(),
                        Tel = c.String(),
                        Wilaya = c.String(),
                        Addresse = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entreprises", t => t.EntrepriseId, cascadeDelete: true)
                .Index(t => t.EntrepriseId);
            
            CreateTable(
                "dbo.Commandes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FournisseurId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        NumPiece = c.String(nullable: false),
                        Date = c.DateTime(),
                        Tva = c.Decimal(precision: 18, scale: 2),
                        Ttc = c.Decimal(precision: 18, scale: 2),
                        Tht = c.Decimal(precision: 18, scale: 2),
                        Validation = c.Boolean(nullable: false),
                        Delivrance = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entreprises", t => t.FournisseurId, cascadeDelete: true)
                .Index(t => t.FournisseurId);
            
            CreateTable(
                "dbo.LigneCommandes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CommandeId = c.Long(nullable: false),
                        MedicamentId = c.Int(nullable: false),
                        Qnt = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .ForeignKey("dbo.Commandes", t => t.CommandeId, cascadeDelete: true)
                .Index(t => t.CommandeId)
                .Index(t => t.MedicamentId);
            
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
                        LaboratoireId = c.Int(),
                        Discription = c.String(),
                        Tva = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conditionnements", t => t.ConditionnementId, cascadeDelete: true)
                .ForeignKey("dbo.Dcis", t => t.DciId, cascadeDelete: true)
                .ForeignKey("dbo.Formes", t => t.FormeId, cascadeDelete: true)
                .ForeignKey("dbo.Laboratoires", t => t.LaboratoireId)
                .ForeignKey("dbo.Specialites", t => t.SpecialiteId, cascadeDelete: true)
                .Index(t => t.DciId)
                .Index(t => t.SpecialiteId)
                .Index(t => t.FormeId)
                .Index(t => t.ConditionnementId)
                .Index(t => t.LaboratoireId);
            
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
                        SpecialiteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Specialites", t => t.SpecialiteId, cascadeDelete: false)
                .Index(t => t.SpecialiteId);
            
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
                "dbo.Formes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
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
                "dbo.MedicamentPictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(nullable: false),
                        MedicamentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .Index(t => t.MedicamentId);
            
            CreateTable(
                "dbo.ParamStocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicamentId = c.Int(nullable: false),
                        EntrepriseId = c.Int(nullable: false),
                        PointVenteId = c.Int(),
                        QntMinimale = c.Int(nullable: false),
                        QntCritique = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entreprises", t => t.EntrepriseId, cascadeDelete: true)
                .ForeignKey("dbo.PointVentes", t => t.PointVenteId)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .Index(t => t.MedicamentId)
                .Index(t => t.EntrepriseId)
                .Index(t => t.PointVenteId);
            
            CreateTable(
                "dbo.Remboursements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        MedicamentId = c.Int(nullable: false),
                        TarifReference = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .Index(t => t.MedicamentId);
            
            CreateTable(
                "dbo.Entrees",
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
            
            CreateTable(
                "dbo.LigneEntreeMagasins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicamentId = c.Int(nullable: false),
                        NumLot = c.String(),
                        Qnt = c.Int(nullable: false),
                        BonEntreeId = c.Long(nullable: false),
                        DateFabrication = c.DateTime(),
                        DatePeremption = c.DateTime(),
                        TauxBenifice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BonEntrees", t => t.BonEntreeId, cascadeDelete: true)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .Index(t => t.MedicamentId)
                .Index(t => t.BonEntreeId);
            
            CreateTable(
                "dbo.LigneEntrees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicamentId = c.Int(nullable: false),
                        NumLot = c.String(),
                        Qnt = c.Int(nullable: false),
                        Montant = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrixVente = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrixAchat = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EntreeId = c.Long(nullable: false),
                        DateFabrication = c.DateTime(),
                        DatePeremption = c.DateTime(),
                        TauxBenifice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entrees", t => t.EntreeId, cascadeDelete: true)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .Index(t => t.MedicamentId)
                .Index(t => t.EntreeId);
            
            CreateTable(
                "dbo.country",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.Stocks", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.LigneEntrees", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.LigneEntrees", "EntreeId", "dbo.Entrees");
            DropForeignKey("dbo.LigneEntreeMagasins", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.LigneEntreeMagasins", "BonEntreeId", "dbo.BonEntrees");
            DropForeignKey("dbo.Entrees", "FournisseurId", "dbo.Entreprises");
            DropForeignKey("dbo.LigneCommandes", "CommandeId", "dbo.Commandes");
            DropForeignKey("dbo.LigneCommandes", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.Medicaments", "SpecialiteId", "dbo.Specialites");
            DropForeignKey("dbo.Remboursements", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.ParamStocks", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.ParamStocks", "PointVenteId", "dbo.PointVentes");
            DropForeignKey("dbo.ParamStocks", "EntrepriseId", "dbo.Entreprises");
            DropForeignKey("dbo.MedicamentPictures", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.Medicaments", "LaboratoireId", "dbo.Laboratoires");
            DropForeignKey("dbo.Medicaments", "FormeId", "dbo.Formes");
            DropForeignKey("dbo.Medicaments", "DciId", "dbo.Dcis");
            DropForeignKey("dbo.Dcis", "SpecialiteId", "dbo.Specialites");
            DropForeignKey("dbo.Medicaments", "ConditionnementId", "dbo.Conditionnements");
            DropForeignKey("dbo.Commandes", "FournisseurId", "dbo.Entreprises");
            DropForeignKey("dbo.BonEntrees", "MagasinId", "dbo.Magasins");
            DropForeignKey("dbo.Magasins", "PointVenteId", "dbo.PointVentes");
            DropForeignKey("dbo.PointVentes", "EntrepriseId", "dbo.Entreprises");
            DropForeignKey("dbo.Magasins", "EntrepriseId", "dbo.Entreprises");
            DropForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs");
            DropForeignKey("dbo.BonEntrees", "FournisseurId", "dbo.Entreprises");
            DropForeignKey("dbo.UtilisateurRoles", "UtilisateurId", "dbo.Utilisateurs");
            DropForeignKey("dbo.UtilisateurRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.Stocks", new[] { "MagasinId" });
            DropIndex("dbo.Stocks", new[] { "MedicamentId" });
            DropIndex("dbo.LigneEntrees", new[] { "EntreeId" });
            DropIndex("dbo.LigneEntrees", new[] { "MedicamentId" });
            DropIndex("dbo.LigneEntreeMagasins", new[] { "BonEntreeId" });
            DropIndex("dbo.LigneEntreeMagasins", new[] { "MedicamentId" });
            DropIndex("dbo.Entrees", new[] { "FournisseurId" });
            DropIndex("dbo.Remboursements", new[] { "MedicamentId" });
            DropIndex("dbo.ParamStocks", new[] { "PointVenteId" });
            DropIndex("dbo.ParamStocks", new[] { "EntrepriseId" });
            DropIndex("dbo.ParamStocks", new[] { "MedicamentId" });
            DropIndex("dbo.MedicamentPictures", new[] { "MedicamentId" });
            DropIndex("dbo.Dcis", new[] { "SpecialiteId" });
            DropIndex("dbo.Medicaments", new[] { "LaboratoireId" });
            DropIndex("dbo.Medicaments", new[] { "ConditionnementId" });
            DropIndex("dbo.Medicaments", new[] { "FormeId" });
            DropIndex("dbo.Medicaments", new[] { "SpecialiteId" });
            DropIndex("dbo.Medicaments", new[] { "DciId" });
            DropIndex("dbo.LigneCommandes", new[] { "MedicamentId" });
            DropIndex("dbo.LigneCommandes", new[] { "CommandeId" });
            DropIndex("dbo.Commandes", new[] { "FournisseurId" });
            DropIndex("dbo.PointVentes", new[] { "EntrepriseId" });
            DropIndex("dbo.Magasins", new[] { "PointVenteId" });
            DropIndex("dbo.Magasins", new[] { "EntrepriseId" });
            DropIndex("dbo.UtilisateurRoles", new[] { "RoleId" });
            DropIndex("dbo.UtilisateurRoles", new[] { "UtilisateurId" });
            DropIndex("dbo.Entreprises", new[] { "PropreitaireId" });
            DropIndex("dbo.BonEntrees", new[] { "MagasinId" });
            DropIndex("dbo.BonEntrees", new[] { "FournisseurId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.country");
            DropTable("dbo.LigneEntrees");
            DropTable("dbo.LigneEntreeMagasins");
            DropTable("dbo.Entrees");
            DropTable("dbo.Remboursements");
            DropTable("dbo.ParamStocks");
            DropTable("dbo.MedicamentPictures");
            DropTable("dbo.Laboratoires");
            DropTable("dbo.Formes");
            DropTable("dbo.Specialites");
            DropTable("dbo.Dcis");
            DropTable("dbo.Conditionnements");
            DropTable("dbo.Medicaments");
            DropTable("dbo.LigneCommandes");
            DropTable("dbo.Commandes");
            DropTable("dbo.PointVentes");
            DropTable("dbo.Magasins");
            DropTable("dbo.Roles");
            DropTable("dbo.UtilisateurRoles");
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.Entreprises");
            DropTable("dbo.BonEntrees");
        }
    }
}
