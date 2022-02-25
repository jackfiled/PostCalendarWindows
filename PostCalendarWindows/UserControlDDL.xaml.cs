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
using PostCalendarWindows.DDL;
using PostCalendarWindows.ViewModel;
using PostCalendarWindows.DataModel;

namespace PostCalendarWindows
{
    /// <summary>
    /// UserContorlDDL.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlDDL : UserControl
    {
        public Database database;
        public DeadlineManager manager;

        private List<DDLColumnItem> columnItems = new List<DDLColumnItem>();
        private DDLType selected_type;

        private Binding ScrollHeightBindingObj = new Binding("Height");
        private Binding areaWidthBindingObj = new Binding("ActualWidth");
        private Binding areaHeightBindingObj = new Binding("ActualHeight");

        public UserControlDDL(Database db)
        {
            InitializeComponent();

            database = db;
            manager = new DeadlineManager(db);

            //设置滚动条的高度绑定
            ScrollHeightBindingObj.Source = this;
            ScrollHeightBindingObj.Converter = new DDLHeightConverter();
            scroll.SetBinding(HeightProperty, ScrollHeightBindingObj);

            //设置展示区域的宽度与高度绑定的对象
            areaWidthBindingObj.Source= this;
            areaHeightBindingObj.Source = this;

            //添加选择栏对象
            columnItems.Add(new DDLColumnItem("全部", PackIconKind.CalendarTextOutline, DDLType.All));
            columnItems.Add(new DDLColumnItem("学习", PackIconKind.BookOpen, DDLType.Study));
            columnItems.Add(new DDLColumnItem("个人", PackIconKind.Account, DDLType.Personal));

            //初始化界面
            RefreshColumn();
            columnItems[0].isClicked = true;
            selected_type = DDLType.All;
            columnItems[0].NotifyIsClickedChanged();
            Refresh();
        }

        /// <summary>
        /// 刷新选择栏
        /// </summary>
        private void RefreshColumn()
        {
            column_stack_plane.Children.Clear();
            foreach(DDLColumnItem item in columnItems)
            {
                column_stack_plane.Children.Add(new UCDDLColumnItem(item));
            }
        }

        /// <summary>
        /// 刷新需要显示的ddl事件
        /// </summary>
        private void Refresh()
        {
            main_stack_plane.Children.Clear();
            manager.LoadDeadlineFromDB(selected_type);
            foreach (var item in manager.ddlShowItems)
            {
                main_stack_plane.Children.Add(new UserControlDDLItem(item));
            }
        }

        private void column_item_select(object sender, RoutedEventArgs e)
        {
            DDLColumnItem? item = e.OriginalSource as DDLColumnItem;

            if(item != null)
            {
                foreach(DDLColumnItem column in columnItems)
                {
                    if(item.itemType == column.itemType)
                    {
                        selected_type = (DDLType)column.itemType;
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
            RefreshColumn();
            Refresh();
        }

        private void ddl_refresh(object sender, RoutedEventArgs e)
        {
            area.Children.Clear();
            Refresh();
        }

        private void ddl_more(object sender, RoutedEventArgs e)
        {
            int id = (int)e.OriginalSource;
            DeadlineEvent? _event = database.ReadDDLEvent(id);
            if(_event != null)
            {
                UserControlDetail detail = new UserControlDetail();
                detail.InitMoreDDL(_event);
                detail.SetBinding(HeightProperty, areaHeightBindingObj);
                detail.SetBinding(WidthProperty, areaWidthBindingObj);
                area.Children.Add(detail);
            }
        }

        private void ddl_finish(object sender, RoutedEventArgs e)
        {
            int id = (int)e.OriginalSource;
            database.DeleteDDLEvent(id);

            Refresh();
        }

        private void ddl_delete(object sender, RoutedEventArgs e)
        {
            int id = (int)e.OriginalSource;
            database.DeleteDDLEvent(id);

            Refresh();
        }

        private void ddl_add(object sender, RoutedEventArgs e)
        {
            DeadlineEvent? _event = e.OriginalSource as DeadlineEvent;
            if(_event != null)
            {
                database.CreateDDLEvent(_event);
            }
        }

        private void ddl_span_add(object sender, RoutedEventArgs e)
        {
            DeadlineSpanEvent? _event = e.OriginalSource as DeadlineSpanEvent;
            if(_event != null)
            {
                database.CreateDDLSpanEvent(_event);
            }
        }

        private void ddl_update(object sender, RoutedEventArgs e)
        {
            DeadlineEvent? _event = e.OriginalSource as DeadlineEvent;
            if(_event != null)
            {
                database.UpdateDDLEvent(_event);
            }
        }

        private void ddl_span_update(object sender, RoutedEventArgs e)
        {
            DeadlineSpanEvent? _event = e.OriginalSource as DeadlineSpanEvent;
            if(_event != null)
            {
                database.UpdateDDLSpanEvent(_event);
            }
        }
    }
}
