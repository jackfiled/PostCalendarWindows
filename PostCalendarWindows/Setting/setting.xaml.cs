using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using PostCalendarWindows.ViewModel;
using PostCalendarWindows.DataModel;

namespace PostCalendarWindows.Setting
{
    /// <summary>
    /// setting.xaml 的交互逻辑
    /// </summary>
    public partial class setting : Window
    {
        Database db;

        public setting(Database _db)
        {
            InitializeComponent();

            //从数据库中查询一些常量数据
            db = _db;

            var CalendarSettingItem = new ItemMenu("日历设置", PackIconKind.Settings, new UserControlCalendarSetting());
            var DDLSettingItem = new ItemMenu("DDL设置", PackIconKind.SettingsApplications, new UserControlDDLSetting());
            var GeneralSettingItem = new ItemMenu("设置", PackIconKind.SettingsVertical, new UserControlGeneralSetting());
            
            SettingItem.Items.Add(new UserControlMenuItem(CalendarSettingItem));
            SettingItem.Items.Add(new UserControlMenuItem(DDLSettingItem));
            SettingItem.Items.Add(new UserControlMenuItem(GeneralSettingItem));

        }

        private void SettingItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl screen = ((UserControlMenuItem)(sender as ListView).SelectedItem)._item.Screen;
            if(screen != null)
            {
                SettingMain.Children.Clear();
                SettingMain.Children.Add(screen);
            }
        }
    }
}
