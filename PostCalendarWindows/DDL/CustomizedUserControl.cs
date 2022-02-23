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

    public class DetailButton : Button
    {
        /// <summary>
        /// 刷新事件，显示主界面
        /// </summary>
        public static readonly RoutedEvent RefreshDDLEvent = EventManager.RegisterRoutedEvent(
            "RefreshDDL", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Button));

        public event RoutedEventHandler RefreshDDL
        {
            add { AddHandler(RefreshDDLEvent, value); }
            remove { RemoveHandler(RefreshDDLEvent, value); }
        }

        /// <summary>
        /// 添加DDL时间点事件
        /// </summary>
        public static readonly RoutedEvent AddDDLEvent = EventManager.RegisterRoutedEvent(
            "AddDDL", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Button));

        public event RoutedEventHandler AddDDL
        {
            add { AddHandler(AddDDLEvent, value); }
            remove { RemoveHandler(AddDDLEvent, value); }
        }

        /// <summary>
        /// 添加ddl时间段事件
        /// </summary>
        public static readonly RoutedEvent AddDDLSpanEvent = EventManager.RegisterRoutedEvent(
            "AddDDLSpan", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Button));

        public event RoutedEventHandler AddDDLSpan
        {
            add { AddHandler(AddDDLSpanEvent, value); }
            remove { RemoveHandler(AddDDLSpanEvent, value); }
        }

        /// <summary>
        /// 更新ddl时间点事件
        /// </summary>
        public static readonly RoutedEvent UpdateDDLEvent = EventManager.RegisterRoutedEvent(
            "UpdateDDL", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Button));

        public event RoutedEventHandler UpdateDDL
        {
            add { AddHandler(UpdateDDLEvent, value); }
            remove { RemoveHandler(UpdateDDLEvent, value); }
        }

        /// <summary>
        /// 更新DDL时间段事件
        /// </summary>
        public static readonly RoutedEvent UpdateDDLSpanEvent = EventManager.RegisterRoutedEvent(
            "UpdateDDLSpan", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Button));

        public event RoutedEventHandler UpdateDDLSpan
        {
            add { AddHandler(UpdateDDLSpanEvent, value); }
            remove { RemoveHandler(UpdateDDLSpanEvent, value); }
        }

        /// <summary>
        /// 引发界面刷新事件
        /// </summary>
        public void RaiseRefreshEvent()
        {
            RoutedEventArgs newRefreshArgs = new RoutedEventArgs(RefreshDDLEvent);
            RaiseEvent(newRefreshArgs);
        }

        /// <summary>
        /// 引发添加ddl时间点事件
        /// </summary>
        /// <param name="_event">需要添加的ddl时间点事件</param>
        public void RaiseDDLAddEvent(DeadlineEvent _event)
        {
            RoutedEventArgs newDDLAddEvent = new RoutedEventArgs(AddDDLEvent, _event);
            RaiseEvent(newDDLAddEvent);
        }

        /// <summary>
        /// 引发添加ddl时间段事件
        /// </summary>
        ///<param name="_event">需要添加的ddl时间段时间</param>
        public void RaiseDDLSpanAddEvent(DeadlineSpanEvent _event)
        {
            RoutedEventArgs newDDLSpanAddEvent = new RoutedEventArgs(AddDDLSpanEvent, _event);
            RaiseEvent(newDDLSpanAddEvent);
        }

        /// <summary>
        /// 引发更新ddl时间点事件
        /// </summary>
        /// <param name="_event">需要更新的ddl时间点事件</param>
        public void RaiseDDLUpdateEvent(DeadlineEvent _event)
        {
            RoutedEventArgs newDDLUpdateArgs = new RoutedEventArgs(UpdateDDLEvent, _event);
            RaiseEvent(newDDLUpdateArgs);
        }

        /// <summary>
        /// 引发更新ddl时间段事件
        /// </summary>
        /// <param name="_event"></param>
        public void RaiseDDLSpanUpdateEvent(DeadlineSpanEvent _event)
        {
            RoutedEventArgs newDDLSpanUpdateArgs = new RoutedEventArgs(UpdateDDLSpanEvent, _event);
            RaiseEvent(newDDLSpanUpdateArgs);
        }
    }
}
