using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using PostCalendarWindows.ViewModel;

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

            var calendar_item = new ItemMenu("日历", PackIconKind.Schedule);
            var ddl_item = new ItemMenu("DDL", PackIconKind.ScaleBalance);

            Menu.Children.Add(new UserControlMenuItem(calendar_item, this));
            Menu.Children.Add(new UserControlMenuItem(ddl_item, this));
        }

        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);
            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }
    }
}
