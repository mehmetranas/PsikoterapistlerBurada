namespace PsikoterapsitlerBurada.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateQuestionAndCategoryTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionText = c.String(nullable: false),
                        DateTime = c.DateTime(),
                        Category_Id = c.Int(nullable: false),
                        WhoAsked_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.WhoAsked_Id)
                .Index(t => t.Category_Id)
                .Index(t => t.WhoAsked_Id);
            
            CreateTable(
                "dbo.QuestionsAskedWhom",
                c => new
                    {
                        Question_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Question_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.Questions", t => t.Question_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Question_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "WhoAsked_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Questions", "Category_Id", "dbo.Categories");
            DropForeignKey("dbo.QuestionsAskedWhom", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.QuestionsAskedWhom", "Question_Id", "dbo.Questions");
            DropIndex("dbo.QuestionsAskedWhom", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.QuestionsAskedWhom", new[] { "Question_Id" });
            DropIndex("dbo.Questions", new[] { "WhoAsked_Id" });
            DropIndex("dbo.Questions", new[] { "Category_Id" });
            DropTable("dbo.QuestionsAskedWhom");
            DropTable("dbo.Questions");
            DropTable("dbo.Categories");
        }
    }
}
