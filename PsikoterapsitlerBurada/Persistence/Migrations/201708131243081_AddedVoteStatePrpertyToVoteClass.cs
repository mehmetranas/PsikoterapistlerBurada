namespace PsikoterapsitlerBurada.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedVoteStatePrpertyToVoteClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Votes", "DateTime", c => c.DateTime(nullable: false, defaultValueSql:"GETDATE()"));
            AddColumn("dbo.Votes", "VoteState", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Votes", "VoteState");
            DropColumn("dbo.Votes", "DateTime");
        }
    }
}
