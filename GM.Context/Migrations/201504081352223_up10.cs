namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class up10 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conditionnements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Formes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Laboratoires",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        Pays = c.String(),
                        Logo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medicaments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomCommerciale = c.String(),
                        Code = c.String(),
                        NumEnregistrement = c.String(),
                        Dose = c.String(),
                        Remboursable = c.Boolean(nullable: false),
                        DciId = c.Int(nullable: false),
                        SpecialiteId = c.Int(nullable: false),
                        FormeId = c.Int(nullable: false),
                        ConditionnementId = c.Int(nullable: false),
                        LaboratoireId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conditionnements", t => t.ConditionnementId, cascadeDelete: true)
                .ForeignKey("dbo.Dcis", t => t.DciId, cascadeDelete: true)
                .ForeignKey("dbo.Formes", t => t.FormeId, cascadeDelete: true)
                .ForeignKey("dbo.Specialites", t => t.SpecialiteId, cascadeDelete: true)
                .Index(t => t.DciId)
                .Index(t => t.SpecialiteId)
                .Index(t => t.FormeId)
                .Index(t => t.ConditionnementId);
            
            CreateTable(
                "dbo.Remboursements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(),
                        MedicamaentId = c.Int(nullable: false),
                        TarifRefference = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicamentId = c.Int(nullable: false),
                        EntrepriseId = c.Int(nullable: false),
                        Qnt = c.Long(),
                        QntMin = c.Long(),
                        QntCritique = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Medicaments", "SpecialiteId", "dbo.Specialites");
            DropForeignKey("dbo.Medicaments", "FormeId", "dbo.Formes");
            DropForeignKey("dbo.Medicaments", "DciId", "dbo.Dcis");
            DropForeignKey("dbo.Medicaments", "ConditionnementId", "dbo.Conditionnements");
            DropIndex("dbo.Medicaments", new[] { "ConditionnementId" });
            DropIndex("dbo.Medicaments", new[] { "FormeId" });
            DropIndex("dbo.Medicaments", new[] { "SpecialiteId" });
            DropIndex("dbo.Medicaments", new[] { "DciId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.Remboursements");
            DropTable("dbo.Medicaments");
            DropTable("dbo.Laboratoires");
            DropTable("dbo.Formes");
            DropTable("dbo.Conditionnements");
        }
    }
}
