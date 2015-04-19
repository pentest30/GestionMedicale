namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up6 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Utilisateurs", "Fax");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Utilisateurs", "Fax", c => c.String());
        }
    }
}
