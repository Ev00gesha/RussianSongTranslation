namespace RussianSongTranslation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProposedTranslations", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.ProposedTranslations", "TranslationName", c => c.String());
            CreateIndex("dbo.ProposedTranslations", "UserId");
            AddForeignKey("dbo.ProposedTranslations", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProposedTranslations", "UserId", "dbo.Users");
            DropIndex("dbo.ProposedTranslations", new[] { "UserId" });
            DropColumn("dbo.ProposedTranslations", "TranslationName");
            DropColumn("dbo.ProposedTranslations", "UserId");
        }
    }
}
