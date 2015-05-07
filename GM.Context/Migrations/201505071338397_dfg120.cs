namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfg120 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "EntrepriseId", c => c.Int(nullable: false));
            CreateIndex("dbo.Stocks", "EntrepriseId");
            AddForeignKey("dbo.Stocks", "EntrepriseId", "dbo.Entreprises", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "EntrepriseId", "dbo.Entreprises");
            DropIndex("dbo.Stocks", new[] { "EntrepriseId" });
            DropColumn("dbo.Stocks", "EntrepriseId");
        }
    }
}
