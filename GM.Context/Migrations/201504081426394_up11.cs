namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up11 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Stocks", "MedicamentId");
            AddForeignKey("dbo.Stocks", "MedicamentId", "dbo.Medicaments", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "MedicamentId", "dbo.Medicaments");
            DropIndex("dbo.Stocks", new[] { "MedicamentId" });
        }
    }
}
