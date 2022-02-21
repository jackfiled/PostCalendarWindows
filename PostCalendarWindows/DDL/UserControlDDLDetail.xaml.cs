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
            activty_type_column.SetBinding(TextBlock.TextProperty, activityTypeColumnBindingObj);

            nameBindingObj.Source = _item;
            detailBindingObj.Source = _item;
            endDateBindingObj.Source = _item;
            endTimeBindingObj.Source = _item;

            name_input.SetBinding(TextBox.TextProperty, nameBindingObj);
            detail_input.SetBinding(TextBox.TextProperty, detailBindingObj);
            end_date_input.SetBinding(TextBox.TextProperty, endDateBindingObj);
            end_time_input.SetBinding(TextBox.TextProperty, endTimeBindingObj);

            if (item.isDDL)
            {
                activty_type_column.Visibility = Visibility.Collapsed;
                activity_type_input.Visibility = Visibility.Collapsed;
            }
            else
            {
                ddl_type_column.Visibility = Visibility.Collapsed;
                ddl_type_input.Visibility = Visibility.Collapsed;
            }
        }
    }
}
