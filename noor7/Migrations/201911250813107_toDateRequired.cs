namespace noor7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class toDateRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Absents", "ToDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Absents", "ToDate", c => c.DateTime());
        }
    }
}
