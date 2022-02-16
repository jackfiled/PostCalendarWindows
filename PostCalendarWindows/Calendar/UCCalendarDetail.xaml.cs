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
using PostCalendarWindows.DataModel;
using PostCalendarWindows.ViewModel;

namespace PostCalendarWindows.Calendar
{
    /// <summary>
    /// UCCalendarDetail.xaml 的交互逻辑
    /// </summary>
    public partial class UCCalendarDetail : UserControl
    {
        private CalendarDetail detailItem;

        private Binding nameInputBindingObj = new Binding("Name");
        private Binding placeInputBindingObj = new Binding("Place");
        private Binding detailInputBindingObj = new Binding("Details");
        private Binding dateInputBindingObj = new Binding("DateStr");
        private Binding beginTimeInputBindingObj = new Binding("BeginTimeStr");
        private Binding endTimeInputBindingObj = new Binding("EndTimeStr");

        public UCCalendarDetail(CalendarEvent _event)
        {
            InitializeComponent();

            detailItem = new CalendarDetail(_event);

            nameInputBindingObj.Source = detailItem;
            placeInputBindingObj.Source = detailItem;
            detailInputBindingObj.Source = detailItem;
            dateInputBindingObj.Source = detailItem;
            beginTimeInputBindingObj.Source = detailItem;
            endTimeInputBindingObj.Source = detailItem;
            name_input.SetBinding(TextBox.TextProperty, nameInputBindingObj);
            place_input.SetBinding(TextBox.TextProperty, placeInputBindingObj);
            details_input.SetBinding(TextBox.TextProperty,detailInputBindingObj);
            date_input.SetBinding(DatePicker.TextProperty, dateInputBindingObj);
            start_time_input.SetBinding(TimePicker.TextProperty, beginTimeInputBindingObj);
            end_time_input.SetBinding(TimePicker.TextProperty, endTimeInputBindingObj);
        }

        private void confirm_button_Click(object sender, RoutedEventArgs e)
        {
            CustomizedButton? button = sender as CustomizedButton;
            CalendarEvent _event = new CalendarEvent();

            if (detailItem.Judge())
            {
                _event.SetInnar(detailItem.Name, detailItem.Place, detailItem.Details, detailItem.Date, detailItem.BeginTime, detailItem.EndTime);
                _event.Id = detailItem.Id;//这里是修改，必须设置id

                if(button != null)
                {
                    button.RaiseChangeEvent(_event);
                    button.RaiseRefreshEvent();
                }
            }
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            CustomizedButton? button = sender as CustomizedButton;
            if(button != null)
            {
                button.RaiseRefreshEvent();
            }
        }

        private void delete_button_Click(object sender, RoutedEventArgs e)
        {
            CustomizedButton? button = sender as CustomizedButton;

            if(button != null)
            {
                button.RaiseDeleteEvent(detailItem.Id);
                button.RaiseRefreshEvent();
            }
        }
    }
}
