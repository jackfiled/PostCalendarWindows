using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;

namespace PostCalendarWindows.Calendar
{
    public class Calendar
    {
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
            app = null;

            //貌似除了直接杀掉，没有其他的办法可以干掉这个打开的excel
            //这真是愚蠢
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
    }
}
