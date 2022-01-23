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
using PostCalendarWindows.Calendar;
using PostCalendarWindows.ViewModel;

namespace PostCalendarWindows
{
    /// <summary>
    /// Calendar.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlCalendar : UserControl
    {
        public UserControlCalendar()
        {
            InitializeComponent();

            //这里设置滚动条的高度与这个界面的高度绑定
            //创建相关的转换器以减去日历上方日期的高度
            var ScrollViewerHeightBindingObj = new Binding("Height");
            ScrollViewerHeightBindingObj.Source = this;
            ScrollViewerHeightBindingObj.Converter = new CalendarHeightConverter();
            scroll.SetBinding(HeightProperty, ScrollViewerHeightBindingObj);

            //这里显示日历上的日期
            DateTime dt = DateTime.Now;
            ColumnTimeData ctd = new ColumnTimeData(dt.Year, dt.Month, dt.Day, dt.DayOfWeek);
            this.DataContext = ctd;

            UserControlCalendarItem testItem = new UserControlCalendarItem(50);
            var calendarItemBindingObj = new Binding("ActualWidth");
            calendarItemBindingObj.Source = reference;
            calendarItemBindingObj.Converter = new CalendarItemWidthConverter();
            testItem.SetBinding(WidthProperty, calendarItemBindingObj);

            canva.Children.Add(testItem);
        }
    }
}
