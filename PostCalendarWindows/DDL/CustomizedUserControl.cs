using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using PostCalendarWindows.ViewModel;

namespace PostCalendarWindows.DDL
{
    /// <summary>
    /// 用于DDL界面顶端控制栏的Grid控件
    /// 支持选择事件
    /// </summary>
    public class DDLColumnGrid : Grid
    {
        public static readonly RoutedEvent SelectEvent = EventManager.RegisterRoutedEvent(
            "Select", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Grid));

        public event RoutedEventHandler Select
        {
            add { AddHandler(SelectEvent, value); }
            remove { RemoveHandler(SelectEvent, value);}
        }

        public void RaiseSelectEvent(DDLColumnItem item)
        {
            RoutedEventArgs newSelectArgs = new RoutedEventArgs(SelectEvent, item);
            RaiseEvent(newSelectArgs);
        }
    }
}
