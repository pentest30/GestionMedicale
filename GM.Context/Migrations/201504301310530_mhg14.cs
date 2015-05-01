namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mhg14 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Commandes", "NumPiece", c => c.String(nullable: false));
            CreateIndex("dbo.Commandes", "FournisseurId");
            AddForeignKey("dbo.Commandes", "FournisseurId", "dbo.Entreprises", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Commandes", "FournisseurId", "dbo.Entreprises");
            DropIndex("dbo.Commandes", new[] { "FournisseurId" });
            AlterColumn("dbo.Commandes", "NumPiece", c => c.String());
        }
    }
}
