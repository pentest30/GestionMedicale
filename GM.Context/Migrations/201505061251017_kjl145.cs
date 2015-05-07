namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjl145 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BonEntrees", "FactureId", c => c.Long(nullable: false));
            CreateIndex("dbo.BonEntrees", "FactureId");
            AddForeignKey("dbo.BonEntrees", "FactureId", "dbo.Entrees", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BonEntrees", "FactureId", "dbo.Entrees");
            DropIndex("dbo.BonEntrees", new[] { "FactureId" });
            DropColumn("dbo.BonEntrees", "FactureId");
        }
    }
}
