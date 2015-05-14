namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs");
            DropIndex("dbo.Entreprises", new[] { "PropreitaireId" });
            AlterColumn("dbo.Entreprises", "PropreitaireId", c => c.Guid());
            CreateIndex("dbo.Entreprises", "PropreitaireId");
            AddForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs");
            DropIndex("dbo.Entreprises", new[] { "PropreitaireId" });
            AlterColumn("dbo.Entreprises", "PropreitaireId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Entreprises", "PropreitaireId");
            AddForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs", "Id", cascadeDelete: true);
        }
    }
}
