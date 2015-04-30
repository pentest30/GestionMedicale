using System.Data.Entity.Migrations;

namespace GM.Context.Migrations
{
    public partial class mg11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Commandes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FournisseurId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        NumPiece = c.String(),
                        Date = c.DateTime(),
                        Tva = c.Decimal(precision: 18, scale: 2),
                        Ttc = c.Decimal(precision: 18, scale: 2),
                        Tht = c.Decimal(precision: 18, scale: 2),
                        Validation = c.Boolean(nullable: false),
                        Delivrance = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LigneCommandes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CommandeId = c.Int(nullable: false),
                        MedicamentId = c.Int(nullable: false),
                        Qnt = c.Int(nullable: false),
                        Commande_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Commandes", t => t.Commande_Id)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .Index(t => t.MedicamentId)
                .Index(t => t.Commande_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LigneCommandes", "MedicamentId", "dbo.Medicaments");
            DropForeignKey("dbo.LigneCommandes", "Commande_Id", "dbo.Commandes");
            DropIndex("dbo.LigneCommandes", new[] { "Commande_Id" });
            DropIndex("dbo.LigneCommandes", new[] { "MedicamentId" });
            DropTable("dbo.LigneCommandes");
            DropTable("dbo.Commandes");
        }
    }
}
