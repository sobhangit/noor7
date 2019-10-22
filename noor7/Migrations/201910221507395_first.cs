namespace noor7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Absents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Problem = c.String(nullable: false),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(),
                        IsCertificate = c.Boolean(),
                        IsTrue = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        FatherName = c.String(),
                        InSchoolFrom = c.Int(nullable: false),
                        Class = c.String(nullable: false),
                        Code = c.Int(),
                        ClassListNumber = c.Int(nullable: false),
                        Exit = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                        FinalGrade = c.Int(nullable: false),
                        ExamDate = c.DateTime(nullable: false),
                        ExamType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Practices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        Numbers = c.Int(nullable: false),
                        PassedNumbers = c.Int(nullable: false),
                        PracticeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Defects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        DefaceDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Lates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        LateDate = c.DateTime(nullable: false),
                        LateTime = c.Int(),
                        Problem = c.String(nullable: false),
                        IsTrue = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Notebooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                        NoteBookDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Speaks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Problem = c.String(nullable: false),
                        Result = c.String(nullable: false),
                        SpeakDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Speaks", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Notebooks", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Lates", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Defects", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Courses", "StudentID", "dbo.Students");
            DropForeignKey("dbo.Practices", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Exams", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Absents", "StudentID", "dbo.Students");
            DropIndex("dbo.Speaks", new[] { "StudentID" });
            DropIndex("dbo.Notebooks", new[] { "StudentID" });
            DropIndex("dbo.Lates", new[] { "StudentID" });
            DropIndex("dbo.Defects", new[] { "StudentID" });
            DropIndex("dbo.Practices", new[] { "CourseID" });
            DropIndex("dbo.Exams", new[] { "CourseID" });
            DropIndex("dbo.Courses", new[] { "StudentID" });
            DropIndex("dbo.Absents", new[] { "StudentID" });
            DropTable("dbo.Speaks");
            DropTable("dbo.Notebooks");
            DropTable("dbo.Lates");
            DropTable("dbo.Defects");
            DropTable("dbo.Practices");
            DropTable("dbo.Exams");
            DropTable("dbo.Courses");
            DropTable("dbo.Students");
            DropTable("dbo.Absents");
        }
    }
}
