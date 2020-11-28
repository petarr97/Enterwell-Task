namespace EnterwellTask.Migrations.MyDB
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FinalMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Stavkas", "UkupnaCijena");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Stavkas", "UkupnaCijena", c => c.Single(nullable: false));
        }
    }
}
