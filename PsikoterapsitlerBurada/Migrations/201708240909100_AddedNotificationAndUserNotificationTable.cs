namespace PsikoterapsitlerBurada.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNotificationAndUserNotificationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NotificationType = c.Int(nullable: false),
                        QuestionId = c.Int(),
                        AnswerId = c.Int(),
                        FollowingId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answers", t => t.AnswerId)
                .ForeignKey("dbo.Followings", t => t.FollowingId)
                .ForeignKey("dbo.Questions", t => t.QuestionId)
                .Index(t => t.QuestionId)
                .Index(t => t.AnswerId)
                .Index(t => t.FollowingId);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        NotificationId = c.Int(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.NotificationId, t.UserId })
                .ForeignKey("dbo.Notifications", t => t.NotificationId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.NotificationId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserNotifications", "NotificationId", "dbo.Notifications");
            DropForeignKey("dbo.Notifications", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Notifications", "FollowingId", "dbo.Followings");
            DropForeignKey("dbo.Notifications", "AnswerId", "dbo.Answers");
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.UserNotifications", new[] { "NotificationId" });
            DropIndex("dbo.Notifications", new[] { "FollowingId" });
            DropIndex("dbo.Notifications", new[] { "AnswerId" });
            DropIndex("dbo.Notifications", new[] { "QuestionId" });
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Notifications");
        }
    }
}
