using System.Windows;
using System.Net;
using System.Net.Mail;
using System.Security;
using System;

namespace MailSenderWPFTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string host = "smtp.mail.ru";//гугл не рекомендуется - расценит как врага, можно smtp.mail.ru smtp.yandex.ru
            int port = 25;
            string userName = UserNameValue.Text;
            SecureString password = PasswordValue.SecurePassword;//для хранения паролей, сторонние не смогут прочитать, но для передачи в бд не подойдет, нужно будет преобразовывать в строку
            string msg = $"Hello, world! {DateTime.Now}";
            using (SmtpClient client = new SmtpClient(host, port))
            {
                client.EnableSsl = true;//подтверждаем шифрование
                client.Credentials = new NetworkCredential(userName,password);//учетные данные
                //другие параметры из методички можно не включать
                using (MailMessage message = new MailMessage())
                {
                    message.From = new MailAddress("sergolga@mail.ru", "Olga");
                    message.To.Add(new MailAddress("sergolga@mail.ru","Olga"));
                    message.Subject = "Hello";
                    message.Body = msg;
                    message.IsBodyHtml = false;
                    // message.Attachments.Add(new Attachment(file))
                    try { client.Send(message); MessageBox.Show("Was sent!"); } catch (Exception error){ MessageBox.Show(error.Message, "Get problem"); }
                }
            }
        }
    }

}
