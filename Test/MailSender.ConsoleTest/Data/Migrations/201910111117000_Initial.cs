using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace MailSender.ConsoleTest.Data.Migrations
{
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artists",
                c=> new
                {
                    Id=c.Int(nullable: false, identity: true),
                    Name=c.String(nullable: false),
                    Birthday=c.DateTime(nullable:false),
                }).PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Trecks",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    Length = c.Int(nullable: false),
                    Artist_Id=c.Int(),
                }).PrimaryKey(t => t.Id).ForeignKey("dbo.Artists", t => t.Artist_Id).Index(t=>t.Artist_Id);
        }
        public override void Down()
        {
            DropForeignKey("dbo.Trecks", "Artist_Id", "dbo.Artists");
            DropIndex("dbo.Trecks", new[] { "Artist_Id" });
            DropTable("dbo.Trecks");
            DropTable("dbo.Artists");
        }
    }
}
