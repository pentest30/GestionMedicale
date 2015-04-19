namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bannes", "UtilisateurId", "dbo.Utilisateurs");
            DropIndex("dbo.Bannes", new[] { "UtilisateurId" });
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
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utilisateurs", t => t.PropreitaireId, cascadeDelete: true)
                .Index(t => t.PropreitaireId);
            
            CreateTable(
                "dbo.Specialites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(nullable: false),
                        Code = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            
            DropForeignKey("dbo.Magasins", "EntrepriseId", "dbo.Entreprises");
            DropForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs");
            DropIndex("dbo.Entreprises", new[] { "PropreitaireId" });
            DropIndex("dbo.Magasins", new[] { "EntrepriseId" });
            DropTable("dbo.Specialites");
            DropTable("dbo.Entreprises");
            DropTable("dbo.Magasins");
            CreateIndex("dbo.Bannes", "UtilisateurId");
            AddForeignKey("dbo.Bannes", "UtilisateurId", "dbo.Utilisateurs", "Id", cascadeDelete: true);
        }
    }
}
