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
using System.Windows.Shapes;

namespace MailSender.Controls
{
    /// <summary>
    /// Логика взаимодействия для SendEndWindow.xaml
    /// </summary>
    public partial class SendEndWindow : Window, IResult
    {
        public SendEndWindow()
        {
            InitializeComponent();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void Show(string message)
        {
            this.LabelResult.Text = message;//!!!!
            this.Show(); }

    }
}
