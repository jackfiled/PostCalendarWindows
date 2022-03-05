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
using PostCalendarWindows.ViewModel;
using PostCalendarWindows.DDL;
using PostCalendarWindows.DataModel;

namespace PostCalendarWindows
{
    /// <summary>
    /// UserControlActivity.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlActivity : UserControl
    {
        public Database database;
        public DeadlineManager manager;

        private List<DDLColumnItem> columnItems = new List<DDLColumnItem>();
        private ActivityType select_type;

        private Binding ScrollHeightBindingObj = new Binding("Height");
        private Binding areaHeightBindingObj = new Binding("ActualHeight");
        private Binding areaWidthBindingObj = new Binding("ActualWidth");

        public UserControlActivity(Database db)
        {
            InitializeComponent();

            database = db;
            manager = new DeadlineManager(database);

            //设置滚动条的高度绑定
            ScrollHeightBindingObj.Source = this;
            ScrollHeightBindingObj.Converter = new DDLHeightConverter();
            scroll.SetBinding(HeightProperty, ScrollHeightBindingObj);

            //设置显示区域的宽高绑定
            areaHeightBindingObj.Source = this;
            areaWidthBindingObj.Source= this;

            columnItems.Add(new DDLColumnItem("全部", PackIconKind.AccountGroupOutline, ActivityType.All));
            columnItems.Add(new DDLColumnItem("思政", PackIconKind.AccountTie, ActivityType.Thought));
            columnItems.Add(new DDLColumnItem("文体", PackIconKind.DanceBallroom, ActivityType.PE));
            columnItems.Add(new DDLColumnItem("志愿", PackIconKind.HandHeart, ActivityType.Volunteer));
            columnItems.Add(new DDLColumnItem("讲座", PackIconKind.Desk, ActivityType.Lecture));
            columnItems.Add(new DDLColumnItem("竞赛", PackIconKind.ArmFlex, ActivityType.Competition));
            columnItems.Add(new DDLColumnItem("评优", PackIconKind.ArrangeBringForward, ActivityType.Recoginition));
            columnItems.Add(new DDLColumnItem("其他", PackIconKind.AccountGroupOutline, ActivityType.Other));

            //初始化显示的对象们
            RefreshColumn();
            columnItems[0].isClicked = true;
            select_type = ActivityType.All;
            columnItems[0].NotifyIsClickedChanged();
            Refresh();
        }

        /// <summary>
        /// 刷新选择栏
        /// </summary>
        private void RefreshColumn()
        {
            column_stack_plane.Children.Clear();
            manager.LoadActivityFromDB(select_type);
            foreach(DDLColumnItem item in columnItems)
            {
                column_stack_plane.Children.Add(new UCDDLColumnItem(item));
            }
        }

        private void column_item_select(object sender, RoutedEventArgs e)
        {
            DDLColumnItem? item  = e.OriginalSource as DDLColumnItem;
            if(item != null)
            {
                foreach(DDLColumnItem column in columnItems)
                {
                    if(item.itemType == column.itemType)
                    {
                        select_type = (ActivityType)column.itemType;
                        column.isClicked = true;
                        column.NotifyIsClickedChanged();
                    }
                    else
                    {
                        column.isClicked = false;
                        column.NotifyIsClickedChanged();
                    }
                }
            }
            Refresh();
        }

        private void Refresh()
        {
            main_stack_plane.Children.Clear();
            manager.LoadActivityFromDB(select_type);
            foreach(ActivityItem item in manager.activityItems)
            {
                main_stack_plane.Children.Add(new UserControlActivityItem(item));
            }
        }

        private void add_2_calendar(object sender, RoutedEventArgs e)
        {
            int id = (int)e.OriginalSource;
            DeadlineSpanEvent? _event = database.ReadDDLSpanEvent(id);

            if(_event != null)
            {
                Calendar.UCAddCalendar? addArea = manager.InitAddCalendarArea(_event);
                if(addArea != null)
                {
                    area.Children.Add(addArea);
                    addArea.SetBinding(WidthProperty, areaWidthBindingObj);
                    addArea.SetBinding(HeightProperty, areaHeightBindingObj);
                }
            }
        }

        private void add_2_ddl(object sender, RoutedEventArgs e)
        {
            int id = (int)e.OriginalSource;
            DeadlineEvent? _event = database.ReadDDLEvent(id);
            if( _event != null)
            {
                _event.ddlType = DDLType.Personal;
                _event.activityType = ActivityType.NotActivity;
                database.CreateDDLEvent(_event);
            }
        }

        private void add_calendar(object sender, RoutedEventArgs e)
        {
            Calendar.CalendarEvent? _event = e.OriginalSource as Calendar.CalendarEvent;

            if( _event != null)
            {
                database.CreateCalendarItem(_event);
            }

            area.Children.Clear();
            Refresh();
        }

        private void refresh_scene(object sender, RoutedEventArgs e)
        {
            area.Children.Clear();
            Refresh();
        }
    }
}
