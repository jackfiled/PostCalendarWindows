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

            //设置展示区域的宽度与高度
            areaWidthBindingObj.Source= this;
            areaHeightBindingObj.Source = this;

            //添加选择栏对象
            columnItems.Add(new DDLColumnItem("全部", PackIconKind.CalendarTextOutline, DDLType.All));
            columnItems.Add(new DDLColumnItem("学习", PackIconKind.BookOpen, DDLType.Study));
            columnItems.Add(new DDLColumnItem("个人", PackIconKind.Account, DDLType.Personal));

            //初始化界面
            RefreshColumn();
            columnItems[0].isClicked = true;
            columnItems[0].NotifyIsClickedChanged();
            manager.LoadDeadlineFromDB(DDLType.All);
            Refresh();

            UserControlDetail detail = new UserControlDetail();
            detail.InitAddActivity();
            detail.SetBinding(WidthProperty, areaWidthBindingObj);
            detail.SetBinding(HeightProperty, areaHeightBindingObj);
            area.Children.Add(detail);
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

        private void column_item_select(object sender, RoutedEventArgs e)
        {
            DDLColumnItem? item = e.OriginalSource as DDLColumnItem;

            if(item != null)
            {
                foreach(DDLColumnItem column in columnItems)
                {
                    if(item.itemType == column.itemType)
                    {
                        column.isClicked = true;
                        column.NotifyIsClickedChanged();
                    }
                    else
                    {
                        column.isClicked = false;
                        column.NotifyIsClickedChanged();
                    }
                }

                manager.LoadDeadlineFromDB((DDLType)item.itemType);
            }
            RefreshColumn();
            Refresh();
        }

        /// <summary>
        /// 刷新需要显示的ddl事件
        /// </summary>
        private void Refresh()
        {
            main_stack_plane.Children.Clear();
            foreach(var item in manager.ddlShowItems)
            {
                main_stack_plane.Children.Add(new UserControlDDLItem(item));
            }
        }
    }
}
