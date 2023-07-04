namespace RussianSongTranslation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alboms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ReleaseDate = c.DateTime(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SongInAlboms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlbomId = c.Int(nullable: false),
                        SongId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alboms", t => t.AlbomId, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .Index(t => t.AlbomId)
                .Index(t => t.SongId);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Text = c.String(),
                        Duration = c.Int(nullable: false),
                        Language = c.String(),
                        GenreId = c.Int(nullable: false),
                        SongerId = c.Int(nullable: false),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Songers", t => t.SongerId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.SongerId);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SongId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.SongId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Songers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProposedTranslations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SongId = c.Int(nullable: false),
                        Translation = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Songs", t => t.SongId, cascadeDelete: true)
                .Index(t => t.SongId);
            
            CreateTable(
                "dbo.TranslatedSongs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Translation = c.String(),
                        Song_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Songs", t => t.Song_Id, cascadeDelete: true)
                .Index(t => t.Song_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TranslatedSongs", "Song_Id", "dbo.Songs");
            DropForeignKey("dbo.ProposedTranslations", "SongId", "dbo.Songs");
            DropForeignKey("dbo.SongInAlboms", "SongId", "dbo.Songs");
            DropForeignKey("dbo.Songs", "SongerId", "dbo.Songers");
            DropForeignKey("dbo.Songs", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Favorites", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "SongId", "dbo.Songs");
            DropForeignKey("dbo.SongInAlboms", "AlbomId", "dbo.Alboms");
            DropIndex("dbo.TranslatedSongs", new[] { "Song_Id" });
            DropIndex("dbo.ProposedTranslations", new[] { "SongId" });
            DropIndex("dbo.Favorites", new[] { "UserId" });
            DropIndex("dbo.Favorites", new[] { "SongId" });
            DropIndex("dbo.Songs", new[] { "SongerId" });
            DropIndex("dbo.Songs", new[] { "GenreId" });
            DropIndex("dbo.SongInAlboms", new[] { "SongId" });
            DropIndex("dbo.SongInAlboms", new[] { "AlbomId" });
            DropTable("dbo.TranslatedSongs");
            DropTable("dbo.ProposedTranslations");
            DropTable("dbo.Songers");
            DropTable("dbo.Genres");
            DropTable("dbo.Users");
            DropTable("dbo.Favorites");
            DropTable("dbo.Songs");
            DropTable("dbo.SongInAlboms");
            DropTable("dbo.Alboms");
        }
    }
}
