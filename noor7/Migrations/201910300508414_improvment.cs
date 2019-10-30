namespace noor7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class improvment : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exams", "Grade", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exams", "Grade", c => c.Int(nullable: false));
        }
    }
}
