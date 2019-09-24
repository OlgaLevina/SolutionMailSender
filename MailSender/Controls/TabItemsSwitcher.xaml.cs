using System;
using System.Windows;
using System.Windows.Controls;

namespace MailSender.Controls
{
    /// <summary>
    /// Логика взаимодействия для TabItemsSwitcher.xaml
    /// </summary>
    public partial class TabItemsSwitcher : UserControl
    {
        public event EventHandler LeftButtonClick;
        public event EventHandler RightButtonClick;
        public bool LeftButtonVisible
        {
            get => LeftButton.Visibility == Visibility;
            set => LeftButton.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }
        public bool RightButtonVisible
        {
            get => RightButton.Visibility == Visibility;
            set => RightButton.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
        }

        public TabItemsSwitcher()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (!(e.Source is Button button)) return;
            switch(button.Name)
            {
                case "LeftButton":
                    LeftButtonClick?.Invoke(this, EventArgs.Empty);
                    break;
                case "RightButton":
                    RightButtonClick?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }
    }
}