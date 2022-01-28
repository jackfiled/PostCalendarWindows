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

namespace PostCalendarWindows.Setting
{
    /// <summary>
    /// UserControlSettingItem.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlSettingItem : UserControl
    {
        public ItemMenu _item;

        public UserControlSettingItem(ItemMenu item)
        {
            InitializeComponent();
            _item = item;
            this.DataContext = _item;
        }
    }
}
