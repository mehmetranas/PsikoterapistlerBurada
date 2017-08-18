namespace PsikoterapsitlerBurada.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedFavoriteQuestionToQuestionClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteQuestions",
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
            DropForeignKey("dbo.FavoriteQuestions", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FavoriteQuestions", "Question_Id", "dbo.Questions");
            DropIndex("dbo.FavoriteQuestions", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.FavoriteQuestions", new[] { "Question_Id" });
            DropTable("dbo.FavoriteQuestions");
        }
    }
}
