using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostCalendarWindows.DataModel;

namespace PostCalendarWindows.DDL
{
    public class DeadlineManager
    {
        private Database database;

        public DeadlineManager(Database db)
        {
            database = db;
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
