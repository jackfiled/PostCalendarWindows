using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PostCalendarWindows.DDL;

#pragma warning disable CS8618

namespace PostCalendarWindows.ViewModel
{
    public class DDLSpanDetailItem
    {
        public string NameColumn { get; private set; }
        public string DetailColumn { get; private set; }
        public string StartTimeColumn { get; private set; }
        public string EndTimeColumn { get; private set; }
        public string ActivityTypeColumn { get; private set; }

        public string Name { get; set; }
        public string Detail { get; set; }
        public string StartDateStr { get; set; }
        public string StartTimeStr { get; set; }
        public string EndDateStr { get; set; }
        public string EndTimeStr { get; set; }
        public ActivityType activityType { get; set; }

        public DDLSpanDetailItem()
        {
            NameColumn = "活动名称";
            DetailColumn = "活动详情";
            StartTimeColumn = "活动开始时间";
            EndTimeColumn = "活动结束时间";
            ActivityTypeColumn = "活动分类";
        }
    }
}
