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
using PostCalendarWindows.ViewModel;

namespace PostCalendarWindows.DDL
{
    /// <summary>
    /// UserControlDDLSpanDetail.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlDDLSpanDetail : UserControl
    {
        public DDLSpanDetailItem spanItem;

        //显示栏的绑定初始化
        private Binding nameColumnBindingObj = new Binding("NameColumn");
        private Binding detailColumnBindingObj = new Binding("DetailColumn");
        private Binding startTimeColumnBindingObj = new Binding("StartTimeColumn");
        private Binding endTimeColumnBindingObj = new Binding("EndTimeColumn");
        private Binding activityTypeColumnBindingObj = new Binding("ActivityTypeColumn");

        //输入栏的绑定初始化
        private Binding nameBindingObj = new Binding("Name");
        private Binding detailBindingObj = new Binding("Detail");
        private Binding startDateBindingObj = new Binding("StartDateStr");
        private Binding startTimeBindingObj = new Binding("StartTimeStr");
        private Binding endDateBindingObj = new Binding("EndDateStr");
        private Binding endTimeBindingObj = new Binding("EndTimeStr");

        public UserControlDDLSpanDetail(DDLSpanDetailItem _item)
        {
            InitializeComponent();

            spanItem = _item;

            //设置绑定源
            nameColumnBindingObj.Source = spanItem;
            detailColumnBindingObj.Source = spanItem;
            startTimeColumnBindingObj.Source = spanItem;
            endTimeColumnBindingObj.Source = spanItem;
            activityTypeColumnBindingObj.Source = spanItem;

            nameBindingObj.Source = spanItem;
            detailBindingObj.Source = spanItem;
            startDateBindingObj.Source = spanItem;
            startTimeBindingObj.Source = spanItem;
            endDateBindingObj.Source = spanItem;
            endTimeBindingObj.Source = spanItem;

            name_column.SetBinding(TextBlock.TextProperty, nameColumnBindingObj);
            detail_column.SetBinding(TextBlock.TextProperty, detailColumnBindingObj);
            start_time_column.SetBinding(TextBlock.TextProperty, startTimeColumnBindingObj);
            end_time_column.SetBinding(TextBlock.TextProperty, endTimeColumnBindingObj);
            activty_type_column.SetBinding(TextBlock.TextProperty, activityTypeColumnBindingObj);

            name_input.SetBinding(TextBox.TextProperty, nameBindingObj);
            detail_input.SetBinding(TextBox.TextProperty, detailBindingObj);
            start_date_input.SetBinding(TextBox.TextProperty, startDateBindingObj);
            start_time_input.SetBinding(TextBox.TextProperty, startTimeBindingObj);
            end_date_input.SetBinding(TextBox.TextProperty, endDateBindingObj);
            end_time_input.SetBinding(TextBox.TextProperty, endTimeBindingObj);

            if (spanItem.isUpdate)
            {
                activity_type_input.SelectedIndex = (int)spanItem.activityType;
            }
        }

        private void confirm_button_Click(object sender, RoutedEventArgs e)
        {
            DetailButton? button = sender as DetailButton;

            if (spanItem.isUpdate)
            {
                DeadlineSpanEvent _event = new DeadlineSpanEvent();
                _event.SetInner(spanItem.Name, spanItem.Detail, spanItem.StartDateTime, spanItem.EndDateTime, (ActivityType)activity_type_input.SelectedIndex);
                _event.Id = spanItem.Id;
                button?.RaiseDDLSpanUpdateEvent(_event);
            }
            else
            {
                DeadlineSpanEvent _event = new DeadlineSpanEvent();
                _event.SetInner(spanItem.Name, spanItem.Detail, spanItem.StartDateTime, spanItem.EndDateTime, (ActivityType)activity_type_input.SelectedIndex);
                button?.RaiseDDLSpanAddEvent(_event);
            }
            button?.RaiseRefreshEvent();
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            DetailButton? button = sender as DetailButton;
            button?.RaiseRefreshEvent();
        }
    }
}
