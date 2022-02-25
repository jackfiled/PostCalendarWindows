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
        private Binding ScrollViewerHeightBindingObj = new Binding("Height");
        private Binding calendarItemWidthBindingObj = new Binding("ActualWidth");
        private Binding AreaWidthBindingObj = new Binding("ActualWidth");
        private Binding AreaHeightBindingObj = new Binding("ActualHeight");

        private Calendar.CalendarManager calendar;
        private DataModel.Database db;
        private ColumnTimeData ctd;

        public UserControlCalendar(DataModel.Database database)
        {
            InitializeComponent();
            calendar = new Calendar.CalendarManager(database);
            db = database;

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
            ScrollViewerHeightBindingObj.Source = this;
            ScrollViewerHeightBindingObj.Converter = new CalendarHeightConverter();
            scroll.SetBinding(HeightProperty, ScrollViewerHeightBindingObj);

            //设置日历对象宽度的绑定器
            calendarItemWidthBindingObj.Source = reference;
            calendarItemWidthBindingObj.Converter = new CalendarItemWidthConverter();

            //设置展示区域的宽度和高度
            AreaHeightBindingObj.Source = this;
            AreaWidthBindingObj.Source = this;

            //这里显示日历上的日期
            ctd = new ColumnTimeData(calendar.week_first_day);

            var YearBinding = new Binding("Year");
            var SunBinding = new Binding("Sun");
            var MonBinding = new Binding("Mon");
            var TuesBinding = new Binding("Tues");
            var WedBinding = new Binding("Wed");
            var ThuBinding = new Binding("Thu");
            var FriBinding = new Binding("Fri");
            var SatBinding = new Binding("Sat");
            YearBinding.Source = ctd;
            SunBinding.Source = ctd;
            MonBinding.Source = ctd;
            TuesBinding.Source = ctd;
            WedBinding.Source = ctd;
            ThuBinding.Source = ctd;
            FriBinding.Source = ctd;
            SatBinding.Source = ctd;
            YearBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            SunBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            MonBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            TuesBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            WedBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            ThuBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            FriBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            SatBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            year_text.SetBinding(TextBlock.TextProperty, YearBinding);
            sun_text.SetBinding(TextBlock.TextProperty, SunBinding);
            mon_text.SetBinding(TextBlock.TextProperty, MonBinding);
            tues_text.SetBinding(TextBlock.TextProperty, TuesBinding);
            wed_text.SetBinding(TextBlock.TextProperty, WedBinding);
            thu_text.SetBinding(TextBlock.TextProperty, ThuBinding);
            fri_text.SetBinding(TextBlock.TextProperty, FriBinding);
            sat_text.SetBinding(TextBlock.TextProperty, SatBinding);
        }

        /// <summary>
        /// 清除画布
        /// </summary>
        public void clearCanva()
        {
            foreach (Canvas canva in canva_list)
            {
                canva.Children.Clear();
            }
        }

        /// <summary>
        /// 绘制日历类传来的所有组件
        /// </summary>
        public void displayCanva()
        {
            foreach (ShowItem e in calendar.show_items)
            {
                var calendarItem = new CalendarItem(e.id, e.name, e.place, e.length);
                var calendarItemUserControl = new UserControlCalendarItem(calendarItem);
                calendarItemUserControl.SetBinding(WidthProperty, calendarItemWidthBindingObj);
                canva_list[e.dayOfWeek].Children.Add(calendarItemUserControl);
                Canvas.SetTop(calendarItemUserControl, e.begin_length);
            }
        }

        /// <summary>
        /// 点击上一周按钮的事件处理
        /// </summary>
        private void last_week_click(object sender, RoutedEventArgs e)
        {
            calendar.week_first_day = calendar.week_first_day.AddDays(-7);
            ctd.AddDays(-7);
            calendar.Refresh();
            clearCanva();
            displayCanva();
        }

        /// <summary>
        /// 点击下一周按钮的事件处理
        /// </summary>
        private void next_week_click(object sender, RoutedEventArgs e)
        {
            calendar.week_first_day = calendar.week_first_day.AddDays(7);
            ctd.AddDays(7);
            calendar.Refresh();
            clearCanva();
            displayCanva();
        }

        /// <summary>
        /// 点击添加按钮的事件处理
        /// </summary>
        private void add_click(object sender, RoutedEventArgs e)
        {
            UCAddCalendar uCAddCalendar = new UCAddCalendar();

            uCAddCalendar.SetBinding(WidthProperty, AreaWidthBindingObj);
            uCAddCalendar.SetBinding(HeightProperty, AreaHeightBindingObj);
            area.Children.Clear();
            area.Children.Add(uCAddCalendar);
        }

        /// <summary>
        /// 处理在添加事件，修改事件界面引发的刷新日历事件
        /// </summary>
        private void calendar_refresh(object sender, RoutedEventArgs e)
        {
            area.Children.Clear();
            calendar.Refresh();
            clearCanva();
            displayCanva();
        }

        /// <summary>
        /// 处理添加日历事件
        /// </summary>
        private void calendar_add(object sender, RoutedEventArgs e)
        {
            CalendarEvent? _event = e.OriginalSource as CalendarEvent;
            if(_event != null)
            {
                db.CreateCalendarItem(_event);
            }
            calendar.Refresh();
            clearCanva();
            displayCanva();
        }

        /// <summary>
        /// 处理修改日历事件
        /// </summary>
        private void calendar_change(object sender, RoutedEventArgs e)
        {
            CalendarEvent? _event = e.OriginalSource as CalendarEvent;
            if(_event != null)
            {
                db.UpdateCalendarItem(_event);
            }
            calendar.Refresh();
            clearCanva();
            displayCanva();
        }

        /// <summary>
        /// 处理删除日历事件
        /// </summary>
        private void calendar_delete(object sender, RoutedEventArgs e)
        {
            int? id = e.OriginalSource as int?;
            if(id != null)
            {
                _ = db.DeleteCalendarItem((int)id);
            }
            calendar.Refresh();
            clearCanva();
            displayCanva();
        }

        /// <summary>
        /// 双击一个日历展示对象引发事件的处理
        /// </summary>
        private void calendar_more(object sender, RoutedEventArgs e)
        {
            int? id = e.OriginalSource as int?;
            if(id != null)
            {
                CalendarEvent? _event = db.ReadCalendarItem((int)id);
                if(_event != null)
                {
                    UCCalendarDetail calendarDetail = new UCCalendarDetail(_event);
                    calendarDetail.SetBinding(WidthProperty, AreaWidthBindingObj);
                    calendarDetail.SetBinding(HeightProperty, AreaHeightBindingObj);

                    area.Children.Clear();
                    area.Children.Add(calendarDetail);
                }
            }
        }

        private void open_excel_click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Excel File(xls)|*.xls";

            var result = openFileDialog.ShowDialog();

            if (result == true)
            {
                calendar.addCurriculumFromExcel(openFileDialog.FileName);
                clearCanva();
                displayCanva();
            }
        }
    }
}
