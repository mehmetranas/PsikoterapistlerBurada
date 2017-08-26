namespace PsikoterapsitlerBurada.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNotificationAndUserNotificateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(),
                        AnswerId = c.Int(),
                        Type = c.Int(nullable: false),
                        Following_FollowerId = c.String(maxLength: 128),
                        Following_FolloweeId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId)
                .ForeignKey("dbo.Followings", t => new { t.Following_FollowerId, t.Following_FolloweeId })
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId)
                .Index(t => t.AnswerId)
                .Index(t => new { t.Following_FollowerId, t.Following_FolloweeId });
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        NotificationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.NotificationId })
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.NotificationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Notifications", new[] { "Following_FollowerId", "Following_FolloweeId" }, "dbo.Followings");
            DropForeignKey("dbo.Notifications", "AnswerId", "dbo.Answers");
            DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.Notifications", new[] { "Following_FollowerId", "Following_FolloweeId" });
            DropIndex("dbo.Notifications", new[] { "AnswerId" });
            DropIndex("dbo.Notifications", new[] { "QuestionId" });
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Notifications");
        }
    }
}
