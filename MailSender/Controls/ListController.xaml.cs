using System;
using System.Collections;
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
using System.ComponentModel;

namespace MailSender.Controls
{
    /// <summary>
    /// Логика взаимодействия для ListController.xaml
    /// </summary>
    public partial class ListController : UserControl
    {
        #region Items //для реализации возможностей привязок
        public static readonly DependencyProperty ItemsProperty = 
            DependencyProperty.Register(
            "Items",
            typeof(IEnumerable),
            typeof(ListController),
            new PropertyMetadata(default(IEnumerable),OnItemsChanged,ItemsCoerceValue),// либо - (null), т.к. в нашем случае будет все равно null по default
            ItemsValidate
            );
        private static bool ItemsValidate(object value) { return true; } //проверка соответствия передаваемого в свойство объекта на соответствие типа данных, если не будет соответствия, то свойство не воспримет переданный объект, а приложение не бует порушено
        private static object ItemsCoerceValue(DependencyObject d, object baseValue) { return baseValue;}//позволяет корректировать значение по каким-либо ограничениям, например, если оно должно быть только больше 0
        private static void OnItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) { } //вызывается каждый раз на изменение, в нем можно производить изменения интерфейса пользователя, хотя это не очень хороший подход
        public IEnumerable Items
        {
            get=>(IEnumerable)GetValue(ItemsProperty);
            set=>SetValue(ItemsProperty,value);
        }
        #endregion
        #region SelectedItem //выбранный элемент
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(object),
            typeof(ListController),
            new PropertyMetadata(default(object))
            );
        [Description("Выбранный элемент")]
        public object SelectedItem
        {
            get => (object)GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }
        #endregion
        #region DisplayMemberPath //показываемый элемент
        public static readonly DependencyProperty DisplayMemberPathProperty =
            DependencyProperty.Register(
            nameof(DisplayMemberPath),
            typeof(string),
            typeof(ListController),
            new PropertyMetadata(default(string))
            );
        [Description("Отображаемые данные")]
        public string DisplayMemberPath
        {
            get => (string)GetValue(DisplayMemberPathProperty);
            set => SetValue(DisplayMemberPathProperty, value);
        }
        #endregion
        #region ValueMemberPath //элемент значения
        public static readonly DependencyProperty ValueMemberPathProperty =
            DependencyProperty.Register(
            nameof(ValueMemberPath),
            typeof(string),
            typeof(ListController),
            new PropertyMetadata(default(string))
            );
        [Description("Элемент значения")]
        public string ValueMemberPath
        {
            get => (string)GetValue(ValueMemberPathProperty);
            set => SetValue(ValueMemberPathProperty, value);
        }
        #endregion
        #region PanelName //назмание панели
        public static readonly DependencyProperty PanelNameProperty =
            DependencyProperty.Register(
            nameof(PanelName),
            typeof(string),
            typeof(ListController),
            new PropertyMetadata(default(string))
            );
        [Description("Название панели")]
        public string PanelName
        {
            get => (string)GetValue(PanelNameProperty);
            set => SetValue(PanelNameProperty, value);
        }
        #endregion
        #region SelectedItemIndex //индекс выбранного элемента
        public static readonly DependencyProperty SelectedItemIndexProperty =
            DependencyProperty.Register(
            nameof(SelectedItemIndex),
            typeof(int),
            typeof(ListController),
            new PropertyMetadata(default(int))
            );
        [Description("Индекс выбранного элемента")]
        public int SelectedItemIndex
        {
            get => (int)GetValue(SelectedItemIndexProperty);
            set => SetValue(SelectedItemIndexProperty, value);
        }
        #endregion
        #region CommandDelete //команда Delete
        public static readonly DependencyProperty CommandDeleteProperty =
            DependencyProperty.Register(
            nameof(CommandDelete),
            typeof(ICommand),
            typeof(ListController),
            new PropertyMetadata(default(ICommand))
            );
        [Description("Команда Delete")]
        public ICommand CommandDelete
        {
            get => (ICommand)GetValue(CommandDeleteProperty);
            set => SetValue(CommandDeleteProperty, value);
        }
        #endregion
        #region CommandEdit //команда Edit
        public static readonly DependencyProperty CommandEditProperty =
            DependencyProperty.Register(
            nameof(CommandEdit),
            typeof(ICommand),
            typeof(ListController),
            new PropertyMetadata(default(ICommand))
            );
        [Description("Команда Edit")]
        public ICommand CommandEdit
        {
            get => (ICommand)GetValue(CommandEditProperty);
            set => SetValue(CommandEditProperty, value);
        }
        #endregion
        #region CommandCreate //команда создания
        public static readonly DependencyProperty CommandCreateProperty =
            DependencyProperty.Register(
            nameof(CommandCreate),
            typeof(ICommand),
            typeof(ListController),
            new PropertyMetadata(default(ICommand))
            );
        [Description("Команда создания")]
        public ICommand CommandCreate
        {
            get => (ICommand)GetValue(CommandCreateProperty);
            set => SetValue(CommandCreateProperty, value);
        }
        #endregion
        #region ItemTemplate //шаблон визуализации данных
        public static readonly DependencyProperty ItemTemplateProperty =
            DependencyProperty.Register(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(ListController),
            new PropertyMetadata(default(DataTemplate))
            );
        [Description("Шаблон визуализации данных")]
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }
        #endregion
        public ListController()
        {
            InitializeComponent();
        }
    }
}
