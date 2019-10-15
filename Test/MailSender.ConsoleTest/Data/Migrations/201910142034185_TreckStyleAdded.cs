namespace MailSender.ConsoleTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TreckStyleAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trecks", "Style", c => c.Byte(defaultValue: 0));//���������� ���������� �������� �� ������� - �������� ����
            //������ ����� ���� ���-�������
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trecks", "Style");
            //������ ����� ���� ���-�������
        }
    }
}
