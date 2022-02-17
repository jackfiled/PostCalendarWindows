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

namespace PostCalendarWindows
{
    /// <summary>
    /// UserContorlDDL.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlDDL : UserControl
    {

        private List<DDLColumnItem> firstColumnItems = new List<DDLColumnItem>();
        private List<DDLColumnItem> ddlColumnItems = new List<DDLColumnItem>();
        private List<DDLColumnItem> activityColumnItems = new List<DDLColumnItem>();

        public UserControlDDL()
        {
            InitializeComponent();

            //初始化第一个选择栏
            firstColumnItems.Add(new DDLColumnItem("DDL", PackIconKind.CalendarTextOutline, ColumnType.Main, DDLClassify.DDL));
            firstColumnItems.Add(new DDLColumnItem("活动", PackIconKind.AccountGroupOutline, ColumnType.Main, DDLClassify.Activity));

            //初始化ddl的选择栏
            ddlColumnItems.Add(new DDLColumnItem("全部", PackIconKind.CalendarTextOutline, ColumnType.DDL, DDLType.All));
            ddlColumnItems.Add(new DDLColumnItem("学习", PackIconKind.BookOpen, ColumnType.DDL, DDLType.Study));
            ddlColumnItems.Add(new DDLColumnItem("个人", PackIconKind.Account, ColumnType.DDL, DDLType.Personal));

            //初始化活动的选择栏
            activityColumnItems.Add(new DDLColumnItem("全部", PackIconKind.AccountGroupOutline, ColumnType.Activity, ActivityType.All));
            activityColumnItems.Add(new DDLColumnItem("思政", PackIconKind.AccountTie, ColumnType.Activity, ActivityType.Thought));
            activityColumnItems.Add(new DDLColumnItem("文体", PackIconKind.DanceBallroom, ColumnType.Activity, ActivityType.PE));
            activityColumnItems.Add(new DDLColumnItem("志愿", PackIconKind.YinYang, ColumnType.Activity, ActivityType.Volunteer));
            activityColumnItems.Add(new DDLColumnItem("讲座", PackIconKind.Desk, ColumnType.Activity, ActivityType.Lecture));
            activityColumnItems.Add(new DDLColumnItem("竞赛", PackIconKind.ArmFlex, ColumnType.Activity, ActivityType.Competition));
            activityColumnItems.Add(new DDLColumnItem("评优", PackIconKind.ArrangeBringForward, ColumnType.Activity, ActivityType.Recoginition));
            activityColumnItems.Add(new DDLColumnItem("其他", PackIconKind.AccountGroupOutline, ColumnType.Activity, ActivityType.Other));
            
            foreach(var item in firstColumnItems)
            {
                first_column.Children.Add(new UCDDLColumnItem(item));
            }
            foreach(var item in ddlColumnItems)
            {
                second_column.Children.Add(new UCDDLColumnItem(item));
            }
        }

        private void Column_Select(object sender, RoutedEventArgs e)
        {
            DDLColumnItem? item = e.OriginalSource as DDLColumnItem;
            if(item != null)
            {
                switch (item.type)
                {
                    case ColumnType.Main:
                        DDLClassify ddlClassify = (DDLClassify)item.itemType;
                        if(ddlClassify == (DDLClassify)firstColumnItems[0].itemType)
                        {
                            firstColumnItems[0].isClicked = true;
                            firstColumnItems[0].NotifyIsClickedChanged();
                            firstColumnItems[1].isClicked = false;
                            firstColumnItems[1].NotifyIsClickedChanged();
                            RefreshSecondColumn(ddlColumnItems);
                        }
                        else
                        {
                            firstColumnItems[1].isClicked = true;
                            firstColumnItems[1].NotifyIsClickedChanged();
                            firstColumnItems[0].isClicked = false;
                            firstColumnItems[0].NotifyIsClickedChanged();
                            RefreshSecondColumn(activityColumnItems);
                        }
                        break;
                    case ColumnType.DDL:
                        DDLType ddlType = (DDLType)item.itemType;
                        foreach(var columnItem in ddlColumnItems)
                        {
                            if(ddlType == (DDLType)columnItem.itemType)
                            {
                                columnItem.isClicked = true;
                            }
                            else
                            {
                                columnItem.isClicked = false;
                            }
                            columnItem.NotifyIsClickedChanged();
                        }
                        break;
                    case ColumnType.Activity:
                        ActivityType activityType = (ActivityType)item.itemType;
                        foreach(var columnItem in activityColumnItems)
                        {
                            if((ActivityType)columnItem.itemType == activityType)
                            {
                                columnItem.isClicked = true;
                            }
                            else
                            {
                                columnItem.isClicked = false;
                            }
                            columnItem.NotifyIsClickedChanged();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void RefreshSecondColumn(List<DDLColumnItem> items)
        {
            second_column.Children.Clear();
            foreach(var item in items)
            {
                second_column.Children.Add(new UCDDLColumnItem(item));
            }
        }
    }
}
