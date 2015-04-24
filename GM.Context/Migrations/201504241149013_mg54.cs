namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg54 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.country",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.country");
        }
    }
}
