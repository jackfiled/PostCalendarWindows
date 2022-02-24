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
        private Binding addButtonTextBindingObj = new Binding("AddButtonText");

        public UserControlActivityItem(ActivityItem item)
        {
            InitializeComponent();

            _item = item;
            nameBindingObj.Source = _item;
            startTimeBindingObj.Source = _item;
            startTimeBindingObj.Converter = new StartTimeStrConverter();
            endTimeBindingObj.Converter = new EndTimeStrConverter();
            endTimeBindingObj.Source = _item;
            detailsBindingObj.Source = _item;
            addButtonTextBindingObj.Source = _item;

            name_column.SetBinding(TextBlock.TextProperty, nameBindingObj);
            start_time_column.SetBinding(TextBlock.TextProperty, startTimeBindingObj);
            end_time_column.SetBinding(TextBlock.TextProperty, endTimeBindingObj);
            details_column.SetBinding(TextBlock.TextProperty, detailsBindingObj);
            add_button.SetBinding(ContentProperty, addButtonTextBindingObj);
        }

        private void hide_click(object sender, RoutedEventArgs e)
        {
            ItemButton? button = sender as ItemButton;
            if (_item.IsDDL)
            {
                button?.RaiseHideDDLEvent(_item.Id);
            }
            else
            {
                button?.RaiseHideDDLSpanEvent(_item.Id);
            }
        }

        private void add_click(object sender, RoutedEventArgs e)
        {
            ItemButton? button = sender as ItemButton;
            if (_item.IsDDL)
            {
                button?.RaiseAdd2DDLEvent(_item.Id);
            }
            else
            {
                button?.RaiseAdd2CalendarEvent(_item.Id);
            }
        }
    }

    /// <summary>
    /// 展示开始时间时加上前缀的转换器
    /// </summary>
    class StartTimeStrConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            string str = (string)value;
            if(str.Length == 0)
            {
                return "DDL";
            }
            else
            {
                return "开始时间 " + str;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            return value;
        }
    }

    /// <summary>
    /// 展示结束时间时加上前缀的转换器
    /// </summary>
    class EndTimeStrConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            return "结束时间 " + (string)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            return value;
        }
    }


}
