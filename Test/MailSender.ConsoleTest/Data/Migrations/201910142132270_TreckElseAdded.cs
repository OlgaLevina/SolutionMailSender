namespace MailSender.ConsoleTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TreckElseAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trecks", "Else", c => c.Byte());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trecks", "Else");
        }
    }
}
