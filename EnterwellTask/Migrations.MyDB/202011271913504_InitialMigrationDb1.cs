namespace EnterwellTask.Migrations.MyDB
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrationDb1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StavkeFaktures",
                c => new
                    {
                        FakturaID = c.Int(nullable: false),
                        StavkaID = c.Int(nullable: false),
                        Kolicina = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FakturaID, t.StavkaID })
                .ForeignKey("dbo.Fakturas", t => t.FakturaID, cascadeDelete: true)
                .ForeignKey("dbo.Stavkas", t => t.StavkaID, cascadeDelete: true)
                .Index(t => t.FakturaID)
                .Index(t => t.StavkaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StavkeFaktures", "StavkaID", "dbo.Stavkas");
            DropForeignKey("dbo.StavkeFaktures", "FakturaID", "dbo.Fakturas");
            DropIndex("dbo.StavkeFaktures", new[] { "StavkaID" });
            DropIndex("dbo.StavkeFaktures", new[] { "FakturaID" });
            DropTable("dbo.StavkeFaktures");
        }
    }
}
