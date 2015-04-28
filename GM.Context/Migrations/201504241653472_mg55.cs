namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg55 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PointVentes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntrepriseId = c.Int(nullable: false),
                        Nom = c.String(),
                        Tel = c.String(),
                        Wilaya = c.String(),
                        Addresse = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entreprises", t => t.EntrepriseId, cascadeDelete: true)
                .Index(t => t.EntrepriseId);
            
            AddColumn("dbo.Magasins", "PointVenteId", c => c.Int());
            CreateIndex("dbo.Magasins", "PointVenteId");
            AddForeignKey("dbo.Magasins", "PointVenteId", "dbo.PointVentes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Magasins", "PointVenteId", "dbo.PointVentes");
            DropForeignKey("dbo.PointVentes", "EntrepriseId", "dbo.Entreprises");
            DropIndex("dbo.PointVentes", new[] { "EntrepriseId" });
            DropIndex("dbo.Magasins", new[] { "PointVenteId" });
            DropColumn("dbo.Magasins", "PointVenteId");
            DropTable("dbo.PointVentes");
        }
    }
}
