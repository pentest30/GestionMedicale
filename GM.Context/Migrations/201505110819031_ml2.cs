namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ml2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Cabinets", "MedecinId", "dbo.Utilisateurs");
            DropIndex("dbo.Cabinets", new[] { "MedecinId" });
            DropTable("dbo.Cabinets");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Cabinets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(nullable: false),
                        Wilaya = c.String(nullable: false),
                        Commune = c.String(nullable: false),
                        Email = c.String(),
                        Addresse = c.String(nullable: false),
                        Tel = c.String(),
                        MedecinId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Cabinets", "MedecinId");
            AddForeignKey("dbo.Cabinets", "MedecinId", "dbo.Utilisateurs", "Id", cascadeDelete: true);
        }
    }
}
