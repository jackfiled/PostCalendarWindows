using System.Windows.Controls;
using PostCalendarWindows.ViewModel;

namespace PostCalendarWindows
{
    /// <summary>
    /// UserControlMenuItem.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        MainWindow _context;
        public UserControlMenuItem(ItemMenu itemMenu, MainWindow context)
        {
            InitializeComponent();

            _context = context;

            this.DataContext = itemMenu;
        }

        private void ListViewMenu_Selected(object sender, SelectedCellsChangedEventArgs e)
        {
            _context.SwitchScreen(((ItemMenu)((ListView)sender).SelectedItem).Screen);
        }
    }
}
