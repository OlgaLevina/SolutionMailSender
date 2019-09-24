using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Security;
using System;
using System.Windows.Controls;
using System.Collections.Generic;

namespace MailSenderWPFTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class WpfMailSender
    {


        public WpfMailSender()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            PersonMail author = new PersonMail
            { Login= LoginValue.Text,Password= PasswordValue.SecurePassword, Name=UserNameValue.Text,Email=UserEmailValue.Text};
            MessageContet msg = new MessageContet { Body = MessageBodyValue.Text, Subject = MessageSubjectValue.Text };
            string[] adresses = MessageEmailsValue.Text.Split(',');
            List<string> adressList=new List<string>();
            foreach (string adr in adresses) { adressList.Add(adr.Trim(' ')); }
            EmailSendServiceClass.Send(new KeyValuePair<string, int>(SenderServis.Host,SenderServis.Port),
                author,adressList,msg, new SendEndWindow());
        }

        private void CreatButton_Click(object sender, RoutedEventArgs e)
        {
            MessageContet msg = new MessageContet();
            MessageBodyValue.Text = msg.Body; MessageSubjectValue.Text = msg.Subject;
            UserEmailValue.Text = LoginValue.Text;
            MessageEmailsValue.Text = UserEmailValue.Text;
        }

        private void Value_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            { if (sender is TextBox) { (sender as TextBox).Text = string.Empty; } }
        }
    }

}
