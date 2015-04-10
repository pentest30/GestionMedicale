namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dcis", "SpecialiteId", c => c.Int(nullable: false));
            CreateIndex("dbo.Dcis", "SpecialiteId");
            AddForeignKey("dbo.Dcis", "SpecialiteId", "dbo.Specialites", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Dcis", "SpecialiteId", "dbo.Specialites");
            DropIndex("dbo.Dcis", new[] { "SpecialiteId" });
            DropColumn("dbo.Dcis", "SpecialiteId");
        }
    }
}
