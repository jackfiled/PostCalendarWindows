using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using PostCalendarWindows.ViewModel;
using PostCalendarWindows.Calendar;
using PostCalendarWindows.DDL;

namespace PostCalendarWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            var calendar_item = new ItemMenu("日历", PackIconKind.Schedule, new UserControlCalendar());
            // 这里在初始化日历控件时，传入的参数是滚动日历的高度
            var ddl_item = new ItemMenu("DDL", PackIconKind.ScaleBalance, new UserContorlDDL());

            ItemMenuList.Items.Add(new UserControlMenuItem(calendar_item));
            ItemMenuList.Items.Add(new UserControlMenuItem(ddl_item));
        }

        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControlMenuItem selected_item = (UserControlMenuItem)((ListView)sender).SelectedItem;
            SwitchScreen(selected_item._item.Screen);
        }

        private void SwitchScreen(object sender)
        {
            var screen = (UserControl)sender;
            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }
    }
}
