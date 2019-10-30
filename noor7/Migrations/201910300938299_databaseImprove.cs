namespace noor7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseImprove : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Notebooks", "Grade", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Notebooks", "Grade", c => c.Int(nullable: false));
        }
    }
}
