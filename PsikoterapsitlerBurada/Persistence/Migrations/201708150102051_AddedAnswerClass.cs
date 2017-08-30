namespace PsikoterapsitlerBurada.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnswerClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                        Question_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.Questions", t => t.Question_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Question_Id);
            
            CreateTable(
                "dbo.Likes",
                c => new
                    {
                        Answer_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Answer_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Answers", t => t.Answer_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Answer_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Likes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Likes", "Answer_Id", "dbo.Answers");
            DropForeignKey("dbo.Answers", "Question_Id", "dbo.Questions");
            DropForeignKey("dbo.Answers", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Likes", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Likes", new[] { "Answer_Id" });
            DropIndex("dbo.Answers", new[] { "Question_Id" });
            DropIndex("dbo.Answers", new[] { "User_Id" });
            DropTable("dbo.Likes");
            DropTable("dbo.Answers");
        }
    }
}
