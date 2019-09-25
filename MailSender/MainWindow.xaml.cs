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
            int tabCount = MainTabControl.Items.Count-1;
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
            if(TestAdress.Text==string.Empty || TestPassword.Text==string.Empty)
            {
                SendEndWindow result = new SendEndWindow();
                result.ShowResult("Укажите данные Тестового отправителя на вкладке Планировщик!!");
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

        }
    }
}
