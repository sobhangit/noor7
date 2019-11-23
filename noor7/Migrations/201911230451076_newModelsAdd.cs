namespace noor7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newModelsAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Cycle = c.Int(nullable: false),
                        JobType = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            AddColumn("dbo.Exams", "TeacherAdvice", c => c.Int(nullable: false));
            AddColumn("dbo.Practices", "TeacherAdvice", c => c.Int(nullable: false));
            AddColumn("dbo.Speaks", "NextSpeak", c => c.DateTime());
            AlterColumn("dbo.Students", "Code", c => c.Long());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "StudentID", "dbo.Students");
            DropIndex("dbo.Jobs", new[] { "StudentID" });
            AlterColumn("dbo.Students", "Code", c => c.Int());
            DropColumn("dbo.Speaks", "NextSpeak");
            DropColumn("dbo.Practices", "TeacherAdvice");
            DropColumn("dbo.Exams", "TeacherAdvice");
            DropTable("dbo.Jobs");
        }
    }
}
