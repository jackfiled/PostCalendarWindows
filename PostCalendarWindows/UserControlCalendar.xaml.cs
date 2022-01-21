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
using PostCalendarWindows.Calendar;
using PostCalendarWindows.ViewModel;

namespace PostCalendarWindows
{
    /// <summary>
    /// Calendar.xaml 的交互逻辑
    /// </summary>
    public partial class UserControlCalendar : UserControl
    {
        public UserControlCalendar()
        {
            DateTime dt = DateTime.Now;
            ColumnTimeData ctd = new ColumnTimeData(dt.Year, dt.Month, dt.Day, dt.DayOfWeek);
            this.DataContext = ctd;
            InitializeComponent();
        }
    }
}
