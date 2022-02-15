﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostCalendarWindows.Calendar;

namespace PostCalendarWindows.ViewModel
{
    public class CalendarDetail
    {
        public string Name { get; set; }
        public string Place { get; set; }
        public string Details { get; set; }
        public string DateStr { get; set; }
        public string BeginTimeStr { get; set; }
        public string EndTimeStr { get; set; }
        public DateOnly Date
        {
            get
            {
                return DateOnly.Parse(DateStr);
            }
        }
        public TimeOnly BeginTime
        {
            get
            {
                return TimeOnly.Parse(BeginTimeStr);
            }
        }
        public TimeOnly EndTime
        {
            get
            {
                return TimeOnly.Parse(EndTimeStr);
            }
        }

        public CalendarDetail(CalendarEvent _event)
        {
            Name = _event.Name;
            Place = _event.Place;
            Details = _event.Details;
            DateStr = _event.Date_string;
            BeginTimeStr = _event.Begin_time_string;
            EndTimeStr = _event.End_time_string;
        }

        public bool Judge()
        {
            if(Name == null || Name.Length == 0)
            {
                return false;
            }
            if(Place == null || Place.Length == 0)
            {
                return false;
            }
            if(Details == null || Details.Length == 0)
            {
                return false;
            }
            if(DateStr == null || DateStr.Length == 0)
            {
                return false;
            }
            if(BeginTimeStr == null || BeginTimeStr.Length == 0)
            {
                return false;
            }
            if(EndTimeStr == null || EndTimeStr.Length == 0)
            {
                return false;
            }
            return true;
        }
    }
}
