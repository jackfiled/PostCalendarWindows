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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PostCalendarWindows.ViewModel;

namespace PostCalendarWindows.Calendar
{
    /// <summary>
    /// UserControlCalendarItem.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlCalendarItem : UserControl
    {
        private CalendarItem _item;
        public UserControlCalendarItem(CalendarItem item)
        {
            InitializeComponent();

            _item = item;
            background.Height = item.height;
            this.DataContext = item;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           background.RaiseMoreEvent(_item.id);
        }
    }
}
