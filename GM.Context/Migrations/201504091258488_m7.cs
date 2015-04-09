namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m7 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LigneEntrees", "BonEntree_Id", "dbo.BonEntrees");
            DropIndex("dbo.LigneEntrees", new[] { "BonEntree_Id" });
            DropColumn("dbo.LigneEntrees", "BonEntreeId");
            RenameColumn(table: "dbo.LigneEntrees", name: "BonEntree_Id", newName: "BonEntreeId");
            AlterColumn("dbo.LigneEntrees", "BonEntreeId", c => c.Long(nullable: false));
            AlterColumn("dbo.LigneEntrees", "BonEntreeId", c => c.Long(nullable: false));
            CreateIndex("dbo.LigneEntrees", "BonEntreeId");
            AddForeignKey("dbo.LigneEntrees", "BonEntreeId", "dbo.BonEntrees", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LigneEntrees", "BonEntreeId", "dbo.BonEntrees");
            DropIndex("dbo.LigneEntrees", new[] { "BonEntreeId" });
            AlterColumn("dbo.LigneEntrees", "BonEntreeId", c => c.Long());
            AlterColumn("dbo.LigneEntrees", "BonEntreeId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.LigneEntrees", name: "BonEntreeId", newName: "BonEntree_Id");
            AddColumn("dbo.LigneEntrees", "BonEntreeId", c => c.Int(nullable: false));
            CreateIndex("dbo.LigneEntrees", "BonEntree_Id");
            AddForeignKey("dbo.LigneEntrees", "BonEntree_Id", "dbo.BonEntrees", "Id");
        }
    }
}
