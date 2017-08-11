namespace PsikoterapsitlerBurada.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsAskedToQuestionClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Questions", "IsAsked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Questions", "IsAsked");
        }
    }
}
