namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gf : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs");
            DropIndex("dbo.Entreprises", new[] { "PropreitaireId" });
            AddColumn("dbo.Stocks", "PrixUnitaire", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stocks", "TauxBenefice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Entreprises", "PropreitaireId", c => c.Guid());
            CreateIndex("dbo.Entreprises", "PropreitaireId");
            AddForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs");
            DropIndex("dbo.Entreprises", new[] { "PropreitaireId" });
            AlterColumn("dbo.Entreprises", "PropreitaireId", c => c.Guid(nullable: false));
            DropColumn("dbo.Stocks", "TauxBenefice");
            DropColumn("dbo.Stocks", "PrixUnitaire");
            CreateIndex("dbo.Entreprises", "PropreitaireId");
            AddForeignKey("dbo.Entreprises", "PropreitaireId", "dbo.Utilisateurs", "Id", cascadeDelete: true);
        }
    }
}
