namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gh1447 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.BonEntrees", "NumPiece", c => c.String(nullable: false));
            AlterColumn("dbo.BonEntrees", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BonEntrees", "Date", c => c.DateTime());
            AlterColumn("dbo.BonEntrees", "NumPiece", c => c.String());
        }
    }
}
