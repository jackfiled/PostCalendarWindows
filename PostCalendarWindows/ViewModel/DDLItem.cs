using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostCalendarWindows.DDL;

namespace PostCalendarWindows.ViewModel
{
    public class DDLItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string DDLTime { get; private set; }
        public string Details { get; private set; }

        public DDLItem(DeadlineEvent _event)
        {
            Id = _event.Id;
            Name = _event.Name;
            DDLTime = _event.EndDateTime.ToString();
            Details = _event.Details;
        }
    }
}
