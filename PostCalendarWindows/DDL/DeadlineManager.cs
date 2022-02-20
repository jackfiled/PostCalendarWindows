using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostCalendarWindows.DataModel;
using PostCalendarWindows.ViewModel;

namespace PostCalendarWindows.DDL
{
    public class DeadlineManager
    {
        public List<DDLItem> ddlShowItems = new List<DDLItem>();
        public  List<ActivityItem> activityItems = new List<ActivityItem>();

        private Database database;

        public DeadlineManager(Database db)
        {
            database = db;
        }

        public void LoadDeadlineFromDB(DDLType type)
        {
            ddlShowItems.Clear();
             List<DeadlineEvent> events = database.LoadDeadline(type, DateTime.Now.AddDays(60));
            events.Sort();
            foreach(DeadlineEvent e in events)
            {
                ddlShowItems.Add(new DDLItem(e));
            }
        }

        public void LoadActivityFromDB(ActivityType type)
        {
            activityItems.Clear();

            List<DeadlineEvent> deadline_event_list = database.LoadDeadline(type, DateTime.Now.AddDays(60));
            List<DeadlineSpanEvent> deadlineSpan_event_list = database.LoadDeadlineSpan(type, DateTime.Now.AddDays(60));

            foreach(DeadlineEvent e in deadline_event_list)
            {
                activityItems.Add(new ActivityItem(e));
            }
            foreach(DeadlineSpanEvent e in deadlineSpan_event_list)
            {
                activityItems.Add(new ActivityItem(e));
            }

            activityItems.Sort();
        }
    }

    /// <summary>
    /// 用于ddl，活动部分滚动条高度绑定的转换器
    /// </summary>
    public class DDLHeightConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            double _value = (double)value;
            if(_value > 45)
            {
                return _value - 45;
            }
            else
            {
                return _value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo cultureInfo)
        {
            return value;
        }
    }
}
