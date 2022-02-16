﻿using System;
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

        private void confirm_button_Click(object sender, RoutedEventArgs e)
        {
            CustomizedButton? button = sender as CustomizedButton;
            CalendarEvent _event = new CalendarEvent();

            _event.SetInnar(name_input.Text, place_input.Text, details_input.Text, DateOnly.Parse(date_input.Text), TimeOnly.Parse(start_time_input.Text), TimeOnly.Parse(end_time_input.Text));
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