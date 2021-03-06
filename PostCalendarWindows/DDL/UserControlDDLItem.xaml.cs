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
    /// UserControlDDLItem.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlDDLItem : UserControl
    {
        private DDLItem _item;
        private Binding nameBindingObj = new Binding("Name");
        private Binding timeBindingObj = new Binding("DDLTime");
        private Binding detailBindingObj = new Binding("Details");

        public UserControlDDLItem(DDLItem item)
        {
            InitializeComponent();

            _item = item;

            nameBindingObj.Source = _item;
            timeBindingObj.Source = _item;
            timeBindingObj.Converter = new TimeConverter();
            detailBindingObj.Source = _item;
            name_column.SetBinding(TextBlock.TextProperty, nameBindingObj);
            ddl_column.SetBinding(TextBlock.TextProperty, timeBindingObj);
            details_column.SetBinding(TextBlock.TextProperty, detailBindingObj);
        }

        private void more_click(object sender, RoutedEventArgs e)
        {
            ItemButton? button = sender as ItemButton;
            button?.RaiseMoreDDLEvent(_item.Id);
        }

        private void delete_click(object sender, RoutedEventArgs e)
        {
            ItemButton? button = sender as ItemButton;
            button?.RaiseDDLDeleteEvent(_item.Id);
        }

        private void finish_click(object sender, RoutedEventArgs e)
        {
            ItemButton? button = sender as ItemButton;
            button?.RaiseFinishDDLEvent(_item.Id);
        }
    }

    class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            return "DDL " + (string)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            return value;
        }
    }
}
