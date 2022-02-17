using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostCalendarWindows.DDL
{
    public enum DDLClassify
    {
        DDL = 0,
        Activity,
    }

    public enum DDLType
    {
        All = 0,
        Study,
        Personal,
    }

    public enum ActivityType
    {
        All = 0,
        Thought,
        PE,
        Volunteer,
        Lecture,
        Competition,
        Recoginition,
        Other,
    }

    public enum ColumnType
    {
        Main = 0,
        DDL,
        Activity,
    }
}
