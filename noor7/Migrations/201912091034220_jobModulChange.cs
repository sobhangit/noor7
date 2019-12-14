namespace noor7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jobModulChange : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Jobs", "Cycle", c => c.String(nullable: false));
            AlterColumn("dbo.Jobs", "JobType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Jobs", "JobType", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "Cycle", c => c.Int(nullable: false));
        }
    }
}
