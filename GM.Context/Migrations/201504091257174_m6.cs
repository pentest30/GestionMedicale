namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneEntrees", "BonEntreeId", c => c.Int(nullable: false));
            AddColumn("dbo.LigneEntrees", "BonEntree_Id", c => c.Long());
            CreateIndex("dbo.LigneEntrees", "BonEntree_Id");
            AddForeignKey("dbo.LigneEntrees", "BonEntree_Id", "dbo.BonEntrees", "Id");
            DropColumn("dbo.LigneEntrees", "PieceComptableId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneEntrees", "PieceComptableId", c => c.Int(nullable: false));
            DropForeignKey("dbo.LigneEntrees", "BonEntree_Id", "dbo.BonEntrees");
            DropIndex("dbo.LigneEntrees", new[] { "BonEntree_Id" });
            DropColumn("dbo.LigneEntrees", "BonEntree_Id");
            DropColumn("dbo.LigneEntrees", "BonEntreeId");
        }
    }
}
