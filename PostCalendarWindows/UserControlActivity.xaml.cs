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

namespace PostCalendarWindows
{
    /// <summary>
    /// UserControlActivity.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlActivity : UserControl
    {
        private List<DDLColumnItem> columnItems = new List<DDLColumnItem>();

        public UserControlActivity()
        {
            InitializeComponent();

            columnItems.Add(new DDLColumnItem("全部", PackIconKind.AccountGroupOutline, ActivityType.All));
            columnItems.Add(new DDLColumnItem("思政", PackIconKind.AccountTie, ActivityType.Thought));
            columnItems.Add(new DDLColumnItem("文体", PackIconKind.DanceBallroom, ActivityType.PE));
            columnItems.Add(new DDLColumnItem("志愿", PackIconKind.YinYang, ActivityType.Volunteer));
            columnItems.Add(new DDLColumnItem("讲座", PackIconKind.Desk, ActivityType.Lecture));
            columnItems.Add(new DDLColumnItem("竞赛", PackIconKind.ArmFlex, ActivityType.Competition));
            columnItems.Add(new DDLColumnItem("评优", PackIconKind.ArrangeBringForward, ActivityType.Recoginition));
            columnItems.Add(new DDLColumnItem("其他", PackIconKind.AccountGroupOutline, ActivityType.Other));

            RefreshColumn();
            columnItems[0].isClicked = true;
            columnItems[0].NotifyIsClickedChanged();//这里必须调用这个方法，不调用绑定就不会生效，但是在第二次点击时就不用调用这个方法了
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
            DDLColumnItem? item  = e.OriginalSource as DDLColumnItem;
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
