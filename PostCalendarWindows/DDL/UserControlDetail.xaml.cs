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
        /// 初始化添加ddl的界面
        /// </summary>
        public void InitAddDDL()
        {
            detailItem = new DDLDetailItem(true);
            ucDDLDetail = new UserControlDDLDetail(detailItem);

            main_stack_plane.Children.Add(ucDDLDetail);

            topColumnBindingObj.Source = detailItem;
            top_column.SetBinding(TextBlock.TextProperty, topColumnBindingObj);
        }


        /// <summary>
        /// 初始化查看ddl事件详情的界面
        /// </summary>
        /// <param name="_event"></param>
        public void InitMoreDDL(DeadlineEvent _event)
        {
            detailItem = new DDLDetailItem(true, _event);
            ucDDLDetail= new UserControlDDLDetail(detailItem);

            main_stack_plane.Children.Add(ucDDLDetail);

            topColumnBindingObj.Source= detailItem;
            top_column.SetBinding(TextBlock.TextProperty, topColumnBindingObj);
        }

        /// <summary>
        /// 初始化添加活动的界面
        /// </summary>
        public void InitAddActivity()
        {
            IsAddActivity = true;
            detailItem = new DDLDetailItem(false);
            spanDetailItem = new DDLSpanDetailItem();
            ucDDLDetail = new UserControlDDLDetail(detailItem);
            ucDDLSpanDetail = new UserControlDDLSpanDetail(spanDetailItem);

            //设置默认显示添加ddl时间点事件
            ddlColumnItem.isClicked = true;
            ddlColumnItem.NotifyIsClickedChanged();
            column_stack_plane.Children.Add(new UCDDLColumnItem(ddlColumnItem));
            column_stack_plane.Children.Add(new UCDDLColumnItem(ddlSpanColumnItem));
            main_stack_plane.Children.Add(ucDDLDetail);

            topColumnBindingObj.Source = detailItem;
            top_column.SetBinding(TextBlock.TextProperty, topColumnBindingObj);
        }

        public void InitMoreActivity(DeadlineEvent _event)
        {
            detailItem = new DDLDetailItem(false, _event);
            ucDDLDetail = new UserControlDDLDetail(detailItem);

            main_stack_plane.Children.Add(ucDDLDetail);

            topColumnBindingObj.Source = detailItem;
            top_column.SetBinding(TextBlock.TextProperty, topColumnBindingObj);
        }

        public void InitMoreActivity(DeadlineSpanEvent _event)
        {
            spanDetailItem = new DDLSpanDetailItem(_event);
            ucDDLSpanDetail= new UserControlDDLSpanDetail(spanDetailItem);

            main_stack_plane.Children.Add(ucDDLSpanDetail);

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
