namespace PsikoterapsitlerBurada.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedVoteClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.QuestionId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votes", "QuestionId", "dbo.Questions");
            DropIndex("dbo.Votes", new[] { "UserId" });
            DropIndex("dbo.Votes", new[] { "QuestionId" });
            DropTable("dbo.Votes");
        }
    }
}
