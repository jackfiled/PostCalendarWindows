using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostCalendarWindows.Calendar;

namespace PostCalendarWindows.ViewModel
{
    public class CalendarItem
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public string place { get; private set; }
        public double height { get; private set; }

        public CalendarItem(int _id ,string _name, string _place, double _height)
        {
            id = _id;
            name = _name;
            place = _place;
            height = _height;
        }
    }
}
