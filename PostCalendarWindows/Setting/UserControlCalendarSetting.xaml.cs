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

namespace PostCalendarWindows.Setting
{
    /// <summary>
    /// UserControlCalendarSetting.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlCalendarSetting : UserControl
    {
        public int teaching_week_count = 19;
        public UserControlCalendarSetting()
        {
            InitializeComponent();

            for(int i = 1; i <= teaching_week_count; i++)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = i;
                teaching_week_combobox.Items.Add(item);
            }
        }
    }
}
