using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;
using PostCalendarWindows.ViewModel;

namespace PostCalendarWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Calendar.Calendar calendar = new Calendar.Calendar();
        UserControlCalendar calendarUserControl = new UserControlCalendar();
        UserContorlDDL ddlUserControl = new UserContorlDDL();

        public MainWindow()
        {
            InitializeComponent();

            //显示相关的页面
            //在这里手动设置高度绑定到StackPanelMain
            var calendarBindingObj = new Binding("ActualHeight");
            calendarBindingObj.Source = StackPanelMain;
            calendarUserControl.SetBinding(HeightProperty, calendarBindingObj);

            var ddlBindingObj = new Binding("ActualHeight");
            ddlBindingObj.Source = StackPanelMain;
            ddlUserControl.SetBinding(HeightProperty, ddlBindingObj);

            var calendar_item = new ItemMenu("日历", PackIconKind.Schedule, calendarUserControl);
            var ddl_item = new ItemMenu("DDL", PackIconKind.ScaleBalance, ddlUserControl);

            ItemMenuList.Items.Add(new UserControlMenuItem(calendar_item));
            ItemMenuList.Items.Add(new UserControlMenuItem(ddl_item));

            //展示目前日历事件列表中的事件
            calendarUserControl.displayCanva(calendar.show_items);
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

        //点击加载excel表格按钮的方法
        private void openExcelClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Excel File(xls)|*.xls";

            var result = openFileDialog.ShowDialog();

            if(result == true)
            {
                calendar.addCurriculumFromExcel(openFileDialog.FileName);
                calendarUserControl.clearCanva();
                calendarUserControl.displayCanva(calendar.show_items);

            }
        }
    }
}
