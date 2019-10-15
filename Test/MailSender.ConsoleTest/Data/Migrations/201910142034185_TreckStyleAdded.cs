namespace MailSender.ConsoleTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TreckStyleAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trecks", "Style", c => c.Byte(defaultValue: 0));//назначение дефолтного значения не помогла - сплошные нулы
            //здесьь могут быть скл-запросы
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trecks", "Style");
            //здесьь могут быть скл-запросы
        }
    }
}
