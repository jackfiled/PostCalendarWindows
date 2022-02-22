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

#pragma warning disable CS8618

namespace PostCalendarWindows.DDL
{
    /// <summary>
    /// UserControlDetail.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlDetail : UserControl
    {
        private DDLColumnItem ddlColumnItem = new DDLColumnItem("时间点", PackIconKind.CalendarText, ColumnType.DDL);
        private DDLColumnItem ddlSpanColumnItem = new DDLColumnItem("时间段", PackIconKind.AccountGroupOutline, ColumnType.DDLSpan);
        private DDLDetailItem detailItem;
        private DDLSpanDetailItem spanDetailItem;
        private UserControlDDLDetail ucDDLDetail;
        private UserControlDDLSpanDetail ucDDLSpanDetail;

        private Binding topColumnBindingObj = new Binding("TopColumn");

        /// <summary>
        /// 表示是否为添加活动的界面，只有这个界面需要显示添加ddl与ddlspan两种情况
        /// </summary>
        private bool IsAddActivity = false;

        public UserControlDetail()
        {
            InitializeComponent();
        }


        /// <summary>
        /// ddl界面所用的构造函数
        /// </summary>
        /// <param name="item"></param>
        public UserControlDetail(DDLDetailItem item)
        {
            InitializeComponent();

            top_column.Text = "添加DDL";

            column_stack_plane.Children.Add(new UCDDLColumnItem(ddlColumnItem));
            ddlColumnItem.isClicked = true;
            ddlColumnItem.NotifyIsClickedChanged();

            main_stack_plane.Children.Add(new UserControlDDLDetail(item));
        }

        public void AddDDL()
        {

        }

        public void AddActivity()
        {
            IsAddActivity = true;
            detailItem = new DDLDetailItem(false);
            spanDetailItem = new DDLSpanDetailItem();
            ucDDLDetail = new UserControlDDLDetail(detailItem);
            ucDDLSpanDetail = new UserControlDDLSpanDetail(spanDetailItem);

            column_stack_plane.Children.Add(new UCDDLColumnItem(ddlColumnItem));
            column_stack_plane.Children.Add(new UCDDLColumnItem(ddlSpanColumnItem));
            main_stack_plane.Children.Add(ucDDLDetail);

            topColumnBindingObj.Source = spanDetailItem;
            top_column.SetBinding(TextBlock.TextProperty, topColumnBindingObj);
        }

        private void column_item_select(object sender, RoutedEventArgs e)
        {
            DDLColumnItem? item = e.OriginalSource as DDLColumnItem;
            if(item != null && IsAddActivity)
            {
                ColumnType type = (ColumnType)item.itemType;
                if(type == ColumnType.DDL)
                {
                    ddlColumnItem.isClicked = true;
                    ddlSpanColumnItem.isClicked = false;
                    ddlColumnItem.NotifyIsClickedChanged();
                    ddlSpanColumnItem.NotifyIsClickedChanged();

                    main_stack_plane.Children.Clear();
                    main_stack_plane.Children.Add(ucDDLDetail);
                }
                else
                {
                    ddlColumnItem.isClicked = false;
                    ddlSpanColumnItem.isClicked = true;
                    ddlColumnItem.NotifyIsClickedChanged();
                    ddlSpanColumnItem.NotifyIsClickedChanged();

                    main_stack_plane.Children.Clear();
                    main_stack_plane.Children.Add(ucDDLSpanDetail);
                }
            }
        }
    }
}
