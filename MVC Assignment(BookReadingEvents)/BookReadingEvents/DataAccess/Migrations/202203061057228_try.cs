namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _try : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.BookReadingEventEntity2",
                c => new
                {
                    EventID = c.Int(nullable: false, identity: true),
                    HostEmailID = c.String(nullable: false, maxLength: 128),
                    Title = c.String(nullable: false),
                    Date = c.DateTime(nullable: false),
                    Location = c.String(nullable: false),
                    StartTime = c.Time(nullable: false, precision: 7),
                    Duration = c.Int(nullable: false),
                    Description = c.String(maxLength: 50),
                    OtherDetails = c.String(maxLength: 500),
                    Type = c.Int(nullable: false),
                    InvitedUsers = c.String(),
                })
                .PrimaryKey(t => t.EventID)
                .ForeignKey("dbo.UserEntity2", t => t.HostEmailID, cascadeDelete: true)
                .Index(t => t.HostEmailID);

            CreateTable(
                "dbo.CommentEntity2",
                c => new
                {
                    CommentId = c.Int(nullable: false, identity: true),
                    EventID = c.Int(nullable: false),
                    UserEmailID = c.String(nullable: false),
                    Description = c.String(nullable: false),
                })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.BookReadingEventEntity2", t => t.EventID, cascadeDelete: true)
                .Index(t => t.EventID);

            CreateTable(
                "dbo.UserEntity2",
                c => new
                {
                    EmailID = c.String(nullable: false, maxLength: 128),
                    Password = c.String(nullable: false),
                    FullName = c.String(nullable: false),
                })
                .PrimaryKey(t => t.EmailID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.BookReadingEventEntity2", "HostEmailID", "dbo.UserEntity2");
            DropForeignKey("dbo.CommentEntity2", "EventID", "dbo.BookReadingEventEntity2");
            DropIndex("dbo.CommentEntity2", new[] { "EventID" });
            DropIndex("dbo.BookReadingEventEntity2", new[] { "HostEmailID" });
            DropTable("dbo.UserEntity2");
            DropTable("dbo.CommentEntity2");
            DropTable("dbo.BookReadingEventEntity2");
        }
    }
}
