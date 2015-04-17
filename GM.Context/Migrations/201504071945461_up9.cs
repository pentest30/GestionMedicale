namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Entreprises", "Rue", c => c.String());
            AddColumn("dbo.Utilisateurs", "EnrepriseId", c => c.Int());
            DropColumn("dbo.Utilisateurs", "LocalId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Utilisateurs", "LocalId", c => c.Int());
            DropColumn("dbo.Utilisateurs", "EnrepriseId");
            DropColumn("dbo.Entreprises", "Rue");
        }
    }
}
