using System.Windows.Controls;
using PostCalendarWindows.ViewModel;

namespace PostCalendarWindows
{
    /// <summary>
    /// UserControlMenuItem.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlMenuItem : UserControl
    {
        public ItemMenu _item;
        public UserControlMenuItem(ItemMenu itemMenu)
        {
            InitializeComponent();

            this.DataContext = itemMenu;
            _item = itemMenu;
        }
    }

}
