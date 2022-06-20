using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;
using PostCalendarWindows.ViewModel;
using PostCalendarWindows.Setting;
using PostCalendarWindows.DataModel;

namespace PostCalendarWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UserControlCalendar calendar_user_control;
        UserControlDDL ddl_user_control;
        UserControlActivity activity_user_control;

        public MainWindow()
        {
            InitializeComponent();

            calendar_user_control = new UserControlCalendar();
            ddl_user_control = new UserControlDDL();
            activity_user_control = new UserControlActivity();

            //在这里手动设置高度绑定到StackPanelMain
            Binding height_binding_obj = new("Actual Height");
            height_binding_obj.Source = StackPanelMain;

            calendar_user_control.SetBinding(HeightProperty, height_binding_obj);
            ddl_user_control.SetBinding(HeightProperty, height_binding_obj);
            activity_user_control.SetBinding(HeightProperty, height_binding_obj);

            //显示相关的页面
            var calendar_item = new ItemMenu("日历", PackIconKind.Schedule, calendar_user_control);
            var ddl_item = new ItemMenu("DDL", PackIconKind.CalendarTextOutline, ddl_user_control);
            var activity_item = new ItemMenu("活动", PackIconKind.AccountGroupOutline, activity_user_control);

            ItemMenuList.Items.Add(new UserControlMenuItem(calendar_item));
            ItemMenuList.Items.Add(new UserControlMenuItem(ddl_item));
            ItemMenuList.Items.Add(new UserControlMenuItem(activity_item));

            //展示目前日历事件列表中的事件
            calendar_user_control.displayCanva();
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControlMenuItem selected_item = (UserControlMenuItem)((ListView)sender).SelectedItem;
            SwitchScreen(selected_item._item.Screen);
        }

        private void SwitchScreen(object sender)
        {
            if (sender is UserControl user_control)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(user_control);
            }
        }
    }
}
