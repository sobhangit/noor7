namespace noor7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Absent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Problem = c.String(),
                        FromDate = c.DateTime(nullable: false),
                        ToDate = c.DateTime(nullable: false),
                        IsCertificate = c.Boolean(nullable: false),
                        IsTrue = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        FatherName = c.String(),
                        InSchoolFrom = c.Int(nullable: false),
                        Class = c.String(),
                        Code = c.Int(nullable: false),
                        ClassListNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        StudentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Exam",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                        FinalGrade = c.Int(nullable: false),
                        ExamDate = c.DateTime(nullable: false),
                        ExamType = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Practice",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        Numbers = c.Int(nullable: false),
                        PassedNumbers = c.Int(nullable: false),
                        PracticeDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Course", t => t.CourseID, cascadeDelete: true)
                .Index(t => t.CourseID);
            
            CreateTable(
                "dbo.Defect",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Description = c.String(),
                        DefaceDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Late",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        LateDate = c.DateTime(nullable: false),
                        LateTime = c.Int(nullable: false),
                        Problem = c.String(),
                        IsTrue = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Notebook",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Grade = c.Int(nullable: false),
                        NoteBookDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
            CreateTable(
                "dbo.Speak",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Problem = c.String(),
                        Result = c.String(),
                        SpeakDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Student", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Speak", "StudentID", "dbo.Student");
            DropForeignKey("dbo.Notebook", "StudentID", "dbo.Student");
            DropForeignKey("dbo.Late", "StudentID", "dbo.Student");
            DropForeignKey("dbo.Defect", "StudentID", "dbo.Student");
            DropForeignKey("dbo.Course", "StudentID", "dbo.Student");
            DropForeignKey("dbo.Practice", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Exam", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Absent", "StudentID", "dbo.Student");
            DropIndex("dbo.Speak", new[] { "StudentID" });
            DropIndex("dbo.Notebook", new[] { "StudentID" });
            DropIndex("dbo.Late", new[] { "StudentID" });
            DropIndex("dbo.Defect", new[] { "StudentID" });
            DropIndex("dbo.Practice", new[] { "CourseID" });
            DropIndex("dbo.Exam", new[] { "CourseID" });
            DropIndex("dbo.Course", new[] { "StudentID" });
            DropIndex("dbo.Absent", new[] { "StudentID" });
            DropTable("dbo.Speak");
            DropTable("dbo.Notebook");
            DropTable("dbo.Late");
            DropTable("dbo.Defect");
            DropTable("dbo.Practice");
            DropTable("dbo.Exam");
            DropTable("dbo.Course");
            DropTable("dbo.Student");
            DropTable("dbo.Absent");
        }
    }
}
