using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using PostCalendarWindows.DDL;

namespace PostCalendarWindows.ViewModel
{
    public class DDLColumnItem : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string content { get; private set; }
        public PackIconKind packIconKind { get; private set; }
        public ColumnType type { get; private set; }
        public object itemType { get; set; }
        public bool isClicked { get; set; }

        public DDLColumnItem(string _content, PackIconKind _kind, ColumnType _type, object _itemType)
        {
            content = _content;
            packIconKind = _kind;
            type = _type;
            itemType = _itemType;            
            isClicked = false;
        }

        public void NotifyIsClickedChanged()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(isClicked)));
        }
    }
}
