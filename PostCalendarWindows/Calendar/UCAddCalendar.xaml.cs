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

namespace PostCalendarWindows.Calendar
{
    /// <summary>
    /// UCAddCalendar.xaml 的交互逻辑
    /// </summary>
    public partial class UCAddCalendar : UserControl
    {
        public UCAddCalendar()
        {
            InitializeComponent();
        }

        public UCAddCalendar(string _name, string _details, DateOnly _date, TimeOnly _start_time, TimeOnly _end_time)
        {
            InitializeComponent();

            name_input.Text = _name;
            details_input.Text = _details;
            date_input.Text = _date.ToString();
            start_time_input.Text = _start_time.ToString();
            end_time_input.Text = _end_time.ToString();
        }

        private void confirm_button_Click(object sender, RoutedEventArgs e)
        {
            CustomizedButton? button = sender as CustomizedButton;
            CalendarEvent _event = new CalendarEvent();

            _event.SetInner(name_input.Text, place_input.Text, details_input.Text, DateOnly.Parse(date_input.Text), TimeOnly.Parse(start_time_input.Text), TimeOnly.Parse(end_time_input.Text));
            if(button != null)
            {
                button.RaiseAddEvent(_event);
                button.RaiseRefreshEvent();
            }
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            CustomizedButton? button = sender as CustomizedButton;
            if( button != null)
            {
                button.RaiseRefreshEvent();
            }
        }
    }
}
