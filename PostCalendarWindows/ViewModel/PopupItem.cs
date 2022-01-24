using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostCalendarWindows.ViewModel
{
    //弹出菜单对象的抽象基类
    public abstract class PopupItem
    {
        public string content { get; private set; }

        public PopupItem(string _content)
        {
            content = _content;
        }

        public abstract void isClicked();
    }

    public class ExcelOpenItem : PopupItem
    {
        public string? filePath = null;
        public ExcelOpenItem(string _content) : base(_content)
        {

        }

        public override void isClicked()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "Excel Files(.xls)|*.xls";
            bool? result = openFileDialog.ShowDialog();
            {
                if (result == true)
                {
                    filePath = openFileDialog.FileName;
                }
            }
        }
    }
}
