namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Utilisateurs", "Sexe", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Utilisateurs", "Sexe");
        }
    }
}
