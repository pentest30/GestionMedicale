namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gh14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LigneEntrees", "Montant", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.LigneEntrees", "PrixVente", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.LigneEntrees", "PrixAchat", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.LigneEntrees", "PrixUnitaire");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LigneEntrees", "PrixUnitaire", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.LigneEntrees", "PrixAchat");
            DropColumn("dbo.LigneEntrees", "PrixVente");
            DropColumn("dbo.LigneEntrees", "Montant");
        }
    }
}
