namespace GM.Context.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class lj25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Entreprises", "LogoUrl", c => c.String());
            AddColumn("dbo.Entreprises", "CodePostale", c => c.String());
            DropColumn("dbo.Entreprises", "Logo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Entreprises", "Logo", c => c.String());
            DropColumn("dbo.Entreprises", "CodePostale");
            DropColumn("dbo.Entreprises", "LogoUrl");
        }
    }
}
