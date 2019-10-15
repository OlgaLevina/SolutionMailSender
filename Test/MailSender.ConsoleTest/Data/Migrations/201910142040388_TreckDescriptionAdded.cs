namespace MailSender.ConsoleTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TreckDescriptionAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trecks", "Description", c => c.Byte());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trecks", "Description");
        }
    }
}
