using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
            var calendar = new UserControlCalendar();
            var ddl = new UserContorlDDL();

            //在这里手动设置高度绑定到StackPanelMain
            var calendarBindingObj = new Binding("ActualHeight");
            calendarBindingObj.Source = StackPanelMain;
            BindingOperations.SetBinding(calendar, UserControlCalendar.HeightProperty, calendarBindingObj);

            var ddlBindingObj = new Binding("ActualHeight");
            calendarBindingObj.Source = StackPanelMain;
            BindingOperations.SetBinding(ddl, UserContorlDDL.HeightProperty, ddlBindingObj);

            var calendar_item = new ItemMenu("日历", PackIconKind.Schedule, calendar);
            var ddl_item = new ItemMenu("DDL", PackIconKind.ScaleBalance, ddl);

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
