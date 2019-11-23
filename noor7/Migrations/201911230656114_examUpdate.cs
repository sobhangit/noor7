namespace noor7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class examUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Exams", "TeacherAdvice", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Exams", "TeacherAdvice", c => c.Int(nullable: false));
        }
    }
}
