namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up1 : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Utilisateurs", t => t.UtilisateurId, cascadeDelete: true)
                .Index(t => t.UtilisateurId);
            
            CreateTable(
                "dbo.Utilisateurs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nom = c.String(),
                        Prenom = c.String(),
                        DateNaissance = c.DateTime(nullable: false),
                        Addresse = c.String(),
                        Email = c.String(),
                        Tel = c.String(),
                        Fax = c.String(),
                        PasswordHash = c.String(),
                        Pseudo = c.String(),
                        LienPhotoPersonnelle = c.String(),
                        LienPhotoDocument = c.String(),
                        Validation = c.Boolean(nullable: false),
                        DateInscription = c.DateTime(),
                        LocalId = c.Int(),
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
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bannes", "UtilisateurId", "dbo.Utilisateurs");
            DropForeignKey("dbo.UtilisateurRoles", "UtilisateurId", "dbo.Utilisateurs");
            DropForeignKey("dbo.UtilisateurRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.UtilisateurRoles", new[] { "RoleId" });
            DropIndex("dbo.UtilisateurRoles", new[] { "UtilisateurId" });
            DropIndex("dbo.Bannes", new[] { "UtilisateurId" });
            DropTable("dbo.Roles");
            DropTable("dbo.UtilisateurRoles");
            DropTable("dbo.Utilisateurs");
            DropTable("dbo.Bannes");
        }
    }
}
