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

        private List<DDLColumnItem> columnItems = new List<DDLColumnItem>();

        private Binding ScrollHeightBindingObj = new Binding("Height");

        public UserControlDDL(Database db)
        {
            InitializeComponent();

            database = db;

            //设置滚动条的高度绑定
            ScrollHeightBindingObj.Source = this;
            ScrollHeightBindingObj.Converter = new DDLHeightConverter();
            scroll.SetBinding(HeightProperty, ScrollHeightBindingObj);

            //添加选择栏对象
            columnItems.Add(new DDLColumnItem("全部", PackIconKind.CalendarTextOutline, DDLType.All));
            columnItems.Add(new DDLColumnItem("学习", PackIconKind.BookOpen, DDLType.Study));
            columnItems.Add(new DDLColumnItem("个人", PackIconKind.Account, DDLType.Personal));

            RefreshColumn();
            columnItems[0].isClicked = true;
            columnItems[0].NotifyIsClickedChanged();
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
                    }
                    else
                    {
                        column.isClicked = false;
                    }
                }
            }
            RefreshColumn();
        }
    }
}
