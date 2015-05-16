namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fg21 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stocks", "PrixUnitaire", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Stocks", "TauxBenefice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stocks", "TauxBenefice");
            DropColumn("dbo.Stocks", "PrixUnitaire");
        }
    }
}
