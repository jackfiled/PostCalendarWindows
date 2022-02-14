﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MaterialDesignThemes.Wpf;
using PostCalendarWindows.ViewModel;
using PostCalendarWindows.Setting;
using PostCalendarWindows.DataModel;

namespace PostCalendarWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database db;
        UserControlCalendar calendarUserControl;
        UserControlDDL ddlUserControl;


        public MainWindow()
        {
            InitializeComponent();

            //连接到数据库
            string database_path = @".\database.db";
            string connection = $"Data Source={database_path}";
            if (!System.IO.File.Exists(database_path))
            {
                System.IO.File.Create(database_path);
            }
            db = new Database(connection);

            //将数据库连接赋给相关的管理类
            calendarUserControl = new UserControlCalendar(db);
            ddlUserControl = new UserControlDDL();


            //显示相关的页面
            //在这里手动设置高度绑定到StackPanelMain
            var calendarBindingObj = new Binding("ActualHeight");
            calendarBindingObj.Source = StackPanelMain;
            calendarUserControl.SetBinding(HeightProperty, calendarBindingObj);

            var ddlBindingObj = new Binding("ActualHeight");
            ddlBindingObj.Source = StackPanelMain;
            ddlUserControl.SetBinding(HeightProperty, ddlBindingObj);

            var calendar_item = new ItemMenu("日历", PackIconKind.Schedule, calendarUserControl);
            var ddl_item = new ItemMenu("DDL", PackIconKind.ScaleBalance, ddlUserControl);

            ItemMenuList.Items.Add(new UserControlMenuItem(calendar_item));
            ItemMenuList.Items.Add(new UserControlMenuItem(ddl_item));

            //展示目前日历事件列表中的事件
            calendarUserControl.displayCanva();
        }

        public void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControlMenuItem selected_item = (UserControlMenuItem)((ListView)sender).SelectedItem;
            SwitchScreen(selected_item._item.Screen);
        }

        private void SwitchScreen(object sender)
        {
            var screen = (UserControl)sender;
            if (screen != null)
            {
                StackPanelMain.Children.Clear();
                StackPanelMain.Children.Add(screen);
            }
        }
    }
}
