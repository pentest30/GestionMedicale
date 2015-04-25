namespace GM.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mh154 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParamStocks", "PointVenteId", c => c.Int());
            CreateIndex("dbo.ParamStocks", "PointVenteId");
            AddForeignKey("dbo.ParamStocks", "PointVenteId", "dbo.PointVentes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParamStocks", "PointVenteId", "dbo.PointVentes");
            DropIndex("dbo.ParamStocks", new[] { "PointVenteId" });
            DropColumn("dbo.ParamStocks", "PointVenteId");
        }
    }
}
