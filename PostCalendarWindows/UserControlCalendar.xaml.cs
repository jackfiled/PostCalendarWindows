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
        private List<Canvas> canva_list = new List<Canvas>();
        private Binding calendarItemWidthBindingObj = new Binding("ActualWidth");

        public UserControlCalendar()
        {
            InitializeComponent();

            //这里的代码让我觉得我是傻逼
            canva_list.Add(SunCanva);
            canva_list.Add(MonCanva);
            canva_list.Add(TuesCanva);
            canva_list.Add(WedCanva);
            canva_list.Add(ThuCanva);
            canva_list.Add(FriCanva);
            canva_list.Add(SatCanva);

            //这里设置滚动条的高度与这个界面的高度绑定
            //创建相关的转换器以减去日历上方日期的高度
            var ScrollViewerHeightBindingObj = new Binding("Height");
            ScrollViewerHeightBindingObj.Source = this;
            ScrollViewerHeightBindingObj.Converter = new CalendarHeightConverter();
            scroll.SetBinding(HeightProperty, ScrollViewerHeightBindingObj);

            //设置宽度的绑定器
            calendarItemWidthBindingObj.Source = reference;
            calendarItemWidthBindingObj.Converter = new CalendarItemWidthConverter();

            //这里显示日历上的日期
            DateTime dt = DateTime.Now;
            ColumnTimeData ctd = new ColumnTimeData(dt.Year, dt.Month, dt.Day, dt.DayOfWeek);
            this.DataContext = ctd;

        }

        //清除全部的画布
        public void clearCanva()
        {
            foreach(Canvas canva in canva_list)
            {
                canva.Children.Clear();
            }
        }

        //绘制日历类传来的所有组件
        public void displayCanva(List<Event> event_list)
        {
            foreach(Event e in event_list)
            {
                var calendarItem = new CalendarItem(e.name, e.place, e.length);
                var calendarItemUserControl = new UserControlCalendarItem(calendarItem);
                calendarItemUserControl.SetBinding(WidthProperty, calendarItemWidthBindingObj);
                canva_list[e.dayOfWeek].Children.Add(calendarItemUserControl);
                Canvas.SetTop(calendarItemUserControl, e.begin_length);
            }
        }
    }
}
