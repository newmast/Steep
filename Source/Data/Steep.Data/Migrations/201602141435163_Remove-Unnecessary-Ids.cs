namespace Steep.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUnnecessaryIds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Chapters", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Chapters", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Chapters", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Chapters", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Stories", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Stories", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Stories", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Stories", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Genres", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Genres", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Genres", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Genres", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.Comments", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.Comments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Comments", "DeletedOn", c => c.DateTime());
            AddColumn("dbo.ReadChapters", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ReadChapters", "ModifiedOn", c => c.DateTime());
            AddColumn("dbo.ReadChapters", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.ReadChapters", "DeletedOn", c => c.DateTime());
            CreateIndex("dbo.Chapters", "IsDeleted");
            CreateIndex("dbo.Stories", "IsDeleted");
            CreateIndex("dbo.Genres", "IsDeleted");
            CreateIndex("dbo.Comments", "IsDeleted");
            CreateIndex("dbo.ReadChapters", "IsDeleted");
        }
        
        public override void Down()
        {
            DropIndex("dbo.ReadChapters", new[] { "IsDeleted" });
            DropIndex("dbo.Comments", new[] { "IsDeleted" });
            DropIndex("dbo.Genres", new[] { "IsDeleted" });
            DropIndex("dbo.Stories", new[] { "IsDeleted" });
            DropIndex("dbo.Chapters", new[] { "IsDeleted" });
            DropColumn("dbo.ReadChapters", "DeletedOn");
            DropColumn("dbo.ReadChapters", "IsDeleted");
            DropColumn("dbo.ReadChapters", "ModifiedOn");
            DropColumn("dbo.ReadChapters", "CreatedOn");
            DropColumn("dbo.Comments", "DeletedOn");
            DropColumn("dbo.Comments", "IsDeleted");
            DropColumn("dbo.Comments", "ModifiedOn");
            DropColumn("dbo.Genres", "DeletedOn");
            DropColumn("dbo.Genres", "IsDeleted");
            DropColumn("dbo.Genres", "ModifiedOn");
            DropColumn("dbo.Genres", "CreatedOn");
            DropColumn("dbo.Stories", "DeletedOn");
            DropColumn("dbo.Stories", "IsDeleted");
            DropColumn("dbo.Stories", "ModifiedOn");
            DropColumn("dbo.Stories", "CreatedOn");
            DropColumn("dbo.Chapters", "DeletedOn");
            DropColumn("dbo.Chapters", "IsDeleted");
            DropColumn("dbo.Chapters", "ModifiedOn");
            DropColumn("dbo.Chapters", "CreatedOn");
        }
    }
}
