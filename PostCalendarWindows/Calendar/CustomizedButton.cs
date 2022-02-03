using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PostCalendarWindows.Calendar
{
    /// <summary>
    /// 支持日历刷新，添加，更改三个事件的按钮对象
    /// </summary>
    public class CustomizedButton : Button
    {
        public static readonly RoutedEvent RefreshEvent = EventManager.RegisterRoutedEvent(
            "Refresh", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Button));

        public event RoutedEventHandler Refresh
        {
            add { AddHandler(RefreshEvent, value); }
            remove { RemoveHandler(RefreshEvent,value); }
        }

        public static readonly RoutedEvent AddEvent = EventManager.RegisterRoutedEvent(
            "Add", RoutingStrategy.Bubble, typeof (RoutedEventHandler), typeof(Button));

        public event RoutedEventHandler Add
        {
            add { AddHandler(AddEvent, value);}
            remove { RemoveHandler(AddEvent, value); }
        }

        public static readonly RoutedEvent ChangeEvent = EventManager.RegisterRoutedEvent(
            "Change", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Button));

        public event RoutedEventHandler Change
        {
            add { AddHandler(ChangeEvent, value);}
            remove { RemoveHandler(ChangeEvent, value); }
        }

        public void RaiseRefreshEvent()
        {
            RoutedEventArgs newRefreshArgs = new RoutedEventArgs(RefreshEvent);
            RaiseEvent(newRefreshArgs);
        }

        public void RaiseAddEvent()
        {
            RoutedEventArgs newAddArgs = new RoutedEventArgs(AddEvent);
            RaiseEvent(newAddArgs);
        }

        public void RaiseChangeEvent()
        {
            RoutedEventArgs newChangeArgs = new RoutedEventArgs(ChangeEvent); 
            RaiseEvent(newChangeArgs);
        }
    }
}
