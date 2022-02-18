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
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using PostCalendarWindows.ViewModel;

namespace PostCalendarWindows.DDL
{
    /// <summary>
    /// UCDDLColumnItem.xaml 的交互逻辑
    /// </summary>
    public partial class UCDDLColumnItem : UserControl
    {
        public DDLColumnItem _item;

        private Binding contentBindingObj = new Binding("content");
        private Binding packIconBindingObj = new Binding("packIconKind");
        private Binding colorBindingObj = new Binding("isClicked");

        public UCDDLColumnItem(DDLColumnItem ddlColumnItem)
        {
            InitializeComponent();

            _item = ddlColumnItem;
            contentBindingObj.Source = _item;
            packIconBindingObj.Source = _item;
            colorBindingObj.Source = _item;
            colorBindingObj.Converter = new ColorConverter();
            packIcon.SetBinding(PackIcon.KindProperty, packIconBindingObj);
            textBlock.SetBinding(TextBlock.TextProperty, contentBindingObj);
            rectangle.SetBinding(Rectangle.FillProperty, colorBindingObj);
        }

        private void DDLColumnGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DDLColumnGrid? grid = sender as DDLColumnGrid;
            if (grid != null)
            {
                grid.RaiseSelectEvent(_item);
            }
        }
    }

    /// <summary>
    /// 栏目选择下方状态条颜色的绑定的转换器
    /// </summary>
    class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            bool _value = (bool)value;
            if (_value)
            {
                return Brushes.White;
            }
            else
            {
                return PrimaryColor.Blue;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            return value;
        }
    }
}
