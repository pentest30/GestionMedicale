namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Medicaments", "Discription", c => c.String());
            AddColumn("dbo.Medicaments", "Tva", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Medicaments", "Tva");
            DropColumn("dbo.Medicaments", "Discription");
        }
    }
}
