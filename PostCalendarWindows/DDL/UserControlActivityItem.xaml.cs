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

namespace PostCalendarWindows.DDL
{
    /// <summary>
    /// UserControlActivityItem.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlActivityItem : UserControl
    {
        private ActivityItem _item;
        private Binding nameBindingObj = new Binding("Name");
        private Binding startTimeBindingObj = new Binding("StartTime");
        private Binding endTimeBindingObj = new Binding("EndTime");
        private Binding detailsBindingObj = new Binding("Details");

        public UserControlActivityItem(ActivityItem item)
        {
            InitializeComponent();

            _item = item;
            nameBindingObj.Source = _item;
            startTimeBindingObj.Source = _item;
            endTimeBindingObj.Source = _item;
            detailsBindingObj.Source = _item;

            name_column.SetBinding(TextBlock.TextProperty, nameBindingObj);
            start_time_column.SetBinding(TextBlock.TextProperty, startTimeBindingObj);
            end_time_column.SetBinding(TextBlock.TextProperty, endTimeBindingObj);
            details_column.SetBinding(TextBlock.TextProperty, detailsBindingObj);
        }
    }
}
