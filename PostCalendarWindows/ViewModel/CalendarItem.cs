using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostCalendarWindows.ViewModel
{
    public class CalendarItem
    {
        public string name { get; private set; }
        public string place { get; private set; }
        public int height { get; private set; }

        public CalendarItem(string _name, string _place, int _height)
        {
            name = _name;
            place = _place;
            height = _height;
        }
    }
}
