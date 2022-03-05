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

namespace PostCalendarWindows.DDL
{
    /// <summary>
    /// UserControlDDLDetail.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlDDLDetail : UserControl
    {
        private DDLDetailItem _item;

        private Binding nameColumnBindingObj = new Binding("NameColumn");
        private Binding detailColumnBindingObj = new Binding("DetailColumn");
        private Binding endTimeColumnBindingObj = new Binding("EndTimeColumn");
        private Binding ddlTypeColumnBindingObj = new Binding("DDLTypeColumn");
        private Binding activityTypeColumnBindingObj = new Binding("ActivityTypeColumn");

        private Binding nameBindingObj = new Binding("Name");
        private Binding detailBindingObj = new Binding("Detail");
        private Binding endDateBindingObj = new Binding("EndDateStr");
        private Binding endTimeBindingObj = new Binding("EndTimeStr");

        public UserControlDDLDetail(DDLDetailItem item)
        {
            InitializeComponent();

            _item = item;
            nameColumnBindingObj.Source = _item;
            detailColumnBindingObj.Source = _item;
            endTimeColumnBindingObj.Source = _item;
            ddlTypeColumnBindingObj.Source = _item;
            activityTypeColumnBindingObj.Source = _item;

            name_column.SetBinding(TextBlock.TextProperty, nameColumnBindingObj);
            detail_column.SetBinding(TextBlock.TextProperty, detailColumnBindingObj);
            end_time_column.SetBinding(TextBlock.TextProperty, endTimeColumnBindingObj);
            ddl_type_column.SetBinding(TextBlock.TextProperty, ddlTypeColumnBindingObj);
            activity_type_column.SetBinding(TextBlock.TextProperty, activityTypeColumnBindingObj);

            nameBindingObj.Source = _item;
            detailBindingObj.Source = _item;
            endDateBindingObj.Source = _item;
            endTimeBindingObj.Source = _item;
            endDateBindingObj.Mode = BindingMode.TwoWay;
            endTimeBindingObj.Mode = BindingMode.TwoWay;

            name_input.SetBinding(TextBox.TextProperty, nameBindingObj);
            detail_input.SetBinding(TextBox.TextProperty, detailBindingObj);
            end_date_input.SetBinding(DatePicker.TextProperty, endDateBindingObj);
            end_time_input.SetBinding(TimePicker.TextProperty, endTimeBindingObj);

            if (item.isDDL)
            {
                activity_type_column.Visibility = Visibility.Collapsed;
                activity_type_input.Visibility = Visibility.Collapsed;
                if (item.isUpdate)
                {
                    ddl_type_input.SelectedIndex = (int)item.ddlType - 1;
                    activity_type_input.SelectedIndex = (int)item.activityType - 1;
                }
                else
                {
                    activity_type_input.SelectedIndex = (int)ActivityType.NotActivity - 1;
                }
            }
            else
            {
                ddl_type_column.Visibility = Visibility.Collapsed;
                ddl_type_input.Visibility = Visibility.Collapsed;
                activity_type_column.Margin = new Thickness(30, 245, 0, 0);
                activity_type_input.Margin = new Thickness(150, 240, 0, 0);
                if (item.isUpdate)
                {
                    ddl_type_input.SelectedIndex = (int)item.ddlType;
                    activity_type_input.SelectedIndex = (int)item.activityType;
                }
                else
                {
                    ddl_type_input.SelectedIndex = (int)DDLType.NotDDL;
                }
            }
        }

        private void confirm_button_Click(object sender, RoutedEventArgs e)
        {
            DetailButton? button = sender as DetailButton;
            if (_item.isUpdate)
            {
                DeadlineEvent _event = new DeadlineEvent();
                _event.Id = _item.Id;//由于是更改事件，需要设置事件的id
                if (_item.isDDL)
                {
                    _event.SetInner(_item.Name, _item.Detail, _item.EndDateTime, (DDLType)(ddl_type_input.SelectedIndex + 1), ActivityType.NotActivity);
                }
                else
                {
                    _event.SetInner(_item.Name, _item.Detail, _item.EndDateTime, DDLType.NotDDL, (ActivityType)(activity_type_input.SelectedIndex + 1));
                }
                button?.RaiseDDLUpdateEvent(_event);
            }
            else
            {
                DeadlineEvent _event = new DeadlineEvent();
                if (_item.isDDL)
                {
                    _event.SetInner(_item.Name, _item.Detail, _item.EndDateTime, (DDLType)(ddl_type_input.SelectedIndex + 1), ActivityType.NotActivity);
                }
                else
                {
                    _event.SetInner(_item.Name, _item.Detail, _item.EndDateTime, DDLType.NotDDL, (ActivityType)(activity_type_input.SelectedIndex + 1));
                }
                button?.RaiseDDLAddEvent(_event);
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
