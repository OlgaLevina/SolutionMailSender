using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MailSender.Controls;
using MailSenderLib.Entities;
using MailSenderLib.Data.LinqToSQL;
using System.Security;


namespace MailSender
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();//https://icon-icons.com/ru/pack/Evil-icons-user-interface/1097
        }

        private void MenuQuit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TabItemsSwitcher_LeftButtonClick(object sender, EventArgs e)
        {
            if (!(sender is TabItemsSwitcher switcher)) return;
            MainTabControl.SelectedIndex--;
            int tabCount = MainTabControl.Items.Count - 1;
            foreach (TabItem tab in MainTabControl.Items) if (tab.Visibility == Visibility.Hidden) tabCount--;
            if (MainTabControl.SelectedIndex == 0) switcher.LeftButtonVisible = false;
            if (MainTabControl.SelectedIndex == MainTabControl.Items.Count - 3) switcher.RightButtonVisible = true;
        }

        private void TabItemsSwitcher_RightButtonClick(object sender, EventArgs e)
        {
            if (!(sender is TabItemsSwitcher switcher)) return;
            MainTabControl.SelectedIndex++;
            int tabCount = MainTabControl.Items.Count - 1;
            foreach (TabItem tab in MainTabControl.Items) if (tab.Visibility == Visibility.Hidden) tabCount--;
            if (MainTabControl.SelectedIndex == MainTabControl.Items.Count - 2) switcher.RightButtonVisible = false;
            if (MainTabControl.SelectedIndex == 1) switcher.LeftButtonVisible = true;
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(TestPassword.SecurePassword.ToString());
            if (TestAdress.Text == string.Empty || TestPassword.SecurePassword.Length == 0 || MessageText.Text==string.Empty)
            {
                //https://social.msdn.microsoft.com/Forums/vstudio/ru-RU/c315d637-12da-4383-b598-e252363515dc/disable-button-if-passwordbox-is-empty?forum=wpf
                SendEndWindow result = new SendEndWindow();
                result.Show("Укажите данные Тестового отправителя на вкладке Планировщик, а также текст сообщения на вкладке Письма!!");
            }
            else {
                new EmailSendServiceClass(new MailSenderLib.Entities.Server(TestServer.SelectedItem as Server) { UserName = TestAdress.Text, Password = TestPassword.SecurePassword },
                    LettersList.SelectedItem as Email,
                    new SendEndWindow()).Send( new List<string>() { TestAdress.Text });
            }
            //string strLogin = cbSenderSelect.Text;
            // string strPassword = cbSenderSelect.SelectedValue.ToString();
            //  if (string.IsNullOrEmpty(strLogin))
            // { MessageBox.Show("Выберите отправителя"); return; }
            // if (string.IsNullOrEmpty(strPassword))
            //  { MessageBox.Show("Укажите пароль отправителя"); return; }
            //  EmailSendServiceClass emailSender = new EmailSendServiceClass(strLogin, strPassword);
            //  emailSender.SendMails((IQueryable<Email>)dgEmails.ItemsSource);
        }

        private void ButtonPlan_Click(object sender, RoutedEventArgs e)
        {
            //MailScheduler<Recipient> schedule = new MailScheduler<Recipient>();// рабочий вариант
            MailScheduler<string> schedule = new MailScheduler<string>();
            TimeSpan tsSendTime = schedule.GetSendTime(TimePick.Text);
            if (tsSendTime == new TimeSpan())
            { new SendEndWindow().Show("Некорректный формат даты"); return; }
            DateTime dtSendDateTime = (CalenderMail.SelectedDate ?? DateTime.Today).Add(tsSendTime);
            if (dtSendDateTime < DateTime.Now)
            { new SendEndWindow().Show("Дата и время отправки писем не могут быть раньше, чем настоящее время"); return; }
            //EmailSendServiceClass emailSender = new EmailSendServiceClass(cbSenderSelect.Text, cbSenderSelect.SelectedValue.ToString());
            schedule.PlanSendEmails(dtSendDateTime,
                new EmailSendServiceClass(new MailSenderLib.Entities.Server(TestServer.SelectedItem as Server) { UserName = TestAdress.Text, Password = TestPassword.SecurePassword },
                    LettersList.SelectedItem as Email,
                    new SendEndWindow()),
                //new Recipient() as IEnumerable<Recipient>,//!!!!
                //new MailSenderDBDataContext().Recipient,// рабочий вариант
                new List<string>() { TestAdress.Text },
                new SendEndWindow());

        }
    }
}
