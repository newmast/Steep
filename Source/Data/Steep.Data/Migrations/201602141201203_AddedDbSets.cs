namespace Steep.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDbSets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chapters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 30),
                        PreviousChapterId = c.Int(),
                        UserId = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        StoryId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chapters", t => t.PreviousChapterId)
                .ForeignKey("dbo.Stories", t => t.StoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.PreviousChapterId)
                .Index(t => t.StoryId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 25),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChapterId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Content = c.String(nullable: false, maxLength: 500),
                        CreatedOn = c.DateTime(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chapters", t => t.ChapterId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ChapterId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ReadChapters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ChapterId = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Chapters", t => t.ChapterId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.ChapterId)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReadChapters", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReadChapters", "ChapterId", "dbo.Chapters");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ChapterId", "dbo.Chapters");
            DropForeignKey("dbo.Chapters", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Chapters", "StoryId", "dbo.Stories");
            DropForeignKey("dbo.Stories", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Chapters", "PreviousChapterId", "dbo.Chapters");
            DropIndex("dbo.ReadChapters", new[] { "User_Id" });
            DropIndex("dbo.ReadChapters", new[] { "ChapterId" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "ChapterId" });
            DropIndex("dbo.Stories", new[] { "GenreId" });
            DropIndex("dbo.Chapters", new[] { "User_Id" });
            DropIndex("dbo.Chapters", new[] { "StoryId" });
            DropIndex("dbo.Chapters", new[] { "PreviousChapterId" });
            DropTable("dbo.ReadChapters");
            DropTable("dbo.Comments");
            DropTable("dbo.Genres");
            DropTable("dbo.Stories");
            DropTable("dbo.Chapters");
        }
    }
}
