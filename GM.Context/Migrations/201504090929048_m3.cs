namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Remboursements", "TarifReference", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Remboursements", "TarifRefference");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Remboursements", "TarifRefference", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Remboursements", "TarifReference");
        }
    }
}
