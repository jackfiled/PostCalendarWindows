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
        public UserControlCalendarSetting()
        {
            InitializeComponent();

            //初始化学年度的选项
            foreach(string start_time in CalendarConst.semesters)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = start_time;
                semester_combobox.Items.Add(item);
            }
            semester_combobox.SelectedIndex = CalendarConst.semesters.IndexOf(CalendarConst.selected_semester);
        }

        private void semester_combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*string? result = (string)(sender as ComboBoxItem).Content;
            if(result != null)
            {
                CalendarConst.selected_semester = result;
            }*/
        }
    }
}
