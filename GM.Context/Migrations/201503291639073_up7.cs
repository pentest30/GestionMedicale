namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilisateurs", "Wilaya", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilisateurs", "Wilaya");
        }
    }
}
