namespace noor7.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteAve : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotebookAves",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StudentID = c.Int(nullable: false),
                        Month = c.String(nullable: false),
                        Average = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.StudentID, cascadeDelete: true)
                .Index(t => t.StudentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NotebookAves", "StudentID", "dbo.Students");
            DropIndex("dbo.NotebookAves", new[] { "StudentID" });
            DropTable("dbo.NotebookAves");
        }
    }
}
