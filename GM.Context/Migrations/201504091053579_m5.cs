namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneEntrees", "PieceComptableId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.LigneEntrees", "PieceComptableId");
        }
    }
}
