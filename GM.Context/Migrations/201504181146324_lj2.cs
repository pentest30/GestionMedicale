namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lj2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MedicamentPictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(nullable: false),
                        MedicamentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Medicaments", t => t.MedicamentId, cascadeDelete: true)
                .Index(t => t.MedicamentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicamentPictures", "MedicamentId", "dbo.Medicaments");
            DropIndex("dbo.MedicamentPictures", new[] { "MedicamentId" });
            DropTable("dbo.MedicamentPictures");
        }
    }
}
