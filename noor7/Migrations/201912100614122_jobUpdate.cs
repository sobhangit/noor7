namespace noor7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "Cycle", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "Cycle", c => c.String(nullable: false));
        }
    }
}
