namespace PsikoterapsitlerBurada.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedUserLikePropToNotificationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "UserLikeId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Notifications", "UserLikeId");
            AddForeignKey("dbo.Notifications", "UserLikeId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notifications", "UserLikeId", "dbo.AspNetUsers");
            DropIndex("dbo.Notifications", new[] { "UserLikeId" });
            DropColumn("dbo.Notifications", "UserLikeId");
        }
    }
}
