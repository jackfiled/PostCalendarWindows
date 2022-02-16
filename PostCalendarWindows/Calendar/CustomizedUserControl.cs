using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

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

        public static readonly RoutedEvent DeleteEvent = EventManager.RegisterRoutedEvent(
            "Delete", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Button));

        public event RoutedEventHandler Delete
        {
            add { AddHandler(DeleteEvent, value); }
            remove { RemoveHandler (DeleteEvent, value); }
        }

        /// <summary>
        /// 引发日历刷新事件
        /// </summary>
        public void RaiseRefreshEvent()
        {
            RoutedEventArgs newRefreshArgs = new RoutedEventArgs(RefreshEvent);
            RaiseEvent(newRefreshArgs);
        }

        /// <summary>
        /// 引发添加日历事件
        /// </summary>
        /// <param name="_event">需要添加的日历事件对象</param>
        public void RaiseAddEvent(CalendarEvent _event)
        {
            RoutedEventArgs newAddArgs = new RoutedEventArgs(AddEvent, _event);
            RaiseEvent(newAddArgs);
        }

        /// <summary>
        /// 引发修改日历事件
        /// </summary>
        /// <param name="e">需要修改的日历对象</param>
        public void RaiseChangeEvent(CalendarEvent e)
        {
            RoutedEventArgs newChangeArgs = new RoutedEventArgs(ChangeEvent, e); 
            RaiseEvent(newChangeArgs);
        }

        /// <summary>
        /// 引发删除日历事件
        /// </summary>
        /// <param name="id">需要删除的日历对象id</param>
        public void RaiseDeleteEvent(int id)
        {
            RoutedEventArgs newDeleteArgs = new RoutedEventArgs(DeleteEvent, id);
            RaiseEvent(newDeleteArgs);
        }
    }

    public class CustomizedColorZone : ColorZone
    {
        public static readonly RoutedEvent MoreEvent = EventManager.RegisterRoutedEvent(
            "More", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ColorZone));

        public event RoutedEventHandler More
        {
            add { AddHandler(MoreEvent, value); }
            remove { RemoveHandler(MoreEvent, value); }
        }

        public void RaiseMoreEvent(int id)
        {
            RoutedEventArgs moreEventArgs = new RoutedEventArgs(MoreEvent, id);
            RaiseEvent(moreEventArgs);
        }
    }
}
