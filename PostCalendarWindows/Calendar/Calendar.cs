using System.Collections.Generic;
using System.Data;
//这个命名空间都是和进程操作有关
using System.Diagnostics;
//这个COM组件可以调用excel所有的功能
using Excel = Microsoft.Office.Interop.Excel;

namespace PostCalendarWindows.Calendar
{
    public class Calendar
    {
        public List<Curriculum> events = new List<Curriculum>();


        public Calendar()
        {

        }

        static DataTable readExecl(string path)
        {
            DataTable dt = new DataTable();
            //不得不说，大微软家的东西可以相互调用就是方便
            Excel.Application app = new Excel.Application();
            Excel.Workbooks wbs = app.Workbooks;
            wbs.Open(path);
            Excel.Worksheet ws = app.Worksheets[1];
            
            int rows = ws.UsedRange.Rows.Count;
            int columns = ws.UsedRange.Columns.Count;

            for(int i = 1; i <= rows; i++)
            {
                DataRow dr = dt.NewRow();
                for(int j = 1; j <= columns; j++)
                {
                    //这里按照我的理解，就是选定那一个单元格
                    //谨记调用Office的API是图形化的API
                    Excel.Range range = ws.Range[app.Cells[i, j], app.Cells[i, j]];
                    range.Select();
                    //这里处理表头，并且考虑到了存在重复列名的问题
                    //不过，在我这个程序中不会用到列名
                    if (i == 1)
                    {
                        string colName = app.ActiveCell.Text.ToString();
                        if (dt.Columns.Contains(colName))
                        {
                            dt.Columns.Add(colName + j);
                        }
                        else
                        {
                            dt.Columns.Add(colName);
                        }
                    }
                    dr[j - 1] = app.ActiveCell.Text.ToString();
                }
                dt.Rows.Add(dr);
            }

            app.Quit();

            //貌似除了直接杀掉，没有其他的办法可以干掉这个打开的excel
            //这真是愚蠢
            //难道微软连这个简单的事情都没办法做好吗？
            Process[] pros = Process.GetProcessesByName("excel");
            foreach(Process proc in pros)
            {
                proc.Kill();
            }

            //将表格开头的两行干掉
            //还有最后一行备注也是
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(0);
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
            return dt;
        }

        void Analyse_excel_data(DataTable dt)
        {
            //北邮作息时间表
            int[] class_start_array = { 800, 850, 950, 1040, 1130, 1300, 1350, 1445, 1540, 1635, 1725, 1830, 1920, 2010 };

            string? last_curriculum_name = null;
            string name;
            string teacher;
            string place;
            int start_week, end_week;

            System.Text.RegularExpressions.Regex pattern = new System.Text.RegularExpressions.Regex(@"\d+");
            System.Text.RegularExpressions.MatchCollection match_result1;
            System.Text.RegularExpressions.MatchCollection match_result2;

            foreach(DataColumn col in dt.Columns)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    string? cell = dr[col].ToString();
                    //首先判断单元格里有信息
                    if(cell!=null && cell.Length != 1)
                    {
                        string[] result = cell.Split('\n');
                        //这里只处理这两种情况，避免一些不必要的麻烦
                        //没有被处理的单元格引发错误吧
                        if(result.Length == 6 || result.Length == 7)
                        {
                            name = result[1];
                            if(name == last_curriculum_name)
                            {
                                continue;
                            }
                            else
                            {
                                last_curriculum_name = name;
                                //分别匹配两种情况
                                if(result.Length == 6)
                                {
                                    match_result1 = pattern.Matches(result[3]);
                                    match_result2 = pattern.Matches(result[5]);
                                    teacher = result[2];
                                    place = result[4];
                                }
                                else
                                {
                                    match_result1 = pattern.Matches(result[4]);
                                    match_result2 = pattern.Matches(result[6]);
                                    teacher = result[3];
                                    place = result[5];
                                }
                                //处理有的课只上一周的问题
                                if(match_result1.Count == 1)
                                {
                                    start_week = int.Parse(match_result1[0].Value);
                                    end_week = start_week;
                                }
                                else
                                {
                                    start_week = int.Parse(match_result1[0].Value);
                                    end_week = int.Parse(match_result1[1].Value);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public class Curriculum
    //存储课程信息的类
    {
        public struct LastLength
        {
            public int start;
            public int end;
        }

        public string name;
        public string details;
        public int begin_time;
        public int end_time;
        public int length;
        public LastLength last_week;

        Curriculum(string event_name, string event_details, int begin, int end, int start_week, int end_week)
        {
            name = event_name;
            details = event_details;
            begin_time = begin;
            end_time = end;
            length = end - begin;
            last_week.start = start_week;
            last_week.end = end_week;
        }
    }
}
