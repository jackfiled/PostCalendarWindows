# 邮历（Windows版）
一款集日程管理与DDL管理于一身的日历软件。 

# 开发未完成，仍在内测阶段

## 支持的功能
### 日历部分

![日历截图](/img/Calendar.png)

1. 支持日历事件的添加，删除，修改。
2. 支持读取教务处自动生成的excel课表文件。
> 这个功能需要电脑上安装excel应用程序.
>
> 目前这个功能仅作实验性的支持，不保证excel读取的完全准确。
>
> > 使用帮助：在教务处网站“学期理论课表”页面有打印按钮，可以下载一个excel表格。
> >
> > 下载完成后，在软件里点击“导入excel课表”按钮，选择excel文件下载的位置，即可自动导入。

### DDL部分
![DDL界面截图](/img/DDL.png)

1. 支持DDL事件的添加，修改，完成，删除。

2. 可以将活动界面中DDL类型的事件直接添加到个人DDL中。

### 活动部分

![活动截图](/img/Activity.png)

**本部分未完成，设想中将与[DDL网站](http://squidward.top)的数据进行同步**

## 使用方法

软件依赖于.net6.0，请点击[链接](https://dotnet.microsoft.com/zh-cn/download/dotnet/thank-you/runtime-desktop-6.0.2-windows-x64-installer)下载.net6.0运行时。 
在安装完成或者确认电脑上已安装.net6.0运行时之后，点击下载旁边的release包，双击下载文件中的PostCalendarWindows.exe即可使用。

