namespace PsikoterapsitlerBurada.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAnswerTextToAnswerClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "AnswerText", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "AnswerText");
        }
    }
}
