namespace EnterwellTask.Migrations.MyDB
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrationDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Fakturas",
                c => new
                    {
                        FakturaID = c.Int(nullable: false, identity: true),
                        DatumStvaranja = c.DateTime(nullable: false),
                        DatumDospijeca = c.DateTime(nullable: false),
                        UkupnaCijenaBezPoreza = c.Single(nullable: false),
                        UkupnaCijenaSaPorezom = c.Single(nullable: false),
                        UserID = c.String(),
                        PrimateljRacuna = c.String(),
                    })
                .PrimaryKey(t => t.FakturaID);
            
            CreateTable(
                "dbo.Stavkas",
                c => new
                    {
                        StavkaID = c.Int(nullable: false, identity: true),
                        Opis = c.String(),
                        Cijena = c.Single(nullable: false),
                        UkupnaCijena = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.StavkaID);
            
            CreateTable(
                "dbo.IdentityUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.IdentityRole_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropIndex("dbo.IdentityUserRoles", new[] { "IdentityRole_Id" });
            DropTable("dbo.IdentityUserRoles");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.IdentityUserLogins");
            DropTable("dbo.Stavkas");
            DropTable("dbo.Fakturas");
        }
    }
}
