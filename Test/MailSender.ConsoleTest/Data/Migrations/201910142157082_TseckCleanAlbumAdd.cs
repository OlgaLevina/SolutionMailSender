namespace MailSender.ConsoleTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TseckCleanAlbumAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Album",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TreckAlbums",
                c => new
                    {
                        Treck_Id = c.Int(nullable: false),
                        Album_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Treck_Id, t.Album_Id })
                .ForeignKey("dbo.Trecks", t => t.Treck_Id, cascadeDelete: true)
                .ForeignKey("dbo.Album", t => t.Album_Id, cascadeDelete: true)
                .Index(t => t.Treck_Id)
                .Index(t => t.Album_Id);
            
            DropColumn("dbo.Trecks", "Description");
            DropColumn("dbo.Trecks", "Else");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Trecks", "Else", c => c.Byte());
            AddColumn("dbo.Trecks", "Description", c => c.Byte());
            DropForeignKey("dbo.TreckAlbums", "Album_Id", "dbo.Album");
            DropForeignKey("dbo.TreckAlbums", "Treck_Id", "dbo.Trecks");
            DropIndex("dbo.TreckAlbums", new[] { "Album_Id" });
            DropIndex("dbo.TreckAlbums", new[] { "Treck_Id" });
            DropTable("dbo.TreckAlbums");
            DropTable("dbo.Album");
        }
    }
}
