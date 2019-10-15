using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest.Data.Migrations
{
    public partial class TreckStyleAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trecks", "Style", c=> c.Byte());
            //sql - запросы могут быть здесь
        }

        public override void Down()
        {
            DropColumn("dbo.Trecks", "Style");
        }
    }
}
