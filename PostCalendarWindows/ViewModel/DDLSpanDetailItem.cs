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

        public int Id { get; set; }
        public string Name { get; set; }
        public string Detail { get; set; }
        public string StartDateStr { get; set; }
        public string StartTimeStr { get; set; }
        public string EndDateStr { get; set; }
        public string EndTimeStr { get; set; }
        public ActivityType activityType { get; set; }
        public DateTime StartDateTime
        {
            get
            {
                DateOnly date = DateOnly.Parse(StartDateStr);
                TimeOnly time = TimeOnly.Parse(StartTimeStr);
                return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
            }
        }
        public DateTime EndDateTime
        {
            get
            {
                DateOnly date = DateOnly.Parse(EndDateStr);
                TimeOnly time = TimeOnly.Parse(EndTimeStr);
                return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
            }
        }

        /// <summary>
        /// 表示是否为更新事件界面
        /// </summary>
        public bool isUpdate;

        /// <summary>
        /// 添加ddl时间段事件时的构造函数
        /// </summary>
        public DDLSpanDetailItem()
        {
            isUpdate = false;

            NameColumn = "活动名称";
            DetailColumn = "活动详情";
            StartTimeColumn = "活动开始时间";
            EndTimeColumn = "活动结束时间";
            ActivityTypeColumn = "活动分类";
        }

        public DDLSpanDetailItem(DeadlineSpanEvent _event)
        {
            isUpdate = true;

            NameColumn = "活动名称";
            DetailColumn = "活动详情";
            StartTimeColumn = "活动开始时间";
            EndTimeColumn = "活动结束时间";
            ActivityTypeColumn = "活动分类";

            Id = _event.Id;
            Name = _event.Name;
            Detail = _event.Details;
            StartDateStr = new DateOnly(_event.StartDateTime.Year, _event.StartDateTime.Month, _event.StartDateTime.Day).ToString();
            StartTimeStr = new TimeOnly(_event.StartDateTime.Hour, _event.StartDateTime.Minute, _event.StartDateTime.Second).ToString();
            EndDateStr = new DateOnly(_event.EndDateTime.Year, _event.EndDateTime.Month, _event.EndDateTime.Day).ToString();
            EndTimeStr = new TimeOnly(_event.EndDateTime.Hour, _event.EndDateTime.Minute, _event.EndDateTime.Second).ToString();
            activityType = _event.activityType;
        }
    }
}
