namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Medicaments", "LaboratoireId", "dbo.Laboratoires");
            DropIndex("dbo.Medicaments", new[] { "LaboratoireId" });
            AlterColumn("dbo.Medicaments", "LaboratoireId", c => c.Int());
            CreateIndex("dbo.Medicaments", "LaboratoireId");
            AddForeignKey("dbo.Medicaments", "LaboratoireId", "dbo.Laboratoires", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medicaments", "LaboratoireId", "dbo.Laboratoires");
            DropIndex("dbo.Medicaments", new[] { "LaboratoireId" });
            AlterColumn("dbo.Medicaments", "LaboratoireId", c => c.Int(nullable: false));
            CreateIndex("dbo.Medicaments", "LaboratoireId");
            AddForeignKey("dbo.Medicaments", "LaboratoireId", "dbo.Laboratoires", "Id", cascadeDelete: true);
        }
    }
}
